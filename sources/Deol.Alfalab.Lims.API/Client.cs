using Deol.Alfalab.Lims.API.Messages;
using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API
{
    public class Client : IDisposable
    {
        private HttpClient HttpClient { get; } = new HttpClient();

        private Encoding RequestEncoding { get; set; }

        private Encoding ResponseEncoding { get; set; }

        public Client(Uri baseAddress, ClientOptions clientOptions)
        {
            HttpClient.BaseAddress = baseAddress;

            RequestEncoding = clientOptions.RequestEncoding ?? Encoding.UTF8;
            ResponseEncoding = clientOptions.ResponseEncoding ?? Encoding.GetEncoding("Windows-1251");
        }

        public Client(string baseAddress, ClientOptions clientOptions) : this(new Uri(baseAddress), clientOptions) { }

        public Client(Uri baseAddress) : this(baseAddress, new ClientOptions()) { }

        public Client(string baseAddress) : this(new Uri(baseAddress), new ClientOptions()) { }

        public void Dispose() => HttpClient.Dispose();

        #region Public API

        public async Task<ResponseImportReferral> CreateOrUpdateReferralAsync(RequestCreateOrUpdateReferral request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseImportReferral>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseImportReferral> RemoveReferralAsync(RequestRemoveReferral request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseImportReferral>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseCreateDoctorOrders> CreateDoctorOrdersAsync(RequestCreateDoctorOrders request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseCreateDoctorOrders>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseChangeEmail> ChangeEmailAsync(RequestChangeEmail request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseChangeEmail>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseReferralResults> GetReferralResultsAsync(RequestReferralResults request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseReferralResults>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseReferralResults> GetNextReferralResultsAsync(RequestNextReferralResults request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseReferralResults>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseCountReferralResults> GetCountReferralResultsAsync(RequestCountReferralResults request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseCountReferralResults>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseNewReferralResults> GetNewReferralResultsAsync(RequestNewReferralResults request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseNewReferralResults>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseMessage> SetReferralResultsImportAsync(RequestReferralResultsImport request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseMessage>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponsePatientReferralResults> GetPatientReferralResultsAsync(RequestPatientReferralResults request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponsePatientReferralResults>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseDictionariesVersion> GetDictionariesVersionAsync(RequestDictionariesVersion request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseDictionariesVersion>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseDictionaries> GetDictionariesAsync(RequestDictionaries request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponseDictionaries>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponsePreprintBarcodes> GetPreprintBarcodesAsync(RequestPreprintBarcodes request, CancellationToken cancellationToken = default) =>
            await SendMessageAsync<ResponsePreprintBarcodes>(request, cancellationToken).ConfigureAwait(false);

        public async Task<ResponseBlankFile> GetBlankFileBinaryAsync(RequestBlankFile request, CancellationToken cancellationToken = default)
        {
            var httpResponse = await SendRequestAsync(request, cancellationToken).ConfigureAwait(false);

            var response = await ParsingResponseFileBinaryAsync(httpResponse).ConfigureAwait(false);

            return response;

            async Task<ResponseBlankFile> ParsingResponseFileBinaryAsync(HttpResponseMessage httpResponseMessage)
            {
                var resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                var contentType = httpResponseMessage.Content.Headers.ContentType.MediaType;

                var responseBlankFile = new ResponseBlankFile();

                if (contentType == "application/xml")
                {
                    var resultString = Encoding.GetEncoding("Windows-1251").GetString(resultBytes);
                    var xml = XDocument.Parse(resultString);

                    responseBlankFile.InitMessage(xml);
                }
                else if (contentType.Substring(0, 12) == "application/")
                {
                    var fileExtension = contentType.Substring(12);
                    responseBlankFile.InitBinaryData(resultBytes, fileExtension);
                }
                else
                {
                    responseBlankFile.InitBinaryData(resultBytes, null);
                }

                return responseBlankFile;
            }
        }

        #endregion

        private async Task<TResponseMessage> SendMessageAsync<TResponseMessage>(IRequestMessage queryMessage, CancellationToken cancellationToken = default)
            where TResponseMessage : IResponseMessage, new()
        {
            var httpResponse = await SendRequestAsync(queryMessage, cancellationToken).ConfigureAwait(false);

            var response = await ParsingResponseAsync<TResponseMessage>(httpResponse).ConfigureAwait(false);

            return response;
        }

        private async Task<HttpResponseMessage> SendRequestAsync(IRequestMessage queryMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                var xml = queryMessage.ToXMLMessage();
                
                xml.Declaration = new XDeclaration("1.0", RequestEncoding.WebName, "yes");

                var message = xml.ToStringWithDeclaration();

                var content = new StringContent(message, RequestEncoding, "application/xml");

                return await HttpClient.PostAsync("/", content, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SendRequestException("Ошибка при отправке сообщения в ЛИС", ex);
            }
        }

        private async Task<TResponseMessage> ParsingResponseAsync<TResponseMessage>(HttpResponseMessage httpResponseMessage)
            where TResponseMessage : IResponseMessage, new()
        {
            try
            {
                var resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                var resultString = ResponseEncoding.GetString(resultBytes);

                var xml = XDocument.Parse(resultString);

                var response = new TResponseMessage();

                response.InitFromXMLMessage(xml);

                return response;
            }
            catch (Exception ex)
            {
                throw new ParsingResponseExсeption("Ошибка при разборе ответа от ЛИС", ex);
            }
        }
    }
}

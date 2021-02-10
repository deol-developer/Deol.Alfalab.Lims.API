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

        public void Dispose() => this.HttpClient.Dispose();

        public Client(Uri baseAddress, ClientOptions clientOptions)
        {
            this.HttpClient.BaseAddress = baseAddress;

            this.RequestEncoding = clientOptions.RequestEncoding ?? Encoding.UTF8;
            this.ResponseEncoding = clientOptions.ResponseEncoding ?? Encoding.GetEncoding("Windows-1251");
        }

        public Client(string baseAddress, ClientOptions clientOptions) : this(new Uri(baseAddress), clientOptions) { }

        public Client(Uri baseAddress) : this(baseAddress, new ClientOptions()) { }

        public Client(string baseAddress) : this(new Uri(baseAddress), new ClientOptions()) { }

        private async Task<TResponseMessage> SendMessageAsync<TResponseMessage>(IRequestMessage queryMessage, CancellationToken cancellationToken = default)
            where TResponseMessage : IResponseMessage, new()
        {
            return await ParsingResponseAsync<TResponseMessage>(await this.SendRequestAsync(queryMessage, cancellationToken));
        }

        private async Task<HttpResponseMessage> SendRequestAsync(IRequestMessage queryMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                var declaration = new XDeclaration("1.0", this.RequestEncoding.WebName, "yes").ToString();
                var message = queryMessage.ToXMLMessage();

                var content = new StringContent(declaration + Environment.NewLine + message, this.RequestEncoding, "application/xml");

                return await this.HttpClient.PostAsync("/", content, cancellationToken);
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
                var resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();

                var resultString = this.ResponseEncoding.GetString(resultBytes);

                var response = new TResponseMessage();
                response.InitFromXMLMessage(resultString);

                return response;
            }
            catch (Exception ex)
            {
                throw new ParsingResponseExсeption("Ошибка при разборе ответа от ЛИС", ex);
            }
        }

        public async Task<ResponseBlankFile> GetBlankFileBinaryAsync(RequestBlankFile request, CancellationToken cancellationToken = default)
        {
            return await ParsingResponseFileBinaryAsync(await this.SendRequestAsync(request, cancellationToken));

            async Task<ResponseBlankFile> ParsingResponseFileBinaryAsync(HttpResponseMessage httpResponseMessage)
            {
                var resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();

                var contentType = httpResponseMessage.Content.Headers.ContentType.MediaType;

                var responseBlankFile = new ResponseBlankFile();

                if (contentType == "application/xml")
                {
                    var resultString = Encoding.GetEncoding("Windows-1251").GetString(resultBytes);
                    responseBlankFile.InitMessage(resultString);
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

        public async Task<ResponseImportReferral> CreateOrUpdateReferralAsync(RequestCreateOrUpdateReferral request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseImportReferral>(request, cancellationToken);

        public async Task<ResponseImportReferral> RemoveReferralAsync(RequestRemoveReferral request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseImportReferral>(request, cancellationToken);

        public async Task<ResponseCreateDoctorOrders> CreateDoctorOrdersAsync(RequestCreateDoctorOrders request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseCreateDoctorOrders>(request, cancellationToken);

        public async Task<ResponseChangeEmail> ChangeEmailAsync(RequestChangeEmail request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseChangeEmail>(request, cancellationToken);

        public async Task<ResponseReferralResults> GetReferralResultsAsync(RequestReferralResults request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseReferralResults>(request, cancellationToken);

        public async Task<ResponseReferralResults> GetNextReferralResultsAsync(RequestNextReferralResults request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseReferralResults>(request, cancellationToken);

        public async Task<ResponseCountReferralResults> GetCountReferralResultsAsync(RequestCountReferralResults request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseCountReferralResults>(request, cancellationToken);

        public async Task<ResponseNewReferralResults> GetNewReferralResultsAsync(RequestNewReferralResults request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseNewReferralResults>(request, cancellationToken);

        public async Task<ResponseMessage> SetReferralResultsImportAsync(RequestReferralResultsImport request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseMessage>(request, cancellationToken);

        public async Task<ResponsePatientReferralResults> GetPatientReferralResultsAsync(RequestPatientReferralResults request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponsePatientReferralResults>(request, cancellationToken);

        public async Task<ResponseDictionariesVersion> GetDictionariesVersionAsync(RequestDictionariesVersion request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseDictionariesVersion>(request, cancellationToken);

        public async Task<ResponseDictionaries> GetDictionariesAsync(RequestDictionaries request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponseDictionaries>(request, cancellationToken);

        public async Task<ResponsePreprintBarcodes> GetPreprintBarcodesAsync(RequestPreprintBarcodes request, CancellationToken cancellationToken = default) => await this.SendMessageAsync<ResponsePreprintBarcodes>(request, cancellationToken);
    }
}

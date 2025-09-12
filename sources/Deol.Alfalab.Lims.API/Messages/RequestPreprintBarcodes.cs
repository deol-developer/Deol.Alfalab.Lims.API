using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestPreprintBarcodes : RequestMessage
    {
        public RequestElementCount Query { get; } = new RequestElementCount();

        public override string MessageType => "query-preprint-barcodes";

        public RequestPreprintBarcodes() : base() { }

        public RequestPreprintBarcodes(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            Query.ToXMLElement()
        };
    }

    public class RequestElementCount : IRequestMessageElement
    {
        public string HospitalCode { get; set; }

        public int Count { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Query", new XAttribute("Count", Count));

            if (HospitalCode != null)
                element.Add(new XAttribute("HospitalCode", HospitalCode));

            return element;
        }
    }
}

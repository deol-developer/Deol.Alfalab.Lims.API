using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestChangeEmail : RequestMessage
    {
        public RequestElementEmail Query { get; } = new RequestElementEmail();
        
        public override string MessageType => "query-change-request-email";

        public RequestChangeEmail() : base() { }

        public RequestChangeEmail(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            this.Query.ToXMLElement()
        };
    }

    public class RequestElementEmail : IRequestMessageElement
    {
        public string MisId { get; set; }
        public string Nr { get; set; }
        public int? LisId { get; set; }
        public string EmailAddress { get; set; }

        public XElement ToXMLElement() =>
            new XElement("Query",
                new XAttribute("LisId",         MessageHelper.GetAttributeValue(this.LisId)),
                new XAttribute("MisId",         MessageHelper.GetAttributeValue(this.MisId)),
                new XAttribute("Nr",            MessageHelper.GetAttributeValue(this.Nr)),
                new XAttribute("EmailAddress",  MessageHelper.GetAttributeValue(this.EmailAddress)));
    }
}

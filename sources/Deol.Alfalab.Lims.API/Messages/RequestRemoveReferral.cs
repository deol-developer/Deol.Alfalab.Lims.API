using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestRemoveReferral : RequestMessage
    {
        public RequestElementReferralId Query { get; } = new RequestElementReferralId();
        
        public override string MessageType => "query-referral-remove";

        public RequestRemoveReferral() : base() { }

        public RequestRemoveReferral(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            this.Query.ToXMLElement()
        };
    }

    public class RequestElementReferralId : IRequestMessageElement
    {
        private string ElementName { get; }

        public RequestElementReferralId(string elementName = "Query") => this.ElementName = elementName;

        public string MisId { get; set; }
        public string Nr { get; set; }
        public int? LisId { get; set; }

        public XElement ToXMLElement() => 
            new XElement(this.ElementName, 
                new XAttribute("LisId", MessageHelper.GetAttributeValue(this.LisId)), 
                new XAttribute("MisId", MessageHelper.GetAttributeValue(this.MisId)),
                new XAttribute("Nr",    MessageHelper.GetAttributeValue(this.Nr)));
    }
}

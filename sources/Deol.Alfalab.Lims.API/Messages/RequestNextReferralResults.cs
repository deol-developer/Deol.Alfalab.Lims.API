using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestNextReferralResults : RequestMessage
    {
        public RequestElementQueueParameters Query { get; } = new RequestElementQueueParameters();

        public override string MessageType => "query-next-referral-results";

        public RequestNextReferralResults() : base() { }

        public RequestNextReferralResults(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => 
            this.Query.IsEmpty ? Enumerable.Empty<XElement>() : new XElement[] { this.Query.ToXMLElement() };
    }

    public class RequestElementQueueParameters : IRequestMessageElement
    {
        public bool? OnlyCreatedFromLis { get; set; }
        public bool? AllowModified { get; set; }
        public string ModValue { get; set; }
        public string ModCount { get; set; }

        public bool IsEmpty =>
            this.OnlyCreatedFromLis == null &&
            this.AllowModified == null &&
            this.ModValue == null &&
            this.ModCount == null;

        public XElement ToXMLElement()
        {
            var element = new XElement("Query");

            if (this.OnlyCreatedFromLis != null)
                element.Add(new XAttribute("OnlyCreatedFromLis", MessageHelper.GetAttributeValue(this.OnlyCreatedFromLis.Value)));

            if (this.AllowModified != null)
                element.Add(new XAttribute("AllowModified", MessageHelper.GetAttributeValue(this.AllowModified.Value)));

            if (this.ModValue != null)
                element.Add(new XAttribute("ModValue", this.ModValue));

            if (this.ModCount != null)
                element.Add(new XAttribute("ModCount", this.ModCount));

            return element;
        }
    }
}

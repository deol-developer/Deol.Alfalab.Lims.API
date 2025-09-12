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
            Query.IsEmpty ? Enumerable.Empty<XElement>() : new XElement[] { Query.ToXMLElement() };
    }

    public class RequestElementQueueParameters : IRequestMessageElement
    {
        public bool? OnlyCreatedFromLis { get; set; }
        public bool? AllowModified { get; set; }
        public string ModValue { get; set; }
        public string ModCount { get; set; }

        public bool IsEmpty =>
            OnlyCreatedFromLis == null &&
            AllowModified == null &&
            ModValue == null &&
            ModCount == null;

        public XElement ToXMLElement()
        {
            var element = new XElement("Query");

            if (OnlyCreatedFromLis != null)
                element.Add(new XAttribute("OnlyCreatedFromLis", MessageHelper.GetAttributeValue(OnlyCreatedFromLis.Value)));

            if (AllowModified != null)
                element.Add(new XAttribute("AllowModified", MessageHelper.GetAttributeValue(AllowModified.Value)));

            if (ModValue != null)
                element.Add(new XAttribute("ModValue", ModValue));

            if (ModCount != null)
                element.Add(new XAttribute("ModCount", ModCount));

            return element;
        }
    }
}

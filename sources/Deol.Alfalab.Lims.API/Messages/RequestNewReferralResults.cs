using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestNewReferralResults : RequestMessage
    {
        public RequestElementQueueRangeParameters Query { get; } = new RequestElementQueueRangeParameters();

        public override string MessageType => "query-new-referral-results";

        public RequestNewReferralResults() : base() { }

        public RequestNewReferralResults(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            this.Query.ToXMLElement()
        };
    }

    public class RequestElementQueueRangeParameters : IRequestMessageElement
    {
        public bool? OnlyCreatedFromLis { get; set; }
        public bool? AllowModified { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }

        public XElement ToXMLElement()
        {
            var element =
                new XElement("Query",
                    new XAttribute("DateFrom", MessageHelper.GetAttributeValue(this.DateFrom)),
                    new XAttribute("DateTill", MessageHelper.GetAttributeValue(this.DateTill)));

            if (this.OnlyCreatedFromLis != null)
                element.Add(new XAttribute("OnlyCreatedFromLis", MessageHelper.GetAttributeValue(this.OnlyCreatedFromLis.Value)));

            if (this.AllowModified != null)
                element.Add(new XAttribute("AllowModified", MessageHelper.GetAttributeValue(this.AllowModified.Value)));

            return element;
        }
    }
}

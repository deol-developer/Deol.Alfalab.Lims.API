using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestCountReferralResults : RequestMessage
    {
        public RequestElementQueueParameters Query { get; } = new RequestElementQueueParameters();

        public override string MessageType => "query-count-referral-results";

        public RequestCountReferralResults() : base() { }

        public RequestCountReferralResults(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() =>
            this.Query.IsEmpty? Enumerable.Empty<XElement>() : new XElement[] { this.Query.ToXMLElement() };
    }
}

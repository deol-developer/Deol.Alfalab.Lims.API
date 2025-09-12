using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestReferralResults : RequestMessage
    {
        public RequestElementReferralId Query { get; } = new RequestElementReferralId();

        public override string MessageType => "query-referral-results";

        public RequestReferralResults() : base() { }

        public RequestReferralResults(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            Query.ToXMLElement()
        };
    }
}

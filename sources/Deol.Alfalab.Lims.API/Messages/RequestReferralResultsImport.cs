using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestReferralResultsImport : RequestMessage
    {
        public RequestElementVersion Version { get; } = new RequestElementVersion();

        public RequestElementReferralId Referral { get; } = new RequestElementReferralId("Referral");

        public override string MessageType => "result-referral-results-import";

        public RequestReferralResultsImport() : base() { }

        public RequestReferralResultsImport(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            this.Version.ToXMLElement(),
            this.Referral.ToXMLElement()
        };
    }

    public class RequestElementVersion : IRequestMessageElement
    {
        public int Version { get; set; }

        public XElement ToXMLElement() =>
            new XElement("Version",
                new XAttribute("Version", this.Version));
    }
}

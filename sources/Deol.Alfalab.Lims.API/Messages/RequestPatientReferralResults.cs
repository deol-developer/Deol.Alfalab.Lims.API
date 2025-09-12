using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestPatientReferralResults : RequestMessage
    {
        public RequestElementPatientId Query { get; } = new RequestElementPatientId();

        public override string MessageType => "query-patient-referral-results";

        public RequestPatientReferralResults() : base() { }

        public RequestPatientReferralResults(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            Query.ToXMLElement()
        };
    }

    public class RequestElementPatientId : IRequestMessageElement
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTill { get; set; }

        public bool UseUpdateDate { get; set; }

        public string PatientMisId { get; set; }

        public string PatientCode1 { get; set; }

        public string PatientCode2 { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Query");

            if (PatientMisId != null)
                element.Add(new XAttribute("PatientMisId", PatientMisId));

            if (PatientCode1 != null)
                element.Add(new XAttribute("PatientCode1", PatientCode1));

            if (PatientCode2 != null)
                element.Add(new XAttribute("PatientCode2", PatientCode2));

            if (DateFrom != null)
                element.Add(new XAttribute("DateFrom", MessageHelper.GetAttributeValue(DateFrom.Value)));

            if (DateTill != null)
                element.Add(new XAttribute("DateTill", MessageHelper.GetAttributeValue(DateTill.Value)));

            element.Add(new XAttribute("UseUpdateDate", MessageHelper.GetAttributeValue(UseUpdateDate)));

            return element;
        }
    }
}

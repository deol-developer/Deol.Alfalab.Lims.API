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
            this.Query.ToXMLElement()
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

            if (this.PatientMisId != null)
                element.Add(new XAttribute("PatientMisId", this.PatientMisId));

            if (this.PatientCode1 != null)
                element.Add(new XAttribute("PatientCode1", this.PatientCode1));

            if (this.PatientCode2 != null)
                element.Add(new XAttribute("PatientCode2", this.PatientCode2));

            if (this.DateFrom != null)
                element.Add(new XAttribute("DateFrom", MessageHelper.GetAttributeValue(this.DateFrom.Value)));

            if (this.DateTill != null)
                element.Add(new XAttribute("DateTill", MessageHelper.GetAttributeValue(this.DateTill.Value)));

            element.Add(new XAttribute("UseUpdateDate", MessageHelper.GetAttributeValue(this.UseUpdateDate)));

            return element;
        }
    }
}

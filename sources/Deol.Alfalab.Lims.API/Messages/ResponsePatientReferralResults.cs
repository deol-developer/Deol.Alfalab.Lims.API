using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponsePatientReferralResults : ResponseMessage
    {
        public ResponseElementPatientId Query { get; private set; }

        public IEnumerable<ResponseElementStrongReferralId> Results { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            Query = MessageHelper.GetResponseMessageElement<ResponseElementPatientId>(message.Element("Query"));

            Results = MessageHelper.GetResponseMessageElements<ResponseElementStrongReferralId>(message.Element("Results"));
        }
    }

    public class ResponseElementPatientId : IResponseMessageElement
    {
        public DateTime? DateFrom { get; private set; }

        public DateTime? DateTill { get; private set; }

        public bool? UseUpdateDate { get; private set; }

        public string PatientMisId { get; private set; }

        public string PatientCode1 { get; private set; }

        public string PatientCode2 { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            PatientMisId = element.Attribute("PatientMisId")?.Value;
            PatientCode1 = element.Attribute("PatientCode1")?.Value;
            PatientCode2 = element.Attribute("PatientCode2")?.Value;

            if (DateTime.TryParseExact(element.Attribute("DateFrom")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateFrom))
                DateFrom = dateFrom;

            if (DateTime.TryParseExact(element.Attribute("DateTill")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTill))
                DateTill = dateTill;

            if (bool.TryParse(element.Attribute("UseUpdateDate")?.Value, out var useUpdateDate))
                UseUpdateDate = useUpdateDate;
        }
    }

    public class ResponseElementStrongReferralId : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }
        public int LisId { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("LisId")?.Value, out var lisId))
                LisId = lisId;

            MisId = element.Attribute("MisId")?.Value;
            Nr = element.Attribute("Nr")?.Value;
        }
    }
}

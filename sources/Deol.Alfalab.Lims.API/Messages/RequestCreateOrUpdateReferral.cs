using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestCreateOrUpdateReferral : RequestMessage
    {
        public bool ForUpdate { get; }

        public bool UseUserFields => Referral.UseUserFields;

        public RequestCreateOrUpdateReferral(bool usePatientUserFields = false, bool useReferralUserFields = false, bool forUpdate = false) 
            : base()
        {
            ForUpdate = forUpdate;

            Pation = new RequestElementPation(usePatientUserFields);

            Referral = new RequestElementReferral(useReferralUserFields);

            Assays = new List<RequestElementAssay>();
        }

        public RequestCreateOrUpdateReferral(AuthorizationData authorizationData, bool usePatientUserFields = false, bool useReferralUserFields = false, bool forUpdate = false)
            : base(authorizationData)
        {
            ForUpdate = forUpdate;

            Pation = new RequestElementPation(usePatientUserFields);

            Referral = new RequestElementReferral(useReferralUserFields);

            Assays = new List<RequestElementAssay>();
        }

        public RequestElementPation Pation { get; }

        public RequestElementReferral Referral { get; }

        public ICollection<RequestElementAssay> Assays { get; }

        public override string MessageType => ForUpdate ? "query-edit-referral" : "query-create-referral";

        protected override IEnumerable<XElement> GetMessageElements()
        {
            var elements = new List<XElement>(3)
            {
                Pation.ToXMLElement(),
                Referral.ToXMLElement()
            };

            if (Assays.Any())
                elements.Add(new XElement("Assays", Assays.Select(x => x.ToXMLElement())));

            return elements;
        }
    }

    public enum CyclePeriod
    {
        NotSet = 0,
        Ovulatory = 1,
        Follicular = 2,
        Luteal = 3,
        Menopause = 4,
        Postmenopausal = 5,
        Premenopause = 6,
        NoPregnancyNoCyclePeriod = 7,
        PregnancyNoTerm = 8
    }

    public enum PationGender
    {
        NotSet = 0,
        Male = 1,
        Female = 2
    }

    public class RequestElementPation : IRequestMessageElement
    {
        public bool UseUserFields { get; }
        public Dictionary<string, string> UserFields { get; }

        public RequestElementPation(bool useUserFields = false)
        {
            UseUserFields = useUserFields;

            if (useUserFields)
                UserFields = new Dictionary<string, string>();
        }

        public string MisId { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public PationGender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? BirthYear { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Patient");

            if (MisId != null)
                element.Add(new XAttribute("MisId", MisId));

            if (Code1 != null)
                element.Add(new XAttribute("Code1", Code1));

            if (Code2 != null)
                element.Add(new XAttribute("Code2", Code2));

            if (LastName != null)
                element.Add(new XAttribute("LastName", LastName));

            if (FirstName != null)
                element.Add(new XAttribute("FirstName", FirstName));

            if (LastName != null)
                element.Add(new XAttribute("MiddleName", MiddleName));

            if (Gender != null)
                element.Add(new XAttribute("Gender", (int)Gender));

            if (BirthDate != null)
                element.Add(new XAttribute("BirthDate", MessageHelper.GetAttributeValue(BirthDate.Value)));

            if (BirthYear != null)
                element.Add(new XAttribute("BirthYear", BirthYear.Value));

            if (UseUserFields)
                foreach (var userField in UserFields)
                    element.Add(new XAttribute(userField.Key, userField.Value));

            return element;
        }
    }

    public class RequestElementBaseReferral : IRequestMessageElement
    {
        public bool UseUserFields { get; }
        public Dictionary<string, string> UserFields { get; }

        public RequestElementBaseReferral(bool useUserFields = false)
        {
            UseUserFields = useUserFields;

            if (useUserFields)
                UserFields = new Dictionary<string, string>();
        }

        public string MisId { get; set; }
        public string Nr { get; set; }
        public string HospitalCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DoctorName { get; set; }
        public string DoctorProf { get; set; }
        public string DoctorCode { get; set; }
        public bool? Cito { get; set; }
        public string DiagnosisName { get; set; }
        public string DiagnosisCode { get; set; }
        public string Comment { get; set; }
        public int? PregnancyWeek { get; set; }
        public CyclePeriod? CyclePeriod { get; set; }
        public DateTime? LastMenstruation { get; set; }
        public int? DiuresisMl { get; set; }
        public float? WeightKg { get; set; }
        public int? HeightCm { get; set; }

        public virtual XElement ToXMLElement()
        {
            var element = new XElement("Referral");

            if (MisId != null)
                element.Add(new XAttribute("MisId", MisId));

            if (Nr != null)
                element.Add(new XAttribute("Nr", Nr));

            if (HospitalCode != null)
                element.Add(new XAttribute("HospitalCode", HospitalCode));

            if (DepartmentName != null)
                element.Add(new XAttribute("DepartmentName", DepartmentName));

            if (DepartmentCode != null)
                element.Add(new XAttribute("DepartmentCode", DepartmentCode));

            if (DoctorName != null)
                element.Add(new XAttribute("DoctorName", DoctorName));

            if (DoctorProf != null)
                element.Add(new XAttribute("DoctorProf", DoctorProf));

            if (DoctorCode != null)
                element.Add(new XAttribute("DoctorCode", DoctorCode));

            if (Cito != null)
                element.Add(new XAttribute("Cito", MessageHelper.GetAttributeValue(Cito.Value)));

            if (DiagnosisName != null)
                element.Add(new XAttribute("DiagnosisName", DiagnosisName));

            if (DiagnosisCode != null)
                element.Add(new XAttribute("DiagnosisCode", DiagnosisCode));

            if (Comment != null)
                element.Add(new XAttribute("Comment", Comment));

            if (PregnancyWeek != null)
                element.Add(new XAttribute("PregnancyWeek", PregnancyWeek.Value));

            if (CyclePeriod != null)
                element.Add(new XAttribute("CyclePeriod", (int)CyclePeriod.Value));

            if (LastMenstruation != null)
                element.Add(new XAttribute("LastMenstruation", MessageHelper.GetAttributeValue(LastMenstruation.Value)));

            if (DiuresisMl != null)
                element.Add(new XAttribute("DiuresisMl", DiuresisMl.Value));

            if (WeightKg != null)
                element.Add(new XAttribute("WeightKg", WeightKg.Value));

            if (HeightCm != null)
                element.Add(new XAttribute("HeightCm", HeightCm.Value));

            if (UseUserFields)
                foreach (var userField in UserFields)
                    element.Add(new XAttribute(userField.Key, userField.Value));

            return element;
        }
    }

    public class RequestElementReferral : RequestElementBaseReferral
    {
        public RequestElementReferral(bool useUserFields = false) : base(useUserFields) { }
        
        public int? LisId { get; set; }
        public int? MasterLisId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? SamplingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        
        public ICollection<RequestElementOrder> Orders { get; } = new List<RequestElementOrder>();

        public override XElement ToXMLElement()
        {
            var element = base.ToXMLElement();

            element.Add(new XAttribute("Date", MessageHelper.GetAttributeValue(Date)));

            if (LisId != null)
                element.Add(new XAttribute("LisId", LisId.Value));

            if (MasterLisId != null)
                element.Add(new XAttribute("MasterLisId", MasterLisId.Value));

            if (SamplingDate != null)
                element.Add(new XAttribute("SamplingDate", MessageHelper.GetAttributeValue(SamplingDate.Value)));

            if (DeliveryDate != null)
                element.Add(new XAttribute("DeliveryDate", MessageHelper.GetAttributeValue(DeliveryDate.Value)));

            if (Orders.Any())
                element.Add(new XElement("Orders", Orders.Select(x => x.ToXMLElement())));

            return element;
        }
    }

    public class RequestElementOrder : IRequestMessageElement
    {
        public string Code { get; set; }
        public string BiomaterialCode { get; set; }
        public string PayType { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DoctorName { get; set; }
        public string DoctorCode { get; set; }
        public string DiagnosisName { get; set; }
        public string DiagnosisCode { get; set; }
        public string CardNr { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Item", new XAttribute("Code", MessageHelper.GetAttributeValue(Code)));

            if (BiomaterialCode != null)
                element.Add(new XAttribute("BiomaterialCode", BiomaterialCode));

            if (PayType != null)
                element.Add(new XAttribute("PayType", PayType));
            
            if(DepartmentName != null)
                element.Add(new XAttribute("DepartmentName", DepartmentName));

            if(DepartmentCode != null)
                element.Add(new XAttribute("DepartmentCode", DepartmentCode));
            
            if (DoctorName != null)
                element.Add(new XAttribute("DoctorName", DoctorName));

            if(DoctorCode != null)
                element.Add(new XAttribute("DoctorCode", DoctorCode));

            if(DiagnosisName != null)
                element.Add(new XAttribute("DiagnosisName", DiagnosisName));
            
            if(DiagnosisCode != null)
                element.Add(new XAttribute("DiagnosisCode", DiagnosisCode));
            
            if(CardNr != null)
                element.Add(new XAttribute("CardNr", CardNr));

            return element;
        }
    }

    public class RequestElementAssay : IRequestMessageElement
    {
        public string Barcode { get; set; }
        public string BiomaterialCode { get; set; }

        public ICollection<RequestElementOrder> Orders { get; } = new List<RequestElementOrder>();

        public XElement ToXMLElement()
        {
            var element = new XElement("Item");

            if (Barcode != null)
                element.Add(new XAttribute("Barcode", Barcode));

            if (BiomaterialCode != null)
                element.Add(new XAttribute("BiomaterialCode", BiomaterialCode));

            if (Orders.Any())
                element.Add(new XElement("Orders", Orders.Select(x => x.ToXMLElement())));

            return element;
        }
    }
}

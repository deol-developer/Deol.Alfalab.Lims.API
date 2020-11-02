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

        public bool UseUserFields => this.Referral.UseUserFields;

        public RequestCreateOrUpdateReferral(bool usePatientUserFields = false, bool useReferralUserFields = false, bool forUpdate = false) 
            : base()
        {
            this.ForUpdate = forUpdate;

            this.Pation = new RequestElementPation(usePatientUserFields);

            this.Referral = new RequestElementReferral(useReferralUserFields);

            this.Assays = new List<RequestElementAssay>();
        }

        public RequestCreateOrUpdateReferral(AuthorizationData authorizationData, bool usePatientUserFields = false, bool useReferralUserFields = false, bool forUpdate = false)
            : base(authorizationData)
        {
            this.ForUpdate = forUpdate;

            this.Pation = new RequestElementPation(usePatientUserFields);

            this.Referral = new RequestElementReferral(useReferralUserFields);

            this.Assays = new List<RequestElementAssay>();
        }

        public RequestElementPation Pation { get; }

        public RequestElementReferral Referral { get; }

        public ICollection<RequestElementAssay> Assays { get; }

        public override string MessageType => this.ForUpdate ? "query-edit-referral" : "query-create-referral";

        protected override IEnumerable<XElement> GetMessageElements()
        {
            var elements = new List<XElement>(3)
            {
                this.Pation.ToXMLElement(),
                this.Referral.ToXMLElement()
            };

            if (this.Assays.Any())
                elements.Add(new XElement("Assays", this.Assays.Select(x => x.ToXMLElement())));

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
            this.UseUserFields = useUserFields;

            if (useUserFields)
                this.UserFields = new Dictionary<string, string>();
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

            if (this.MisId != null)
                element.Add(new XAttribute("MisId", this.MisId));

            if (this.Code1 != null)
                element.Add(new XAttribute("Code1", this.Code1));

            if (this.Code2 != null)
                element.Add(new XAttribute("Code2", this.Code2));

            if (this.LastName != null)
                element.Add(new XAttribute("LastName", this.LastName));

            if (this.FirstName != null)
                element.Add(new XAttribute("FirstName", this.FirstName));

            if (this.LastName != null)
                element.Add(new XAttribute("MiddleName", this.MiddleName));

            if (this.Gender != null)
                element.Add(new XAttribute("Gender", (int)this.Gender));

            if (this.BirthDate != null)
                element.Add(new XAttribute("BirthDate", MessageHelper.GetAttributeValue(this.BirthDate.Value)));

            if (this.BirthYear != null)
                element.Add(new XAttribute("BirthYear", this.BirthYear.Value));

            if (this.UseUserFields)
                foreach (var userField in this.UserFields)
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
            this.UseUserFields = useUserFields;

            if (useUserFields)
                this.UserFields = new Dictionary<string, string>();
        }

        public string MisId { get; set; }
        public string Nr { get; set; }
        public string HospitalCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialization { get; set; }
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

            if (this.MisId != null)
                element.Add(new XAttribute("MisId", this.MisId));

            if (this.Nr != null)
                element.Add(new XAttribute("Nr", this.Nr));

            if (this.HospitalCode != null)
                element.Add(new XAttribute("HospitalCode", this.HospitalCode));

            if (this.DepartmentName != null)
                element.Add(new XAttribute("DepartmentName", this.DepartmentName));

            if (this.DepartmentCode != null)
                element.Add(new XAttribute("DepartmentCode", this.DepartmentCode));

            if (this.DoctorName != null)
                element.Add(new XAttribute("DoctorName", this.DoctorName));

            if (this.DoctorSpecialization != null)
                element.Add(new XAttribute("DoctorSpecialization", this.DoctorSpecialization));

            if (this.DoctorCode != null)
                element.Add(new XAttribute("DoctorCode", this.DoctorCode));

            if (this.Cito != null)
                element.Add(new XAttribute("Cito", MessageHelper.GetAttributeValue(this.Cito.Value)));

            if (this.DiagnosisName != null)
                element.Add(new XAttribute("DiagnosisName", this.DiagnosisName));

            if (this.DiagnosisCode != null)
                element.Add(new XAttribute("DiagnosisCode", this.DiagnosisCode));

            if (this.Comment != null)
                element.Add(new XAttribute("Comment", this.Comment));

            if (this.PregnancyWeek != null)
                element.Add(new XAttribute("PregnancyWeek", this.PregnancyWeek.Value));

            if (this.CyclePeriod != null)
                element.Add(new XAttribute("CyclePeriod", (int)this.CyclePeriod.Value));

            if (this.LastMenstruation != null)
                element.Add(new XAttribute("LastMenstruation", MessageHelper.GetAttributeValue(this.LastMenstruation.Value)));

            if (this.DiuresisMl != null)
                element.Add(new XAttribute("DiuresisMl", this.DiuresisMl.Value));

            if (this.WeightKg != null)
                element.Add(new XAttribute("WeightKg", this.WeightKg.Value));

            if (this.HeightCm != null)
                element.Add(new XAttribute("HeightCm", this.HeightCm.Value));

            if (this.UseUserFields)
                foreach (var userField in this.UserFields)
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

            element.Add(new XAttribute("Date", MessageHelper.GetAttributeValue(this.Date)));

            if (this.LisId != null)
                element.Add(new XAttribute("LisId", this.LisId.Value));

            if (this.MasterLisId != null)
                element.Add(new XAttribute("MasterLisId", this.MasterLisId.Value));

            if (this.SamplingDate != null)
                element.Add(new XAttribute("SamplingDate", MessageHelper.GetAttributeValue(this.SamplingDate.Value)));

            if (this.DeliveryDate != null)
                element.Add(new XAttribute("DeliveryDate", MessageHelper.GetAttributeValue(this.DeliveryDate.Value)));

            if (this.Orders.Any())
                element.Add(new XElement("Orders", this.Orders.Select(x => x.ToXMLElement())));

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
            var element = new XElement("Item", new XAttribute("Code", MessageHelper.GetAttributeValue(this.Code)));

            if (this.BiomaterialCode != null)
                element.Add(new XAttribute("BiomaterialCode", this.BiomaterialCode));

            if (this.PayType != null)
                element.Add(new XAttribute("PayType", this.PayType));
            
            if(this.DepartmentName != null)
                element.Add(new XAttribute("DepartmentName", this.DepartmentName));

            if(this.DepartmentCode != null)
                element.Add(new XAttribute("DepartmentCode", this.DepartmentCode));
            
            if (this.DoctorName != null)
                element.Add(new XAttribute("DoctorName", this.DoctorName));

            if(this.DoctorCode != null)
                element.Add(new XAttribute("DoctorCode", this.DoctorCode));

            if(this.DiagnosisName != null)
                element.Add(new XAttribute("DiagnosisName", this.DiagnosisName));
            
            if(this.DiagnosisCode != null)
                element.Add(new XAttribute("DiagnosisCode", this.DiagnosisCode));
            
            if(this.CardNr != null)
                element.Add(new XAttribute("CardNr", this.CardNr));

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

            if (this.Barcode != null)
                element.Add(new XAttribute("Barcode", this.Barcode));

            if (this.BiomaterialCode != null)
                element.Add(new XAttribute("BiomaterialCode", this.BiomaterialCode));

            if (this.Orders.Any())
                element.Add(new XElement("Orders", this.Orders.Select(x => x.ToXMLElement())));

            return element;
        }
    }
}

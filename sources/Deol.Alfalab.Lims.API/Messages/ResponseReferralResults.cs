using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseReferralResults : ResponseMessage
    {
        public ResponseElementVersion Version { get; private set; }

        public ResponseElementPatient Patient { get; private set; }

        public ResponseElementReferral Referral { get; private set; }

        public IEnumerable<ResponseElementBlank> Blanks { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            Version = MessageHelper.GetResponseMessageElement<ResponseElementVersion>(message.Element("Version"));

            Patient = MessageHelper.GetResponseMessageElement<ResponseElementPatient>(message.Element("Patient"));

            Referral = MessageHelper.GetResponseMessageElement<ResponseElementReferral>(message.Element("Referral"));

            Blanks = MessageHelper.GetResponseMessageElements<ResponseElementBlank>(message.Element("Blanks"));
        }
    }

    public class ResponseElementPatient : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Code1 { get; private set; }
        public string Code2 { get; private set; }

        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }

        public PationGender? Gender { get; private set; }

        public DateTime? BirthDate { get; private set; }

        public int? BirthYear { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            MisId      = element.Attribute("MisId")?.Value;
            Code1      = element.Attribute("Code1")?.Value;
            Code2      = element.Attribute("Code2")?.Value;
            LastName   = element.Attribute("LastName")?.Value;
            FirstName  = element.Attribute("FirstName")?.Value;
            MiddleName = element.Attribute("MiddleName")?.Value;

            if (int.TryParse(element.Attribute("Gender")?.Value, out var gender))
                Gender = IntToGender(gender);

            if (DateTime.TryParseExact(element.Attribute("BirthDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
                BirthDate = birthDate;

            if (int.TryParse(element.Attribute("BirthYear")?.Value, out var birthYear))
                BirthYear = birthYear;

            PationGender IntToGender(int gen)
            {
                switch(gen)
                {
                    case 0:  return PationGender.NotSet;
                    case 1:  return PationGender.Male;
                    case 2:  return PationGender.Female;
                    default: return PationGender.NotSet;
                }
            }
        }
    }

    public enum ReferralActivated
    {
        No = 1,
        Partial = 2,
        Full = 3
    }

    public class ResponseElementReferral : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }
        public int LisId { get; private set; }
        public int? MasterLisId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime? SamplingDate { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public bool Removed { get; private set; }
        public bool Done { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? DoneDate { get; private set; }
        public DateTime? MinPlanDate { get; private set; }
        public DateTime? MaxPlanDate { get; private set; }
        public ReferralActivated Activated { get; private set; }
        public bool Manual { get; private set; }

        public IEnumerable<ResponseElementReferralOrder> Orders { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            MisId = element.Attribute("MisId")?.Value;
            
            Nr = element.Attribute("Nr")?.Value;
            
            if (int.TryParse(element.Attribute("LisId")?.Value, out var lisId))
                LisId = lisId;

            if (int.TryParse(element.Attribute("MasterLisId")?.Value, out var masterLisId))
                MasterLisId = masterLisId;

            if (DateTime.TryParseExact(element.Attribute("Date")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                Date = date;

            if (DateTime.TryParseExact(element.Attribute("SamplingDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var samplingeDate))
                SamplingDate = samplingeDate;

            if (DateTime.TryParseExact(element.Attribute("DeliveryDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var deliveryDate))
                DeliveryDate = deliveryDate;

            Removed = Convert.ToBoolean(element.Attribute("Removed")?.Value);
            
            Done = Convert.ToBoolean(element.Attribute("Done")?.Value);

            if (DateTime.TryParseExact(element.Attribute("CreateDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var createDate))
                CreateDate = createDate;

            if (DateTime.TryParseExact(element.Attribute("DoneDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var doneDate))
                DoneDate = doneDate;

            if (DateTime.TryParseExact(element.Attribute("MinPlanDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var minPlanDate))
                MinPlanDate = minPlanDate;

            if (DateTime.TryParseExact(element.Attribute("MaxPlanDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var maxPlanDate))
                MaxPlanDate = maxPlanDate;

            if (int.TryParse(element.Attribute("Activated")?.Value, out var activated))
                Activated = IntToReferralActivated(activated);

            ReferralActivated IntToReferralActivated(int activatedInt)
            {
                switch (activatedInt)
                {
                    case 1: return ReferralActivated.No;
                    case 2: return ReferralActivated.Partial;
                    case 3: return ReferralActivated.Full;
                    default: return ReferralActivated.No;
                }
            }

            Manual = Convert.ToBoolean(element.Attribute("Manual")?.Value);

            Orders = MessageHelper.GetResponseMessageElements<ResponseElementReferralOrder>(element.Element("Orders"));
        }
    }

    public enum ReferralOrderState
    {
        New = 1,
        Done = 3,
        Approved = 4,
        Cancel = 5
    }

    public class ResponseElementReferralOrder : IResponseMessageElement
    {
        public string Code { get; private set; }
        public string BiomaterialCode { get; private set; }
        public ReferralOrderState State { get; private set; }
        public string PlaceCode { get; private set; }
        public bool Defected { get; private set; }
        public string Defects { get; private set; }
        public string DefectCode { get; private set; }
        public string MisId { get; private set; }

        //В протоколе DoneDate. По факту, при стате 3 (Done) - это DoneDate, при статусе 4 (Approved) - это ApproveDate. Использовано название DoneOrApproveDate
        public DateTime DoneOrApproveDate { get; private set; }
        public string DoctorName { get; private set; }
        public string DoctorCode { get; private set; }
        public string DoctorProf { get; private set; }
        public string DoctorSnils { get; private set; }
        public string DoctorExportCode { get; private set; }
        public string DoctorExportCode2 { get; private set; }
        public string DoctorRoleCode { get; private set; }
        public string DoctorSpecialityCode { get; private set; }
        public string DoctorHospitalExportCode { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            Code = element.Attribute("Code")?.Value;
            BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;

            if (int.TryParse(element.Attribute("State")?.Value, out var state))
                State = IntToReferralOrderState(state);

            ReferralOrderState IntToReferralOrderState(int stateInt)
            {
                switch (stateInt)
                {
                    case 1: return ReferralOrderState.New;
                    case 3: return ReferralOrderState.Done;
                    case 4: return ReferralOrderState.Approved;
                    case 5: return ReferralOrderState.Cancel;
                    default: return ReferralOrderState.New;
                }
            }

            PlaceCode = element.Attribute("PlaceCode")?.Value;
            Defected = Convert.ToBoolean(element.Attribute("Defected")?.Value);
            Defects = element.Attribute("Defects")?.Value;
            DefectCode = element.Attribute("DefectCode")?.Value;
            MisId = element.Attribute("MisId")?.Value;

            if (DateTime.TryParseExact(element.Attribute("DoneDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var doneDate))
                DoneOrApproveDate = doneDate;

            DoctorName = element.Attribute("DoctorName")?.Value;
            DoctorCode = element.Attribute("DoctorCode")?.Value;
            DoctorProf = element.Attribute("DoctorProf")?.Value;
            DoctorSnils = element.Attribute("DoctorSnils")?.Value;
            DoctorExportCode = element.Attribute("DoctorExportCode")?.Value;
            DoctorExportCode2 = element.Attribute("DoctorExportCode2")?.Value;
            DoctorRoleCode = element.Attribute("DoctorRoleCode")?.Value;
            DoctorSpecialityCode = element.Attribute("DoctorSpecialityCode")?.Value;
            DoctorHospitalExportCode = element.Attribute("DoctorHospitalExportCode")?.Value;
        }
    }

    public enum BlankType
    {
        Results = 1,
        DefectMessage = 2,
        ExternalDocument = 3
    }

    public class ResponseElementBlank : IResponseMessageElement
    {
        public int BlankId { get; private set; }
        public string BlankGUID { get; private set; }
        public int Version { get; private set; }
        public string Name { get; private set; }
        public string Groups { get; private set; }
        public bool Done { get; private set; }
        public DateTime? DoneDate { get; private set; }
        public string Comment { get; private set; }
        public DateTime? MinPlanDate { get; private set; }
        public DateTime? MaxPlanDate { get; private set; }
        public string FileName { get; private set; }

        public IEnumerable<ResponseElementBlankTest> Tests { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("BlankId")?.Value, out var blankId))
                BlankId = blankId;

            BlankGUID = element.Attribute("BlankGUID")?.Value;

            if (int.TryParse(element.Attribute("Version")?.Value, out var version))
                Version = version;

            Name = element.Attribute("Name")?.Value;
            Groups = element.Attribute("Groups")?.Value;
            Done = Convert.ToBoolean(element.Attribute("Done")?.Value);

            if (DateTime.TryParseExact(element.Attribute("DoneDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var doneDate))
                DoneDate = doneDate;

            Comment = element.Attribute("Comment")?.Value;

            if (DateTime.TryParseExact(element.Attribute("MinPlanDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var minPlaneDate))
                MinPlanDate = minPlaneDate;

            if (DateTime.TryParseExact(element.Attribute("MaxPlanDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var maxPlaneDate))
                MaxPlanDate = maxPlaneDate;

            FileName = element.Attribute("FileName")?.Value;

            Tests = MessageHelper.GetResponseMessageElements<ResponseElementBlankTest>(element.Element("Tests"));
        }
    }

    public enum BlankTestValueType
    {
        NumberOrString = 1,
        FromList = 2,
        Image = 3,
        Document = 4
    }

    public enum BlankTestNormsFlag
    {
        NoData = 0,
        Norm = 1,
        NotNorm = 2,
        BelowCriticalNorm = 3,
        BelowNormal = 4,
        AboveNormal = 6,
        AboveCriticalNorm = 7
    }

    public class ResponseElementBlankTest : IResponseMessageElement
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string GroupName { get; private set; }
        public string GroupCode { get; private set; }
        public int GroupRank { get; private set; }
        public int TestGroupRank { get; private set; }
        public string OrderCode { get; private set; }
        public string BiomaterialCode { get; private set; }
        public string Barcode { get; private set; }
        public string Value { get; private set; }
        public DateTime ValueDate { get; private set; }
        public bool Cancelled { get; private set; }
        public BlankTestValueType ValueType { get; private set; }
        public string BinaryDataFormat { get; private set; }
        public string BinaryDataBase64 { get; private set; }
        public bool IsMicro { get; private set; }
        public string UnitName { get; private set; }
        public bool Defected { get; private set; }
        public string Defects { get; private set; }
        public string DefectCode { get; private set; }
        public string Comment { get; private set; }
        public BlankTestNormsFlag NormsFlag { get; private set; }
        public string NormsComment { get; private set; }
        public string Norms { get; private set; }
        public float NormPoint1 { get; private set; }
        public float NormPoint2 { get; private set; }
        public float NormPoint3 { get; private set; }
        public float NormPoint4 { get; private set; }
        public string Source { get; private set; }
        public string DoctorName { get; private set; }
        public string DoctorCode { get; private set; }
        public string DoctorProf { get; private set; }
        public string DoctorSnils { get; private set; }
        public string DoctorExportCode { get; private set; }
        public string DoctorExportCode2 { get; private set; }
        public string DoctorRoleCode { get; private set; }
        public string DoctorSpecialityCode { get; private set; }
        public string DoctorHospitalExportCode { get; private set; }
        public string AssistantName { get; private set; }
        public string AssistantCode { get; private set; }
        public string AssistantProf { get; private set; }

        public IEnumerable<ResponseElementBlankMicroorganism> Microorganisms { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            Name = element.Attribute("Name")?.Value;
            Code = element.Attribute("Code")?.Value;
            GroupName = element.Attribute("GroupName")?.Value;
            GroupCode = element.Attribute("GroupCode")?.Value;
            
            if (int.TryParse(element.Attribute("GroupRank")?.Value, out var groupRank))
                GroupRank = groupRank;

            if (int.TryParse(element.Attribute("TestGroupRank")?.Value, out var testGroupRank))
                TestGroupRank = testGroupRank;

            OrderCode = element.Attribute("OrderCode")?.Value;
            BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;
            Barcode = element.Attribute("Barcode")?.Value;
            Value = element.Attribute("Value")?.Value;
            
            if (DateTime.TryParseExact(element.Attribute("ValueDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var valueDate))
                ValueDate = valueDate;

            Cancelled = Convert.ToBoolean(element.Attribute("Cancelled")?.Value);

            if (int.TryParse(element.Attribute("ValueType")?.Value, out var valueType))
                ValueType = IntToBlankTestValueType(valueType);

            BlankTestValueType IntToBlankTestValueType(int valueTypeInt)
            {
                switch(valueTypeInt)
                {
                    case 1: return BlankTestValueType.NumberOrString;
                    case 2: return BlankTestValueType.FromList;
                    case 3: return BlankTestValueType.Image;
                    case 4: return BlankTestValueType.Document;
                    default: return BlankTestValueType.NumberOrString;
                }
            }

            BinaryDataFormat = element.Attribute("BinaryDataFormat")?.Value;
            BinaryDataBase64 = element.Attribute("BinaryDataBase64")?.Value;
            IsMicro = Convert.ToBoolean(element.Attribute("IsMicro")?.Value);
            UnitName = element.Attribute("UnitName")?.Value;
            Defected = Convert.ToBoolean(element.Attribute("Defected")?.Value);
            Defects = element.Attribute("Defects")?.Value;
            DefectCode = element.Attribute("DefectCode")?.Value;
            Comment = element.Attribute("Comment")?.Value;

            if (int.TryParse(element.Attribute("NormsFlag")?.Value, out var normsFlag))
                NormsFlag = IntToNormsFlag(normsFlag);

            BlankTestNormsFlag IntToNormsFlag(int normsFlagInt)
            {
                switch(normsFlagInt)
                {
                    case 0: return BlankTestNormsFlag.NoData;
                    case 1: return BlankTestNormsFlag.Norm;
                    case 2: return BlankTestNormsFlag.NotNorm;
                    case 3: return BlankTestNormsFlag.BelowCriticalNorm;
                    case 4: return BlankTestNormsFlag.BelowNormal;
                    case 6: return BlankTestNormsFlag.AboveNormal;
                    case 7: return BlankTestNormsFlag.AboveCriticalNorm;
                    default: return BlankTestNormsFlag.NoData;
                }
            }

            NormsComment = element.Attribute("NormsComment")?.Value;
            Norms = element.Attribute("Norms")?.Value;
            
            if (float.TryParse(element.Attribute("NormPoint1")?.Value, out var normPoint1))
                NormPoint1 = normPoint1;

            if (float.TryParse(element.Attribute("NormPoint2")?.Value, out var normPoint2))
                NormPoint2 = normPoint2;

            if (float.TryParse(element.Attribute("NormPoint3")?.Value, out var normPoint3))
                NormPoint3 = normPoint3;

            if (float.TryParse(element.Attribute("NormPoint4")?.Value, out var normPoint4))
                NormPoint4 = normPoint4;

            Source = element.Attribute("Source")?.Value;
            DoctorName = element.Attribute("DoctorName")?.Value;
            DoctorCode = element.Attribute("DoctorCode")?.Value;
            DoctorProf = element.Attribute("DoctorProf")?.Value;
            DoctorSnils = element.Attribute("DoctorSnils")?.Value;
            DoctorExportCode = element.Attribute("DoctorExportCode")?.Value;
            DoctorExportCode2 = element.Attribute("DoctorExportCode2")?.Value;
            DoctorRoleCode = element.Attribute("DoctorRoleCode")?.Value;
            DoctorSpecialityCode = element.Attribute("DoctorSpecialityCode")?.Value;
            DoctorHospitalExportCode = element.Attribute("DoctorHospitalExportCode")?.Value;
            AssistantName = element.Attribute("AssistantName")?.Value;
            AssistantCode = element.Attribute("AssistantCode")?.Value;
            AssistantProf = element.Attribute("AssistantProf")?.Value;

            Microorganisms = MessageHelper.GetResponseMessageElements<ResponseElementBlankMicroorganism>(element.Element("Microorganisms"));
        }
    }

    public class ResponseElementBlankMicroorganism : IResponseMessageElement
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool Found { get; private set; }
        public string Value { get; private set; }
        public string UnitName { get; private set; }

        public IEnumerable<ResponseElementBlankDrug> Drugs { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            Name = element.Attribute("Name")?.Value;
            Code = element.Attribute("Code")?.Value;
            Found = Convert.ToBoolean(element.Attribute("Found")?.Value);
            Value = element.Attribute("Value")?.Value;
            UnitName = element.Attribute("UnitName")?.Value;

            Drugs = MessageHelper.GetResponseMessageElements<ResponseElementBlankDrug>(element.Element("Drugs"));
        }
    }

    public class ResponseElementBlankDrug : IResponseMessageElement
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Value { get; private set; }
        public DateTime ValueDate { get; private set; }
        public string MIC { get; private set; }
        public string Diameter { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            Name = element.Attribute("Name")?.Value;
            Code = element.Attribute("Code")?.Value;
            Value = element.Attribute("Value")?.Value;

            if (DateTime.TryParseExact(element.Attribute("ValueDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var valueDate))
                ValueDate = valueDate;

            MIC = element.Attribute("MIC")?.Value;
            Diameter = element.Attribute("Diameter")?.Value;
        }
    }
}

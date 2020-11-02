using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseDictionaries : ResponseMessage
    {
        public ResponseElementVersion Version { get; private set; }

        public IEnumerable<ResponseElementDictionaryAnalysisGroup> AnalysisGroups { get; private set; }
        public IEnumerable<ResponseElementDictionaryContainerType> ContainerTypes { get; private set; }
        public IEnumerable<ResponseElementDictionaryPreanalyticInfo> PreanalyticInfos { get; private set; }
        public IEnumerable<ResponseElementDictionaryBiomaterial> Biomaterials { get; private set; }
        public IEnumerable<ResponseElementDictionaryTest> Tests { get; private set; }
        public IEnumerable<ResponseElementDictionaryMicroorganism> Microorganisms { get; private set; }
        public IEnumerable<ResponseElementDictionaryDrug> Drugs { get; private set; }
        public IEnumerable<ResponseElementDictionaryField> Fields { get; private set; }
        public IEnumerable<ResponseElementDictionaryAnalysis> Analyses { get; private set; }
        public IEnumerable<ResponseElementDictionaryPanel> Panels { get; private set; }
        public IEnumerable<ResponseElementDictionaryPrice> Prices { get; private set; }
        public IEnumerable<ResponseElementDictionaryDelayedTarget> DelayedTargets { get; private set; }
        protected override void InitMessageElements(XElement message)
        {
            this.Version            = MessageHelper.GetResponseMessageElement<ResponseElementVersion>(message.Element("Version"));

            this.AnalysisGroups     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisGroup>(message.Element("AnalysisGroups"));

            this.ContainerTypes     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryContainerType>(message.Element("ContainerTypes"));

            this.PreanalyticInfos   = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPreanalyticInfo>(message.Element("PreanalyticInfos"));

            this.Biomaterials       = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryBiomaterial>(message.Element("Biomaterials"));

            this.Tests              = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryTest>(message.Element("Tests"));

            this.Microorganisms     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryMicroorganism>(message.Element("Microorganisms"));

            this.Drugs              = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryDrug>(message.Element("Drugs"));

            this.Fields             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryField>(message.Element("Fields"));

            this.Analyses           = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysis>(message.Element("Analyses"));

            this.Panels             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPanel>(message.Element("Panels"));

            this.Prices             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPrice>(message.Element("Prices"));

            this.DelayedTargets     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryDelayedTarget>(message.Element("DelayedTargets"));
        }
    }

    public class ResponseElementDictionaryItem : IResponseMessageElement
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public int UpdateVersion { get; private set; }
        public bool Removed { get; private set; }

        public virtual void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("Id")?.Value, out var id))
                this.Id = id;

            this.Name = element.Attribute("Name")?.Value;
            this.Code = element.Attribute("Code")?.Value;

            if (DateTime.TryParseExact(element.Attribute("UpdateTime")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var updateTime))
                this.UpdateTime = updateTime;

            if (int.TryParse(element.Attribute("UpdateVersion")?.Value, out var updateVersion))
                this.UpdateVersion = updateVersion;

            this.Removed = Convert.ToBoolean(element.Attribute("Removed")?.Value);
        }
    }

    public class ResponseElementDictionaryAnalysisGroup : ResponseElementDictionaryItem
    {
    }

    public class ResponseElementDictionaryContainerType : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public int ImageIndex { get; private set; }
        public bool? NeedCount { get; private set; }
        public int? MaxTestsCount { get; private set; }
        public bool? SkipLabel { get; private set; }
        public string Description { get; private set; }
        public string Extra1 { get; private set; }
        public string Extra2 { get; private set; }
        public string Extra3 { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ImageIndex")?.Value, out var imageIndex))
                this.ImageIndex = imageIndex;

            if (bool.TryParse(element.Attribute("NeedCount")?.Value, out var needCount))
                this.NeedCount = needCount;

            if (int.TryParse(element.Attribute("MaxTestsCount")?.Value, out var maxTestsCount))
                this.MaxTestsCount = maxTestsCount;

            if (bool.TryParse(element.Attribute("SkipLabel")?.Value, out var skipLabel))
                this.SkipLabel = skipLabel;

            this.Description = element.Attribute("Description")?.Value;
            this.Extra1 = element.Attribute("Extra1")?.Value;
            this.Extra2 = element.Attribute("Extra2")?.Value;
            this.Extra3 = element.Attribute("Extra3")?.Value;
        }
    }

    public class ResponseElementDictionaryPreanalyticInfo : ResponseElementDictionaryItem 
    {
        public string ShortName { get; private set; }
        public int ImageIndex { get; private set; }
        public string Description { get; private set; }
        public string Extra1 { get; private set; }
        public string Extra2 { get; private set; }
        public string Extra3 { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ImageIndex")?.Value, out var imageIndex))
                this.ImageIndex = imageIndex;

            this.Description = element.Attribute("Description")?.Value;
            this.Extra1 = element.Attribute("Extra1")?.Value;
            this.Extra2 = element.Attribute("Extra2")?.Value;
            this.Extra3 = element.Attribute("Extra3")?.Value;
        }
    }

    public class ResponseElementDictionaryBiomaterial : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public string ContainerTypeCode { get; private set; }
        public int? ContainerTypeId { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;
            this.ContainerTypeCode = element.Attribute("ContainerTypeCode")?.Value;

            if (int.TryParse(element.Attribute("ContainerTypeId")?.Value, out var containerTypeId))
                this.ContainerTypeId = containerTypeId;
        }
    }

    public enum DictionaryTestType
    {
        NoData = 0,
        Number = 1,
        String = 2,
        Conclusion = 3,
        IdentificationMicroorganism = 4,
        Image = 5,
        Document = 6
    }

    public class ResponseElementDictionaryTest : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public DictionaryTestType ResultType { get; private set; }
        public string UnitName { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ResultType")?.Value, out var resultType))
                this.ResultType = IntToDictionaryTestType(resultType);

            this.UnitName = element.Attribute("UnitName")?.Value;

            DictionaryTestType IntToDictionaryTestType(int testTypeInt)
            {
                switch(testTypeInt)
                {
                    case 0: return DictionaryTestType.NoData;
                    case 1: return DictionaryTestType.Number;
                    case 2: return DictionaryTestType.String;
                    case 3: return DictionaryTestType.Conclusion;
                    case 4: return DictionaryTestType.IdentificationMicroorganism;
                    case 5: return DictionaryTestType.Image;
                    case 6: return DictionaryTestType.Document;
                    default: return DictionaryTestType.NoData;
                }
            }
        }
    }

    public class ResponseElementDictionaryMicroorganism : ResponseElementDictionaryItem
    {
        public string Name2 { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.Name2 = element.Attribute("Name2")?.Value;
        }
    }

    public class ResponseElementDictionaryDrug : ResponseElementDictionaryItem
    {
        public string Name2 { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.Name2 = element.Attribute("Name2")?.Value;
        }
    }

    public enum FieldType
    {
        String = 1,
        Number = 2,
        Boolean = 3,
        DateTime = 4,
        List = 5,
        Gender = 6
    }

    public class ResponseElementDictionaryField : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public FieldType FieldType { get; private set; }
        public int? MaxLength { get; private set; }
        public float? MinValue { get; private set; }
        public float? MaxValue { get; private set; }
        public int? Precision { get; private set; }
        public bool? NeedTime { get; private set; }
        public string DictionaryCode { get; private set; }
        public bool? DictionaryAllowCreate { get; private set; }

        public IEnumerable<ResponseElementDictionaryValue> DictionaryValues { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("FieldType")?.Value, out var fieldType))
                this.FieldType = IntToFieldType(fieldType);

            if (int.TryParse(element.Attribute("MaxLength")?.Value, out var maxLength))
                this.MaxLength = maxLength;

            if (float.TryParse(element.Attribute("MinValue")?.Value, out var minValue))
                this.MinValue = minValue;

            if (float.TryParse(element.Attribute("MaxValue")?.Value, out var maxValue))
                this.MaxValue = maxValue;

            if (int.TryParse(element.Attribute("Precision")?.Value, out var precision))
                this.Precision = precision;

            if (bool.TryParse(element.Attribute("NeedTime")?.Value, out var needTime))
                this.NeedTime = needTime;

            this.DictionaryCode = element.Attribute("DictionaryCode")?.Value;

            if (bool.TryParse(element.Attribute("DictionaryAllowCreate")?.Value, out var dictionaryAllowCreate))
                this.DictionaryAllowCreate = dictionaryAllowCreate;

            this.DictionaryValues = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryValue>(element.Element("DictionaryValues"));

            FieldType IntToFieldType(int valueInt)
            {
                switch(valueInt)
                {
                    case 1: return FieldType.String;
                    case 2: return FieldType.Number;
                    case 3: return FieldType.Boolean;
                    case 4: return FieldType.DateTime;
                    case 5: return FieldType.List;
                    case 6: return FieldType.Gender;
                    default: return FieldType.String;
                }
            }
        }
    }

    public class ResponseElementDictionaryValue : IResponseMessageElement
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortName { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.Name = element.Attribute("Name")?.Value;
            this.Code = element.Attribute("Code")?.Value;
            this.ShortName = element.Attribute("ShortName")?.Value;
        }
    }

    public class ResponseElementDictionaryAnalysis : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public string AnalysisGroupCode { get; private set; }
        public int? AnalysisGroupId { get; private set; }
        public bool? NeedPregnancyInfo { get; private set; }
        public bool? PregnancyInfoRequired { get; private set; }

        public IEnumerable<ResponseElementDictionaryAnalysisBiomaterial> AnalysesBiomaterials { get; private set; }
        public IEnumerable<ResponseElementDictionaryAnalysisTest> AnalysesTests { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;
            this.AnalysisGroupCode = element.Attribute("AnalysisGroupCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisGroupId")?.Value, out var analysisGroupId))
                this.AnalysisGroupId = analysisGroupId;

            if (bool.TryParse(element.Attribute("NeedPregnancyInfo")?.Value, out var needPregnancyInfo))
                this.NeedPregnancyInfo = needPregnancyInfo;

            if (bool.TryParse(element.Attribute("PregnancyInfoRequired")?.Value, out var pregnancyInfoRequired))
                this.PregnancyInfoRequired = pregnancyInfoRequired;

            this.AnalysesBiomaterials   = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisBiomaterial>(element.Element("AnalysisBiomaterials"));

            this.AnalysesTests          = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisTest>(element.Element("AnalysisTests"));
        }
    }

    public class ResponseElementDictionaryAnalysisBiomaterial : IResponseMessageElement
    {
        public string BiomaterialCode { get; private set; }
        public int BiomaterialId { get; private set; }
        public int SamplingGroupId { get; private set; }
        public string SamplingGroupName { get; private set; }
        public string SamplingGroupCode { get; private set; }
        public int? MaxTestsCount { get; private set; }
        public int ContainerTypeId { get; private set; }
        public string ContainerTypeCode { get; private set; }
        public int ContainersCount { get; private set; } = 1;
        public bool? IsDefault { get; private set; }
        public int? PreanalyticInfoId { get; private set; }
        public string PreanalyticInfoName { get; private set; }
        public string PreanalyticInfoShortName { get; private set; }
        public string PreanalyticInfoDescription { get; private set; }
        public int? PreanalyticInfoImageIndex { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;

            if (int.TryParse(element.Attribute("BiomaterialId")?.Value, out var biomaterialId))
                this.BiomaterialId = biomaterialId;

            if (int.TryParse(element.Attribute("SamplingGroupId")?.Value, out var samplingGroupId))
                this.SamplingGroupId = samplingGroupId;

            this.SamplingGroupName = element.Attribute("SamplingGroupName")?.Value;
            this.SamplingGroupCode = element.Attribute("SamplingGroupCode")?.Value;

            if (int.TryParse(element.Attribute("MaxTestsCount")?.Value, out var maxTestsCount))
                this.MaxTestsCount = maxTestsCount;

            if (int.TryParse(element.Attribute("ContainerTypeId")?.Value, out var сontainerTypeId))
                this.ContainerTypeId = сontainerTypeId;

            this.ContainerTypeCode = element.Attribute("ContainerTypeCode")?.Value;

            if (int.TryParse(element.Attribute("ContainersCount")?.Value, out var сontainersCount))
                this.ContainersCount = сontainersCount;

            if (bool.TryParse(element.Attribute("IsDefault")?.Value, out var isDefault))
                this.IsDefault = isDefault;

            if (int.TryParse(element.Attribute("PreanalyticInfoId")?.Value, out var preanalyticInfoId))
                this.PreanalyticInfoId = preanalyticInfoId;

            this.PreanalyticInfoName = element.Attribute("PreanalyticInfoName")?.Value;
            this.PreanalyticInfoShortName = element.Attribute("PreanalyticInfoShortName")?.Value;
            this.PreanalyticInfoDescription = element.Attribute("PreanalyticInfoDescription")?.Value;

            if (int.TryParse(element.Attribute("PreanalyticInfoImageIndex")?.Value, out var preanalyticInfoImageIndex))
                this.PreanalyticInfoImageIndex = preanalyticInfoImageIndex;
        }
    }

    public class ResponseElementDictionaryAnalysisTest : IResponseMessageElement
    {
        public string TestCode { get; private set; }
        public int TestId { get; private set; }
        public bool Mandatory { get; set; }

        public void InitFromXMLElement(XElement element)
        {
            this.TestCode = element.Attribute("TestCode")?.Value;

            if (int.TryParse(element.Attribute("TestId")?.Value, out var testId))
                this.TestId = testId;

            this.Mandatory = Convert.ToBoolean(element.Attribute("Mandatory")?.Value);
        }
    }

    public class ResponseElementDictionaryPanel : ResponseElementDictionaryItem
    {
        public string ShortName { get; private set; }
        public string AnalysisGroupCode { get; private set; }
        public int AnalysisGroupId { get; private set; }
        public bool? NeedPregnancyInfo { get; private set; }
        public bool? PregnancyInfoRequired { get; private set; }

        public IEnumerable<ResponseElementDictionaryPanelAnalysis> PanelAnalyses { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            this.ShortName = element.Attribute("ShortName")?.Value;
            this.AnalysisGroupCode = element.Attribute("AnalysisGroupCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisGroupId")?.Value, out var analysisGroupId))
                this.AnalysisGroupId = analysisGroupId;

            if (bool.TryParse(element.Attribute("NeedPregnancyInfo")?.Value, out var needPregnancyInfo))
                this.NeedPregnancyInfo = needPregnancyInfo;

            if (bool.TryParse(element.Attribute("PregnancyInfoRequired")?.Value, out var pregnancyInfoRequired))
                this.PregnancyInfoRequired = pregnancyInfoRequired;

            this.PanelAnalyses = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPanelAnalysis>(element.Element("PanelAnalyses"));
        }
    }

    public class ResponseElementDictionaryPanelAnalysis : IResponseMessageElement
    {
        public string AnalysisCode { get; private set; }
        public int AnalysisId { get; private set; }
        public string BiomaterialCode { get; private set; }
        public int BiomaterialId { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.AnalysisCode = element.Attribute("AnalysisCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisId")?.Value, out var analysisId))
                this.AnalysisId = analysisId;

            this.BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;

            if (int.TryParse(element.Attribute("BiomaterialId")?.Value, out var biomaterialId))
                this.BiomaterialId = biomaterialId;
        }
    }

    public enum DurationUnit
    {
        Hours = 1,
        Days = 2
    }

    public class ResponseElementDictionaryPrice : IResponseMessageElement
    {
        public int ServiceId { get; private set; }
        public string ServiceCode { get; private set; }
        public float Price { get; private set; }
        public string Extra1 { get; private set; }
        public int? MinDuration { get; private set; }
        public int? MaxDuration { get; private set; }
        public DurationUnit? DurationUnit { get; private set; }
        public bool? AllowCito { get; private set; }
        public float? CitoPrice { get; private set; }
        public int? CitoMinDuration { get; private set; }
        public int? CitoMaxDuration { get; private set; }
        public DurationUnit? CitoDurationUnit { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("ServiceId")?.Value, out var serviceId))
                this.ServiceId = serviceId;

            this.ServiceCode = element.Attribute("ServiceCode")?.Value;

            if (float.TryParse(element.Attribute("Price")?.Value, out var price))
                this.Price = price;

            this.Extra1 = element.Attribute("Extra1")?.Value;

            if (int.TryParse(element.Attribute("MinDuration")?.Value, out var minDuration))
                this.MinDuration = minDuration;

            if (int.TryParse(element.Attribute("MaxDuration")?.Value, out var maxDuration))
                this.MaxDuration = maxDuration;

            if (int.TryParse(element.Attribute("DurationUnit")?.Value, out var durationUnitInt))
                this.DurationUnit = IntToDurationUnit(durationUnitInt);

            if (bool.TryParse(element.Attribute("AllowCito")?.Value, out var allowCito))
                this.AllowCito = allowCito;

            if (float.TryParse(element.Attribute("CitoPrice")?.Value, out var citoPrice))
                this.CitoPrice = citoPrice;

            if (int.TryParse(element.Attribute("CitoMinDuration")?.Value, out var citoMinDuration))
                this.CitoMinDuration = citoMinDuration;

            if (int.TryParse(element.Attribute("CitoMaxDuration")?.Value, out var citoMaxDuration))
                this.CitoMaxDuration = citoMaxDuration;

            if (int.TryParse(element.Attribute("CitoDurationUnit")?.Value, out var citoDurationUnit))
                this.CitoDurationUnit = IntToDurationUnit(citoDurationUnit);

            DurationUnit IntToDurationUnit(int valueInt)
            {
                switch(valueInt)
                {
                    case 1: return Messages.DurationUnit.Hours;
                    case 2: return Messages.DurationUnit.Days;
                    default: return Messages.DurationUnit.Hours;
                }
            }
        }
    }

    public class ResponseElementDictionaryDelayedTarget : IResponseMessageElement
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int TargetId { get; private set; }
        public string TargetCode { get; private set; }
        public string ReasonName { get; private set; }
        public string Comment { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (DateTime.TryParseExact(element.Attribute("StartDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
                this.StartDate = startDate;

            if (DateTime.TryParseExact(element.Attribute("EndDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
                this.EndDate = endDate;

            if (int.TryParse(element.Attribute("TargetId")?.Value, out var targetId))
                this.TargetId = targetId;

            this.TargetCode = element.Attribute("TargetCode")?.Value;
            this.ReasonName = element.Attribute("ReasonName")?.Value;
            this.Comment = element.Attribute("Comment")?.Value;
        }
    }
}

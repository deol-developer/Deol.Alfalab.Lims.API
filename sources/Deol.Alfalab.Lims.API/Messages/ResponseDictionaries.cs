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
            Version            = MessageHelper.GetResponseMessageElement<ResponseElementVersion>(message.Element("Version"));

            AnalysisGroups     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisGroup>(message.Element("AnalysisGroups"));

            ContainerTypes     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryContainerType>(message.Element("ContainerTypes"));

            PreanalyticInfos   = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPreanalyticInfo>(message.Element("PreanalyticInfos"));

            Biomaterials       = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryBiomaterial>(message.Element("Biomaterials"));

            Tests              = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryTest>(message.Element("Tests"));

            Microorganisms     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryMicroorganism>(message.Element("Microorganisms"));

            Drugs              = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryDrug>(message.Element("Drugs"));

            Fields             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryField>(message.Element("Fields"));

            Analyses           = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysis>(message.Element("Analyses"));

            Panels             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPanel>(message.Element("Panels"));

            Prices             = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPrice>(message.Element("Prices"));

            DelayedTargets     = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryDelayedTarget>(message.Element("DelayedTargets"));
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
                Id = id;

            Name = element.Attribute("Name")?.Value;
            Code = element.Attribute("Code")?.Value;

            if (DateTime.TryParseExact(element.Attribute("UpdateTime")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var updateTime))
                UpdateTime = updateTime;

            if (int.TryParse(element.Attribute("UpdateVersion")?.Value, out var updateVersion))
                UpdateVersion = updateVersion;

            Removed = Convert.ToBoolean(element.Attribute("Removed")?.Value);
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

            ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ImageIndex")?.Value, out var imageIndex))
                ImageIndex = imageIndex;

            if (bool.TryParse(element.Attribute("NeedCount")?.Value, out var needCount))
                NeedCount = needCount;

            if (int.TryParse(element.Attribute("MaxTestsCount")?.Value, out var maxTestsCount))
                MaxTestsCount = maxTestsCount;

            if (bool.TryParse(element.Attribute("SkipLabel")?.Value, out var skipLabel))
                SkipLabel = skipLabel;

            Description = element.Attribute("Description")?.Value;
            Extra1 = element.Attribute("Extra1")?.Value;
            Extra2 = element.Attribute("Extra2")?.Value;
            Extra3 = element.Attribute("Extra3")?.Value;
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

            ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ImageIndex")?.Value, out var imageIndex))
                ImageIndex = imageIndex;

            Description = element.Attribute("Description")?.Value;
            Extra1 = element.Attribute("Extra1")?.Value;
            Extra2 = element.Attribute("Extra2")?.Value;
            Extra3 = element.Attribute("Extra3")?.Value;
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

            ShortName = element.Attribute("ShortName")?.Value;
            ContainerTypeCode = element.Attribute("ContainerTypeCode")?.Value;

            if (int.TryParse(element.Attribute("ContainerTypeId")?.Value, out var containerTypeId))
                ContainerTypeId = containerTypeId;
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

            ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("ResultType")?.Value, out var resultType))
                ResultType = IntToDictionaryTestType(resultType);

            UnitName = element.Attribute("UnitName")?.Value;

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

            Name2 = element.Attribute("Name2")?.Value;
        }
    }

    public class ResponseElementDictionaryDrug : ResponseElementDictionaryItem
    {
        public string Name2 { get; private set; }

        public override void InitFromXMLElement(XElement element)
        {
            base.InitFromXMLElement(element);

            Name2 = element.Attribute("Name2")?.Value;
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

            ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("FieldType")?.Value, out var fieldType))
                FieldType = IntToFieldType(fieldType);

            if (int.TryParse(element.Attribute("MaxLength")?.Value, out var maxLength))
                MaxLength = maxLength;

            if (float.TryParse(element.Attribute("MinValue")?.Value, out var minValue))
                MinValue = minValue;

            if (float.TryParse(element.Attribute("MaxValue")?.Value, out var maxValue))
                MaxValue = maxValue;

            if (int.TryParse(element.Attribute("Precision")?.Value, out var precision))
                Precision = precision;

            if (bool.TryParse(element.Attribute("NeedTime")?.Value, out var needTime))
                NeedTime = needTime;

            DictionaryCode = element.Attribute("DictionaryCode")?.Value;

            if (bool.TryParse(element.Attribute("DictionaryAllowCreate")?.Value, out var dictionaryAllowCreate))
                DictionaryAllowCreate = dictionaryAllowCreate;

            DictionaryValues = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryValue>(element.Element("DictionaryValues"));

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
            Name = element.Attribute("Name")?.Value;
            Code = element.Attribute("Code")?.Value;
            ShortName = element.Attribute("ShortName")?.Value;
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

            ShortName = element.Attribute("ShortName")?.Value;
            AnalysisGroupCode = element.Attribute("AnalysisGroupCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisGroupId")?.Value, out var analysisGroupId))
                AnalysisGroupId = analysisGroupId;

            if (bool.TryParse(element.Attribute("NeedPregnancyInfo")?.Value, out var needPregnancyInfo))
                NeedPregnancyInfo = needPregnancyInfo;

            if (bool.TryParse(element.Attribute("PregnancyInfoRequired")?.Value, out var pregnancyInfoRequired))
                PregnancyInfoRequired = pregnancyInfoRequired;

            AnalysesBiomaterials   = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisBiomaterial>(element.Element("AnalysisBiomaterials"));

            AnalysesTests          = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryAnalysisTest>(element.Element("AnalysisTests"));
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
            BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;

            if (int.TryParse(element.Attribute("BiomaterialId")?.Value, out var biomaterialId))
                BiomaterialId = biomaterialId;

            if (int.TryParse(element.Attribute("SamplingGroupId")?.Value, out var samplingGroupId))
                SamplingGroupId = samplingGroupId;

            SamplingGroupName = element.Attribute("SamplingGroupName")?.Value;
            SamplingGroupCode = element.Attribute("SamplingGroupCode")?.Value;

            if (int.TryParse(element.Attribute("MaxTestsCount")?.Value, out var maxTestsCount))
                MaxTestsCount = maxTestsCount;

            if (int.TryParse(element.Attribute("ContainerTypeId")?.Value, out var сontainerTypeId))
                ContainerTypeId = сontainerTypeId;

            ContainerTypeCode = element.Attribute("ContainerTypeCode")?.Value;

            if (int.TryParse(element.Attribute("ContainersCount")?.Value, out var сontainersCount))
                ContainersCount = сontainersCount;

            if (bool.TryParse(element.Attribute("IsDefault")?.Value, out var isDefault))
                IsDefault = isDefault;

            if (int.TryParse(element.Attribute("PreanalyticInfoId")?.Value, out var preanalyticInfoId))
                PreanalyticInfoId = preanalyticInfoId;

            PreanalyticInfoName = element.Attribute("PreanalyticInfoName")?.Value;
            PreanalyticInfoShortName = element.Attribute("PreanalyticInfoShortName")?.Value;
            PreanalyticInfoDescription = element.Attribute("PreanalyticInfoDescription")?.Value;

            if (int.TryParse(element.Attribute("PreanalyticInfoImageIndex")?.Value, out var preanalyticInfoImageIndex))
                PreanalyticInfoImageIndex = preanalyticInfoImageIndex;
        }
    }

    public class ResponseElementDictionaryAnalysisTest : IResponseMessageElement
    {
        public string TestCode { get; private set; }
        public int TestId { get; private set; }
        public bool Mandatory { get; set; }

        public void InitFromXMLElement(XElement element)
        {
            TestCode = element.Attribute("TestCode")?.Value;

            if (int.TryParse(element.Attribute("TestId")?.Value, out var testId))
                TestId = testId;

            Mandatory = Convert.ToBoolean(element.Attribute("Mandatory")?.Value);
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

            ShortName = element.Attribute("ShortName")?.Value;
            AnalysisGroupCode = element.Attribute("AnalysisGroupCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisGroupId")?.Value, out var analysisGroupId))
                AnalysisGroupId = analysisGroupId;

            if (bool.TryParse(element.Attribute("NeedPregnancyInfo")?.Value, out var needPregnancyInfo))
                NeedPregnancyInfo = needPregnancyInfo;

            if (bool.TryParse(element.Attribute("PregnancyInfoRequired")?.Value, out var pregnancyInfoRequired))
                PregnancyInfoRequired = pregnancyInfoRequired;

            PanelAnalyses = MessageHelper.GetResponseMessageElements<ResponseElementDictionaryPanelAnalysis>(element.Element("PanelAnalyses"));
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
            AnalysisCode = element.Attribute("AnalysisCode")?.Value;

            if (int.TryParse(element.Attribute("AnalysisId")?.Value, out var analysisId))
                AnalysisId = analysisId;

            BiomaterialCode = element.Attribute("BiomaterialCode")?.Value;

            if (int.TryParse(element.Attribute("BiomaterialId")?.Value, out var biomaterialId))
                BiomaterialId = biomaterialId;
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
                ServiceId = serviceId;

            ServiceCode = element.Attribute("ServiceCode")?.Value;

            if (float.TryParse(element.Attribute("Price")?.Value, out var price))
                Price = price;

            Extra1 = element.Attribute("Extra1")?.Value;

            if (int.TryParse(element.Attribute("MinDuration")?.Value, out var minDuration))
                MinDuration = minDuration;

            if (int.TryParse(element.Attribute("MaxDuration")?.Value, out var maxDuration))
                MaxDuration = maxDuration;

            if (int.TryParse(element.Attribute("DurationUnit")?.Value, out var durationUnitInt))
                DurationUnit = IntToDurationUnit(durationUnitInt);

            if (bool.TryParse(element.Attribute("AllowCito")?.Value, out var allowCito))
                AllowCito = allowCito;

            if (float.TryParse(element.Attribute("CitoPrice")?.Value, out var citoPrice))
                CitoPrice = citoPrice;

            if (int.TryParse(element.Attribute("CitoMinDuration")?.Value, out var citoMinDuration))
                CitoMinDuration = citoMinDuration;

            if (int.TryParse(element.Attribute("CitoMaxDuration")?.Value, out var citoMaxDuration))
                CitoMaxDuration = citoMaxDuration;

            if (int.TryParse(element.Attribute("CitoDurationUnit")?.Value, out var citoDurationUnit))
                CitoDurationUnit = IntToDurationUnit(citoDurationUnit);

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
                StartDate = startDate;

            if (DateTime.TryParseExact(element.Attribute("EndDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
                EndDate = endDate;

            if (int.TryParse(element.Attribute("TargetId")?.Value, out var targetId))
                TargetId = targetId;

            TargetCode = element.Attribute("TargetCode")?.Value;
            ReasonName = element.Attribute("ReasonName")?.Value;
            Comment = element.Attribute("Comment")?.Value;
        }
    }
}

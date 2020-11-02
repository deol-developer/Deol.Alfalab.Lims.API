﻿using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseNewReferralResults : ResponseMessage
    {
        public ResponseElementQueueRangeParameters Query { get; private set; }

        public IEnumerable<ResponseElementNewReferralId> Results { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Query = MessageHelper.GetResponseMessageElement<ResponseElementQueueRangeParameters>(message.Element("Query"));

            this.Results = MessageHelper.GetResponseMessageElements<ResponseElementNewReferralId>(message.Element("Results"));
        }
    }

    public class ResponseElementQueueRangeParameters : IResponseMessageElement
    {
        public bool? OnlyCreatedFromLis { get; private set; }
        public bool? AllowModified { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTill { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (bool.TryParse(element.Attribute("OnlyCreatedFromLis")?.Value, out var onlyCreatedFromLis))
                this.OnlyCreatedFromLis = onlyCreatedFromLis;

            if (bool.TryParse(element.Attribute("AllowModified")?.Value, out var allowModified))
                this.AllowModified = allowModified;

            if (DateTime.TryParseExact(element.Attribute("DateFrom")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateFrom))
                this.DateFrom = dateFrom;

            if (DateTime.TryParseExact(element.Attribute("DateTill")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTill))
                this.DateTill = dateTill;
        }
    }

    public class ResponseElementNewReferralId : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }
        public int LisId { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.MisId = element.Attribute("MisId")?.Value;
            this.Nr = element.Attribute("Nr")?.Value;

            if (int.TryParse(element.Attribute("LisId")?.Value, out var lisId))
                this.LisId = lisId;

            if (DateTime.TryParseExact(element.Attribute("UpdateDate")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var updateDate))
                this.UpdateDate = updateDate;
        }
    }
}

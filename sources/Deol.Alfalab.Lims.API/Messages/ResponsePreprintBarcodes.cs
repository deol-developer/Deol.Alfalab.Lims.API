﻿using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponsePreprintBarcodes : ResponseMessage
    {
        public ResponseElementPool Pool { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Pool = MessageHelper.GetResponseMessageElement<ResponseElementPool>(message.Element("Pool"));
        }
    }

    public class ResponseElementPool : IResponseMessageElement
    {
        public int Count { get; private set; }
        public long FirstNr { get; private set; }
        public long LastNr { get; private set; }
        public string HospitalCode { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("Count")?.Value, out var count))
                this.Count = count;

            if (long.TryParse(element.Attribute("FirstNr")?.Value, out var firstNr))
                this.FirstNr = firstNr;

            if (long.TryParse(element.Attribute("LastNr")?.Value, out var lastNr))
                this.LastNr = lastNr;

            this.HospitalCode = element.Attribute("HospitalCode")?.Value;
        }
    }
}

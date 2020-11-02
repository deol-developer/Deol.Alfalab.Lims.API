﻿using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseChangeEmail : ResponseMessage
    {
        public ResponseElementEmail Query { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Query = MessageHelper.GetResponseMessageElement<ResponseElementEmail>(message.Element("Query"));
        }
    }

    public class ResponseElementEmail : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }
        public int? LisId { get; private set; }
        public string EmailAddress { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("LisId")?.Value, out var lisId))
                this.LisId = lisId;

            this.MisId          = element.Attribute("MisId")?.Value;
            this.Nr             = element.Attribute("Nr")?.Value;
            this.EmailAddress   = element.Attribute("EmailAddress")?.Value;
        }
    }
}

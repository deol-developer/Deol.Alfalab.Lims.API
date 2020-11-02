using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseCountReferralResults : ResponseMessage
    {
        public ResponseElementQueueParameters Query { get; private set; }
        public ResponseElementCount Count { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Query = MessageHelper.GetResponseMessageElement<ResponseElementQueueParameters>(message.Element("Query"));

            this.Count = MessageHelper.GetResponseMessageElement<ResponseElementCount>(message.Element("Count"));
        }
    }

    public class ResponseElementQueueParameters : IResponseMessageElement
    {
        public bool? OnlyCreatedFromLis { get; private set; }
        public bool? AllowModified { get; private set; }

        public string ModValue { get; private set; }
        public string ModCount { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (bool.TryParse(element.Attribute("OnlyCreatedFromLis")?.Value, out var onlyCreatedFromLis))
                this.OnlyCreatedFromLis = onlyCreatedFromLis;

            if (bool.TryParse(element.Attribute("AllowModified")?.Value, out var allowModified))
                this.AllowModified = allowModified;

            this.ModValue = element.Attribute("ModValue")?.Value;
            this.ModCount = element.Attribute("ModCount")?.Value;
        }
    }

    public class ResponseElementCount : IResponseMessageElement
    {
        public int Value { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("Value")?.Value, out var value))
                this.Value = value;
        }
    }
}

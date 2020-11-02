using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseDictionariesVersion : ResponseMessage
    {
        public ResponseElementVersion Version { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Version = MessageHelper.GetResponseMessageElement<ResponseElementVersion>(message.Element("Version"));
        }
    }

    public class ResponseElementVersion : IResponseMessageElement
    {
        public int Version { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("Version")?.Value, out var ver))
                this.Version = ver;
        }
    }
}

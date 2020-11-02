using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseImportReferral : ResponseMessage
    {
        public ResponseElementReferralId Referral { get; private set; }

        public IEnumerable<ResponseElementContainer> Containers { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Referral = MessageHelper.GetResponseMessageElement<ResponseElementReferralId>(message.Element("Referral"));

            this.Containers = MessageHelper.GetResponseMessageElements<ResponseElementContainer>(message.Element("Containers"));
        }
    }

    public class ResponseElementReferralId : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }
        public int? LisId { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            if (int.TryParse(element.Attribute("LisId")?.Value, out var lisId))
                this.LisId = lisId;

            this.MisId  = element.Attribute("MisId")?.Value;
            this.Nr     = element.Attribute("Nr")?.Value;
        }
    }

    public class ResponseElementContainer : IResponseMessageElement
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortName { get; private set; }
        public int Rank { get; private set; }
        public int ImageIndex { get; private set; }
        public string Comment { get; private set; }
        public int PlanCount { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.Name = element.Attribute("Name")?.Value;
            this.Code = element.Attribute("Code")?.Value;
            this.ShortName = element.Attribute("ShortName")?.Value;

            if (int.TryParse(element.Attribute("Rank")?.Value, out var rank))
                this.Rank = rank;

            if (int.TryParse(element.Attribute("ImageIndex")?.Value, out var imageIndex))
                this.ImageIndex = imageIndex;

            this.Comment = element.Attribute("Comment")?.Value;

            if (int.TryParse(element.Attribute("PlanCount")?.Value, out var planCount))
                this.PlanCount = planCount;
        }
    }
}

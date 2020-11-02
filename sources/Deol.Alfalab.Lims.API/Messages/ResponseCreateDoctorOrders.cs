using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseCreateDoctorOrders : ResponseMessage
    {
        public ResponseElementShortReferralId Referral { get; private set; }

        protected override void InitMessageElements(XElement message)
        {
            this.Referral = MessageHelper.GetResponseMessageElement<ResponseElementShortReferralId>(message.Element("Referral"));
        }
    }

    public class ResponseElementShortReferralId : IResponseMessageElement
    {
        public string MisId { get; private set; }
        public string Nr { get; private set; }

        public void InitFromXMLElement(XElement element)
        {
            this.MisId = element.Attribute("MisId")?.Value;
            this.Nr = element.Attribute("Nr")?.Value;
        }
    }
}

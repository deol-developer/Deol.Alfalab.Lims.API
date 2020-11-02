using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestCreateDoctorOrders : RequestMessage
    {
        public bool UseUserFields => this.Referral.UseUserFields;

        public RequestCreateDoctorOrders(bool usePatientUserFields = false, bool useReferralUserFields = false)
            :base ()
        {
            this.Pation = new RequestElementPation(usePatientUserFields);

            this.Referral = new RequestElementDoctorReferral(useReferralUserFields);
        }

        public RequestCreateDoctorOrders(AuthorizationData authorizationData, bool usePatientUserFields = false, bool useReferralUserFields = false)
            : base(authorizationData)
        {
            this.Pation = new RequestElementPation(usePatientUserFields);

            this.Referral = new RequestElementDoctorReferral(useReferralUserFields);
        }

        public RequestElementPation Pation { get; }

        public RequestElementDoctorReferral Referral { get; }

        public override string MessageType => "query-create-doctor-orders";

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            this.Pation.ToXMLElement(),
            this.Referral.ToXMLElement()
        };
    }

    public class RequestElementDoctorReferral : RequestElementBaseReferral
    {
        public RequestElementDoctorReferral(bool useUserFields = false) : base(useUserFields) { }

        public DateTime? Date { get; set; }

        public ICollection<RequestElementDoctorOrder> Orders { get; } = new List<RequestElementDoctorOrder>();

        public override XElement ToXMLElement()
        {
            var element = base.ToXMLElement();

            if (this.Date != null)
                element.Add(new XAttribute("Date", MessageHelper.GetAttributeValue(this.Date.Value)));

            element.Add(new XElement("Orders", this.Orders.Select(x => x.ToXMLElement())));

            return element;
        }
    }

    public class RequestElementDoctorOrder : IRequestMessageElement
    {
        public string Code { get; set; }
        public string BiomaterialCode { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Item", new XAttribute("Code", MessageHelper.GetAttributeValue(this.Code)));
           
            if (this.BiomaterialCode != null)
                element.Add(new XAttribute("BiomaterialCode", this.BiomaterialCode));

            return element;
        }
    }
}

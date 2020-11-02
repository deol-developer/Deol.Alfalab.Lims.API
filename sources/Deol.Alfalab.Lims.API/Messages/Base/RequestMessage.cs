using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages.Base
{
    public interface IRequestMessage
    {
        string ToXMLMessage();
    }
    public interface IRequestMessageElement
    {
        XElement ToXMLElement();
    }

    public abstract class RequestMessage : IRequestMessage
    {
        public abstract string MessageType { get; }
        public DateTime Date { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Password { get; set; }

        public RequestMessage() { }

        public RequestMessage(AuthorizationData authorizationData) 
        {
            this.Sender     = authorizationData.Sender;
            this.Receiver   = authorizationData.Receiver;
            this.Password   = authorizationData.Password;
        }

        public string ToXMLMessage()
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Message", this.GetMessageAttributes(), this.GetMessageElements()));

            return $"{doc.Declaration}{Environment.NewLine}{doc}";
        }

        protected virtual IEnumerable<XElement> GetMessageElements() => Enumerable.Empty<XElement>();

        private IEnumerable<XAttribute> GetMessageAttributes() => new XAttribute[]
        {
            new XAttribute("MessageType", MessageHelper.GetAttributeValue(this.MessageType)),
            new XAttribute("Date", MessageHelper.GetAttributeValue(this.Date)),
            new XAttribute("Sender", MessageHelper.GetAttributeValue(this.Sender)),
            new XAttribute("Receiver", MessageHelper.GetAttributeValue(this.Receiver)),
            new XAttribute("Password", MessageHelper.GetAttributeValue(this.Password))
        };
    }
}

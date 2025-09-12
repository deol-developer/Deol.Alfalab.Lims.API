using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages.Base
{
    public interface IRequestMessage
    {
        XDocument ToXMLMessage();
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
            Sender     = authorizationData.Sender;
            Receiver   = authorizationData.Receiver;
            Password   = authorizationData.Password;
        }

        public XDocument ToXMLMessage()
        {
            var doc = new XDocument(
                new XElement("Message", GetMessageAttributes(),
                    GetMessageElements()));
            
            return doc;
        }

        protected virtual IEnumerable<XElement> GetMessageElements() => Enumerable.Empty<XElement>();

        private IEnumerable<XAttribute> GetMessageAttributes() => new XAttribute[]
        {
            new XAttribute("MessageType", MessageHelper.GetAttributeValue(MessageType)),
            new XAttribute("Date", MessageHelper.GetAttributeValue(Date)),
            new XAttribute("Sender", MessageHelper.GetAttributeValue(Sender)),
            new XAttribute("Receiver", MessageHelper.GetAttributeValue(Receiver)),
            new XAttribute("Password", MessageHelper.GetAttributeValue(Password))
        };
    }
}

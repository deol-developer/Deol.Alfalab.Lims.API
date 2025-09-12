using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages.Base
{
    public interface IResponseMessage
    {
        void InitFromXMLMessage(string messageStr);
    }

    public interface IResponseMessageElement
    {
        void InitFromXMLElement(XElement element);
    }

    public class ResponseMessage : IResponseMessage
    {
        public string MessageType { get; private set; }
        public DateTime Date { get; private set; }
        public string Sender { get; private set; }
        public string Receiver { get; private set; }
        public string Error { get; private set; }
        public bool HasError => Error != null;

        public ICollection<ResponseElementWarning> Warnings { get; private set; }
        public bool HahWarnings => Warnings != null && Warnings.Count > 0;

        public bool Success => !HasError && !HahWarnings;

        public void InitFromXMLMessage(string messageStr) 
        {
            var xml = XDocument.Parse(messageStr);

            var message = xml.Element("Message");
            InitMessageAttributes(message);

            var warnings = message.Element("Warnings");
            if (warnings != null)
                InitMessageWarnings(warnings);

            InitMessageElements(message);
        }

        protected virtual void InitMessageElements(XElement message) { }

        private void InitMessageAttributes(XElement message)
        {
            MessageType = message.Attribute("MessageType")?.Value;
            Sender = message.Attribute("Sender")?.Value;
            Receiver = message.Attribute("Receiver")?.Value;

            if (DateTime.TryParseExact(message.Attribute("Date")?.Value, MessageHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                Date = date;

            var error = message.Attribute("Error");

            if (error != null)
                Error = error.Value;
        }

        private void InitMessageWarnings(XElement warnings)
        {
            Warnings = new List<ResponseElementWarning>();

            foreach (var warning in warnings.Elements("Item"))
                Warnings.Add(new ResponseElementWarning(warning.Attribute("Text").Value));
        }
    }

    public class ResponseElementWarning
    {
        public ResponseElementWarning(string text) => Text = text;
        public string Text { get; private set; }
    }
}

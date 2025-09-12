using Deol.Alfalab.Lims.API.Messages.Base;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestBlankFile : RequestMessage
    {
        public RequestElementBlankId Query { get; } = new RequestElementBlankId();

        public override string MessageType => "query-blank-file";

        public RequestBlankFile() : base() { }

        public RequestBlankFile(AuthorizationData authorizationData) : base(authorizationData) { }

        protected override IEnumerable<XElement> GetMessageElements() => new XElement[]
        {
            Query.ToXMLElement()
        };
    }

    public class RequestElementBlankId : IRequestMessageElement
    {
        public int? BlankId { get; set; }

        public string BlankGUID { get; set; }

        public XElement ToXMLElement()
        {
            var element = new XElement("Query");

            if (BlankId != null)
                element.Add(new XAttribute("BlankId", BlankId.Value));

            if (BlankGUID != null)
                element.Add(new XAttribute("BlankGUID", BlankGUID));

            return element;
        }
    }
}

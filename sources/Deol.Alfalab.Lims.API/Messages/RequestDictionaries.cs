using Deol.Alfalab.Lims.API.Messages.Base;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestDictionaries : RequestMessage
    {
        public override string MessageType => "query-dictionaries";

        public RequestDictionaries() : base() { }

        public RequestDictionaries(AuthorizationData authorizationData) : base(authorizationData) { }
    }
}

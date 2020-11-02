using Deol.Alfalab.Lims.API.Messages.Base;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class RequestDictionariesVersion : RequestMessage
    {
        public override string MessageType => "query-dictionaries-version";

        public RequestDictionariesVersion() : base() { }

        public RequestDictionariesVersion(AuthorizationData authorizationData) : base(authorizationData) { }
    }
}

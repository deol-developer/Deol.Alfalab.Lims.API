using System;
using System.Runtime.Serialization;

namespace Deol.Alfalab.Lims.API
{
    [Serializable]
    public class ClientException : SystemException
    {
        public ClientException() : base() { }
        public ClientException(string message) : base(message) { }
        public ClientException(string message, Exception innerException) : base(message, innerException) { }
        protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class SendRequestException : ClientException
    {
        public SendRequestException() : base() { }
        public SendRequestException(string message) : base(message) { }
        public SendRequestException(string message, Exception innerException) : base(message, innerException) { }
        protected SendRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ParsingResponseExсeption : ClientException
    {
        public ParsingResponseExсeption() : base() { }
        public ParsingResponseExсeption(string message) : base(message) { }
        public ParsingResponseExсeption(string message, Exception innerException) : base(message, innerException) { }
        protected ParsingResponseExсeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

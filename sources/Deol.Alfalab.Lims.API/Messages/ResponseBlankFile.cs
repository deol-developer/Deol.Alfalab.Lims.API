using Deol.Alfalab.Lims.API.Messages.Base;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseBlankFile
    {
        public ResponseMessage Message { get; private set; }

        public bool HasMessage => Message != null;

        public void InitMessage(string xmlMessage)
        {
            Message = new ResponseMessage();
            Message.InitFromXMLMessage(xmlMessage);
        }

        public byte[] BinaryData { get; private set; }

        public bool HasBinaryData => BinaryData != null;

        public string FileExtension { get; private set; }

        public bool HasFileExtension => !string.IsNullOrEmpty(FileExtension);

        public void InitBinaryData(byte[] binaryData, string fileExtension)
        {
            BinaryData = binaryData;
            FileExtension = fileExtension;
        }
    }
}

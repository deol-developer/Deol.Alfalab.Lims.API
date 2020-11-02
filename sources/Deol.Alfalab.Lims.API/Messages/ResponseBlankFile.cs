using Deol.Alfalab.Lims.API.Messages.Base;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseBlankFile
    {
        public ResponseMessage Message { get; private set; }

        public bool HasMessage => this.Message != null;

        public void InitMessage(string xmlMessage)
        {
            this.Message = new ResponseMessage();
            this.Message.InitFromXMLMessage(xmlMessage);
        }

        public byte[] BinaryData { get; private set; }

        public bool HasBinaryData => this.BinaryData != null;

        public string FileExtension { get; private set; }

        public bool HasFileExtension => !string.IsNullOrEmpty(this.FileExtension);

        public void InitBinaryData(byte[] binaryData, string fileExtension)
        {
            this.BinaryData = binaryData;
            this.FileExtension = fileExtension;
        }
    }
}

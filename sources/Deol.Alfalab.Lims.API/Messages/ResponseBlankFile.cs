using Deol.Alfalab.Lims.API.Messages.Base;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages
{
    public class ResponseBlankFile
    {
        public ResponseMessage Message { get; private set; }

        public bool HasMessage => Message != null;

        public void InitMessage(XDocument document)
        {
            Message = new ResponseMessage();
            Message.InitFromXMLMessage(document);
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

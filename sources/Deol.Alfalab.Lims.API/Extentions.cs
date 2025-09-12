using System.IO;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API
{
    public static class XDocumentExtentions
    {
        public static string ToStringWithDeclaration(this XDocument document)
        {
            if (document.Declaration is null)
                return document.ToString();

            using (var writer = new StringWriter())
            {
                document.Save(writer);
                return writer.ToString();
            }
        }
    }
}

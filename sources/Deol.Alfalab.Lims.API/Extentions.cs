using System;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API
{
    public static class XDocumentExtentions
    {
        public static string ToStringWithDeclaration(this XDocument document)
        {
            if (document.Declaration is null)
                return document.ToString();

            return document.Declaration.ToString() + Environment.NewLine + document.ToString();
        }
    }
}

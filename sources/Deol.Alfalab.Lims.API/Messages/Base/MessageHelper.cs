using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Deol.Alfalab.Lims.API.Messages.Base
{
    public static class MessageHelper
    {
        public static string DateFormat => "dd.MM.yyyy HH:mm:ss";

        public static string GetAttributeValue(string value) => string.IsNullOrEmpty(value) ? "" : value;
        public static string GetAttributeValue(bool value) => value ? "true" : "false";
        public static string GetAttributeValue(DateTime value) => value.ToString(DateFormat);
        
        public static string GetAttributeValue<TValue>(TValue? value) 
            where TValue : struct
        { 
            if (value.HasValue)
            {
                switch(value)
                {
                    case bool valueBool:         return GetAttributeValue(valueBool);
                    case DateTime valueDateTime: return GetAttributeValue(valueDateTime);
                    default:                     return value.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        public static TResponseMessageElement GetResponseMessageElement<TResponseMessageElement>(XElement element)
            where TResponseMessageElement : class, IResponseMessageElement, new()
        {
            if (element == null)
                return null;

            var item = new TResponseMessageElement();
            item.InitFromXMLElement(element);

            return item;
        }

        public static IEnumerable<TResponseMessageElement> GetResponseMessageElements<TResponseMessageElement>(XElement collectionElement)
            where TResponseMessageElement : class, IResponseMessageElement, new()
        {
            if (collectionElement == null)
                return null;

            var collection = collectionElement.Elements("Item");

            if (collection != null && collection.Any())
            {
                return collection.Select(x =>
                {
                    var item = new TResponseMessageElement();
                    item.InitFromXMLElement(x);
                    return item;
                });
            }
            else
            {
                return Enumerable.Empty<TResponseMessageElement>();
            }
        }
    }
}

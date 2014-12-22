using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace HomeworkTrackingSystemPrj.Utility
{
    public static class  Utility
    {
        public static string CreateFieldXMLElement(Dictionary<string, string> FieldProperties)
        {
            
            StringBuilder fieldXml = new StringBuilder();
            fieldXml.Append("<Field ");
            foreach (KeyValuePair<string, string> fieldProperty in FieldProperties)
            {
                string PropertyName = fieldProperty.Key;
                string PropertyValue = fieldProperty.Value;
                fieldXml.AppendFormat("{0}='{1}' ", PropertyName, PropertyValue);
            }

            return fieldXml.ToString();
        }
    }
}

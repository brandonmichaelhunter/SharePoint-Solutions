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
        public static string GetLetterGrade(int RawGradeValue)
        {
            string LetterGrade = "";
            if(RawGradeValue < 60){LetterGrade="F";}
            else if(RawGradeValue >= 60 || RawGradeValue < 70) {LetterGrade = "D";}
            else if(RawGradeValue >=70 || RawGradeValue < 80) {LetterGrade = "C";}
            else if(RawGradeValue >=80 || RawGradeValue < 90) {LetterGrade = "B";}
            else if(RawGradeValue >=90){LetterGrade= "A";}
            return LetterGrade;
        }
    }
}

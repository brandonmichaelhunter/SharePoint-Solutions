using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.Utility;

namespace HomeworkTrackingSystemPrj.SiteColumns
{
    public static class SiteColumns
    {
        #region Custom site columns 
        public static Guid ClassName { get { return new Guid("{48D6DE55-BB8B-4F16-A93A-B8A821624AAF}"); } }
        public static Guid ClassYear { get { return new Guid("{4A1F502F-8A38-4EF6-9F13-1BA4CFED4474}"); } }
        public static Guid Student { get { return new Guid("{8B0C7FD8-BC5B-4B62-A9E6-E9FC72509784}"); } }
        public static Guid StudentFirstname { get { return new Guid("{CB591436-1375-414E-B347-3FBA8E368643}"); } }
        public static Guid StudentLastname { get { return new Guid("{E7533149-373E-4339-92C9-4A54E01F01EC}"); } }
        public static Guid StudentFullname { get { return new Guid("{729AFD18-284B-4400-8CEA-285CBA04E0F5}"); } }
        public static Guid AssignmentName{ get { return new Guid("{D651A62D-632F-4C0B-A12F-2A6E806B4F7C}"); } }
        public static Guid AssignmentDueDate { get { return new Guid("{8DE34A34-03CA-4DF9-93C7-0DD76DC67486}"); } }
        public static Guid IsAssignmentComplete { get { return new Guid("{383E6542-1FDD-402A-843A-4C7ECF1FB00B}"); } }
        public static Guid Class { get { return new Guid("{A6EE3709-131A-406E-9700-72C510A8CE0C}"); } }
        public static Guid AssignmentGrade { get { return new Guid("{9AF2AECA-0ED2-4AEC-AB87-44EC3F92C146}"); } }
        public static Guid Submissions { get { return new Guid("{323E22E0-792A-4BA4-9652-15CF9E1E13B3}"); } }
        public static Guid AssignmentDescription { get { return new Guid("{C0DE4FF0-2997-4AAD-989B-59B36318A6A7}"); } }
        public static Guid LetterGrade { get { return new Guid("{32F06BCF-6ED1-47F7-9F32-2AE20181AB7D}"); } }
        public static Guid TotalPoints { get { return new Guid("{0E352899-33E7-4384-86D0-54BCD788817E}"); } }
        public static Guid TotalPointsAllowed { get { return new Guid("{7A05A44C-6A27-486D-B263-EFC830B3C329}"); } }
        public static Guid HomeworkAssignmentName { get { return new Guid("{67617964-0A05-4159-A07A-CA6A90D94F0E}"); } }
        public static Guid HomeworkAssignmentPointsRecievied { get { return new Guid("{EE6A920C-1BC7-4B79-B27A-65B62E0E5223}"); } }
        public static Guid HomeworkAssignmentPointsAllowed { get { return new Guid("{19167CEA-5009-4FF1-8B4B-A454402F2D1F}"); } }
        public static Guid Parents { get { return new Guid("{04B0ADA0-9758-4161-99D7-2D85178A7F2B}"); } }
        public static Guid Teacher { get { return new Guid("{6B8D2463-155F-48F2-9FD4-7923C84B49F1}"); } }
        #endregion Custom site columns
        
        
        /* Provision site columns. */
        public static void ProvisionSiteColumns(SPWeb spWeb)
        {
            #region Class Name
            if (!spWeb.AvailableFields.Contains(ClassName))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", ClassName.ToString());
                fp.Add("Name", "ClassName");
                fp.Add("StaticName", "ClassName");
                fp.Add("Required", "TRUE");
                fp.Add("DisplayName", "Class Name");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Class Year
            if (!spWeb.AvailableFields.Contains(ClassYear))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", ClassYear.ToString());
                fp.Add("Name", "ClassYear");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "ClassYear");
                fp.Add("DisplayName", "Class Year");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);
                
                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Student
            //if (!spWeb.AvailableFields.Contains(Student))
            //{
            //    Dictionary<string, string> fp = new Dictionary<string, string>();
            //    fp.Add("ID", Student.ToString());
            //    fp.Add("Name", "Student");
            //    fp.Add("Required", "TRUE");
            //    fp.Add("StaticName", "Student");
            //    fp.Add("DisplayName", "Student");
            //    //fp.Add("Type", SPFieldType.Lookup.ToString());
            //    fp.Add("Group", AppConstants.SiteColumnGroupName);
            //    fp.Add("Type", "LookupMulti");//Enum.GetName(typeof(SPFieldType), SPFieldType.Lookup));
            //    fp.Add("AllowMultipleValues", Boolean.TrueString);
            //    fp.Add("ShowField", "Title");
            //    fp.Add("List", Lists.Students.GetList(spWeb).ID.ToString());
            //    string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
            //    if (fieldXml != string.Empty)
            //    {
            //        fieldXml += "/>";
            //        spWeb.Fields.AddFieldAsXml(fieldXml);
            //    }
            //}
            if (!spWeb.AvailableFields.Contains(Student))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", Student.ToString());
                fp.Add("Name", "Student");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "Student");
                fp.Add("DisplayName", "Student");
                fp.Add("Type", SPFieldType.User.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Class Name
            if (!spWeb.AvailableFields.Contains(ClassName))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", ClassName.ToString());
                fp.Add("Name", "ClassName");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "ClassName");
                fp.Add("DisplayName", "Class Name");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region First Name
            if (!spWeb.AvailableFields.Contains(StudentFirstname))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", StudentFirstname.ToString());
                fp.Add("Name", "StudentFirstname");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "StudentFirstname");
                fp.Add("DisplayName", "First Name");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Last Name
            if (!spWeb.AvailableFields.Contains(StudentLastname))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", StudentLastname.ToString());
                fp.Add("Name", "StudentLastname");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "StudentLastname");
                fp.Add("DisplayName", "Last Name");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Full Name
            if (!spWeb.AvailableFields.Contains(StudentFullname))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", StudentFullname.ToString());
                fp.Add("Name", "StudentFullname");
                fp.Add("StaticName", "StudentFullname");
                fp.Add("DisplayName", "Student Full Name");
                fp.Add("Type", SPFieldType.Calculated.ToString());
                fp.Add("ResultType", "Text");
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += ">";
                    fieldXml += "<Formula>=TEXT([StudentFirstname],[StudentLastname])</Formula>";
                    fieldXml += "<FieldRefs>";
                    fieldXml += "<FieldRef Name='StudentFirstname'/>";
                    fieldXml += "<FieldRef Name='StudentLastname'/>";
                    fieldXml += "</FieldRefs>";
                    fieldXml += "</Field>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Assignment Name
            if (!spWeb.AvailableFields.Contains(AssignmentName))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", AssignmentName.ToString());
                fp.Add("Name", "AssignmentName");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "AssignmentName");
                fp.Add("DisplayName", "Assignment Name");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Assignment Due Date
            if (!spWeb.AvailableFields.Contains(AssignmentDueDate))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", AssignmentDueDate.ToString());
                fp.Add("Name", "AssignmentDueDate");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "AssignmentDueDate");
                fp.Add("DisplayName", "Assignment Due Date");
                fp.Add("Type", SPFieldType.DateTime.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);
                fp.Add("Format", "DateOnly");

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region IsComplete
            if (!spWeb.AvailableFields.Contains(IsAssignmentComplete))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", IsAssignmentComplete.ToString());
                fp.Add("Name", "IsAssignmentComplete");
                fp.Add("StaticName", "IsAssignmentComplete");
                fp.Add("DisplayName", "Is Assignment Complete");
                fp.Add("Type", SPFieldType.Boolean.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += ">";
                    fieldXml += "<Default>0</Default>";
                    fieldXml += "</Field>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Class
            if (!spWeb.AvailableFields.Contains(Class))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", Class.ToString());
                fp.Add("Name", "Class");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "Class");
                fp.Add("DisplayName", "Class");
                fp.Add("Group", AppConstants.SiteColumnGroupName);
                fp.Add("Type", Enum.GetName(typeof(SPFieldType), SPFieldType.Lookup));
                fp.Add("ShowField", "Title");
                fp.Add("List", Lists.Classes.GetList(spWeb).ID.ToString());

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region AssignmentGrade
            if (!spWeb.AvailableFields.Contains(AssignmentGrade))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", AssignmentGrade.ToString());
                fp.Add("Name", "AssignmentGrade");
                fp.Add("StaticName", "AssignmentGrade");
                fp.Add("DisplayName", "Assignment Grade");
                fp.Add("Type", SPFieldType.Number.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Submissions
            if (!spWeb.AvailableFields.Contains(Submissions))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", Submissions.ToString());
                fp.Add("Name", "Submissions");
                fp.Add("StaticName", "Submissions");
                fp.Add("DisplayName", "Submissions");
                fp.Add("Group", AppConstants.SiteColumnGroupName);
                fp.Add("Type", Enum.GetName(typeof(SPFieldType), SPFieldType.Lookup));
                fp.Add("ShowField", "Title");
                fp.Add("List", Lists.Submissions.GetList(spWeb).ID.ToString());

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region AssignmentDescription
            if (!spWeb.AvailableFields.Contains(AssignmentDescription))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", AssignmentDescription.ToString());
                fp.Add("Name", "AssignmentDescription");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "AssignmentDescription");
                fp.Add("DisplayName", "Assignment Description");
                fp.Add("Type", SPFieldType.Note.ToString());
                fp.Add("RichText", Boolean.FalseString);
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region LetterGrade
            if (!spWeb.AvailableFields.Contains(LetterGrade))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", LetterGrade.ToString());
                fp.Add("Name", "LetterGrade");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "LetterGrade");
                fp.Add("DisplayName", "Letter Grade");
                fp.Add("Type", SPFieldType.Text.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region TotalPoints
            if (!spWeb.AvailableFields.Contains(TotalPoints))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", TotalPoints.ToString());
                fp.Add("Name", "TotalPoints");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "TotalPoints");
                fp.Add("DisplayName", "Total Points");
                fp.Add("Type", SPFieldType.Number.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region TotalPointsAllowed
            if (!spWeb.AvailableFields.Contains(TotalPointsAllowed))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", TotalPointsAllowed.ToString());
                fp.Add("Name", "TotalPointsAllowed");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "TotalPointsAllowed");
                fp.Add("DisplayName", "Total Points Allowed");
                fp.Add("Type", SPFieldType.Number.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region HomeworkAssignmentName
            if (!spWeb.AvailableFields.Contains(HomeworkAssignmentName))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", HomeworkAssignmentName.ToString());
                fp.Add("Name", "HomeworkAssignmentName");
                fp.Add("Required", "TRUE");
                fp.Add("StaticName", "HomeworkAssignmentName");
                fp.Add("DisplayName", "Homework Assignment Name");
                fp.Add("Group", AppConstants.SiteColumnGroupName);
                fp.Add("Type", Enum.GetName(typeof(SPFieldType), SPFieldType.Lookup));
                fp.Add("ShowField", "Title");
                fp.Add("List", Lists.HomeworkAssignments.GetList(spWeb).ID.ToString());

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region HomeworkAssignmentPointsRecievied
            if (!spWeb.AvailableFields.Contains(HomeworkAssignmentPointsRecievied))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", HomeworkAssignmentPointsRecievied.ToString());
                fp.Add("Name", "HomeworkAssignmentPointsRecievied");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "HomeworkAssignmentPointsRecievied");
                fp.Add("DisplayName", "Homework Assignment Points Recieved");
                fp.Add("Type", SPFieldType.Number.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region HomeworkAssignmentPointsAllowed
            if (!spWeb.AvailableFields.Contains(HomeworkAssignmentPointsAllowed))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", HomeworkAssignmentPointsAllowed.ToString());
                fp.Add("Name", "HomeworkAssignmentPointsAllowed");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "HomeworkAssignmentPointsAllowed");
                fp.Add("DisplayName", "Homework Assignment Points Allowed");
                fp.Add("Type", SPFieldType.Number.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Parents
            if (!spWeb.AvailableFields.Contains(Parents))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", Parents.ToString());
                fp.Add("Name", "Parents");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "Parents");
                fp.Add("DisplayName", "Parents");
                fp.Add("Type", SPFieldType.User.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion

            #region Teacher
            if (!spWeb.AvailableFields.Contains(Teacher))
            {
                Dictionary<string, string> fp = new Dictionary<string, string>();
                fp.Add("ID", Teacher.ToString());
                fp.Add("Name", "Teacher");
                fp.Add("Required", Boolean.FalseString);
                fp.Add("StaticName", "Teacher");
                fp.Add("DisplayName", "Teacher");
                fp.Add("Type", SPFieldType.User.ToString());
                fp.Add("Group", AppConstants.SiteColumnGroupName);

                string fieldXml = Utility.Utility.CreateFieldXMLElement(fp);
                if (fieldXml != string.Empty)
                {
                    fieldXml += "/>";
                    spWeb.Fields.AddFieldAsXml(fieldXml);
                }
            }
            #endregion
        }


    }
}

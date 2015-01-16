using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.Utility;
using HomeworkTrackingSystemPrj.SiteColumns;

namespace ContentTypes
{
    public static class Student
    {
        public static string ContentTypeName = "Student";
        public static SPContentTypeId ContentTypeID { get { return new SPContentTypeId("0x0100CCBA5B1D76064EDE9B740C7006DA5306"); } }

        public static void ProvisionContentType(SPWeb spWeb)
        {
            /* Create the content type. */
            SPContentType ct = spWeb.ContentTypes[ContentTypeID];
            if (ct == null)
            {

                ct = new SPContentType(ContentTypeID, spWeb.ContentTypes, ContentTypeName);
                ct.Group = AppConstants.ContentTypeGroupName;
                spWeb.ContentTypes.Add(ct);
            }

            /*Add fields to content type .*/
            //if (!ct.Fields.Contains(SiteColumns.StudentFirstname))
            //{
            //    SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.StudentFirstname]);
            //    ct.FieldLinks.Add(field);
            //}

            //if (!ct.Fields.Contains(SiteColumns.StudentLastname))
            //{
            //    SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.StudentLastname]);
            //    ct.FieldLinks.Add(field);
            //}

            //if (!ct.Fields.Contains(SiteColumns.StudentFullname))
            //{
            //    SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.StudentFullname]);
            //    ct.FieldLinks.Add(field);
            //}
            if (!ct.Fields.Contains(SiteColumns.Student)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Student]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Parents)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Parents]);
                ct.FieldLinks.Add(field);
            }

            ct.Update(true);
        }
    }
}

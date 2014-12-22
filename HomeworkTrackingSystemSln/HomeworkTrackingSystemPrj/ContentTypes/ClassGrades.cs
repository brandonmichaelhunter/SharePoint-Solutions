using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.SiteColumns;
using HomeworkTrackingSystemPrj.Utility;

namespace ContentTypes
{
    public static class ClassGrades
    {
        public static string ContentTypeName = "ClassGrades";
        public static SPContentTypeId ContentTypeID { get { return new SPContentTypeId("0x010085610833E01A43D5BA96068BB31A57B3"); } }

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
            if (!ct.Fields.Contains(SiteColumns.ClassName))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.ClassName]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Student))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Student]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.ClassYear))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.ClassYear]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.TotalPoints))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.TotalPoints]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.TotalPointsAllowed))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.TotalPointsAllowed]);
                ct.FieldLinks.Add(field);
            }
            
            if (!ct.Fields.Contains(SiteColumns.LetterGrade))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.LetterGrade]);
                ct.FieldLinks.Add(field);
            }

            ct.Update();
        }
    }
}

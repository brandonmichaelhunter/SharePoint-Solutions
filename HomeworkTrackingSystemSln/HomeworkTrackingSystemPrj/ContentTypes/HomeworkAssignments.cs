using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.Utility;
using HomeworkTrackingSystemPrj.SiteColumns;
namespace ContentTypes
{
    public static class HomeworkAssignments
    {
        public static string ContentTypeName = "HomeworkAssignments";
        public static SPContentTypeId ContentTypeID { get { return new SPContentTypeId("0x0101009A3B96C126E74DE2A25A82E4BC1CB15D"); } }

        public static void ProvisionContentType(SPWeb spWeb)
        {
            /* Create the content type. */
            SPContentType ct = spWeb.ContentTypes[ContentTypeID];
            if (ct == null){
                ct = new SPContentType(ContentTypeID, spWeb.ContentTypes, ContentTypeName);
                ct.Group = AppConstants.ContentTypeGroupName;
                spWeb.ContentTypes.Add(ct);
            }

            /*Add fields to content type .*/
            if (!ct.Fields.Contains(SiteColumns.AssignmentName)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.AssignmentName]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.AssignmentDueDate)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.AssignmentDueDate]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.IsAssignmentComplete)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.IsAssignmentComplete]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.ClassName)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.ClassName]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.AssignmentGrade)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.AssignmentGrade]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Submissions)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Submissions]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.AssignmentDescription)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.AssignmentDescription]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.HomeworkAssignmentPointsRecievied)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.HomeworkAssignmentPointsRecievied]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.HomeworkAssignmentPointsAllowed)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.HomeworkAssignmentPointsAllowed]);
                ct.FieldLinks.Add(field);
            }

            ct.Update();
        }
    }
}

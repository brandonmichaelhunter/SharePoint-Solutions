using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.Utility;
using HomeworkTrackingSystemPrj.SiteColumns;

namespace ContentTypes
{
    public static class Submission
    {
        public static string ContentTypeName = "Submissions";
        public static SPContentTypeId ContentTypeID { get { return new SPContentTypeId("0x01010066187A47EAB64A9B82152A60BD6467C5"); } }

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
            if (!ct.Fields.Contains(SiteColumns.HomeworkAssignmentName))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.HomeworkAssignmentName]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Student))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Student]);
                ct.FieldLinks.Add(field);
            }

            ct.Update();
        }
    }
}

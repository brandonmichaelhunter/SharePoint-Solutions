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
        public static Guid EventReceiverID { get { return new Guid("850BD17F-F697-4A0D-AA80-02E5C296C4F0"); } }
        public static void ProvisionContentType(SPWeb spWeb)
        {
            /* Create the content type. */
            SPContentType ct = GetContentType(spWeb);
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

            if (!ct.Fields.Contains(SiteColumns.IsAssignmentComplete))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.IsAssignmentComplete]);
                ct.FieldLinks.Add(field);
            }

            ct.Update(true);
        }

        private static SPContentType GetContentType(SPWeb spWeb)
        {
            SPContentType ct = spWeb.ContentTypes[ContentTypeID];
            return ct;
        }
        public static void RegisterEventReceiverWithContentType(SPWeb spWeb, string AssemblyFullName)
        {
            SPContentType ct = spWeb.ContentTypes[ContentTypeID];
            if (ct != null)
            {
                /* Remove event reciever if it already exists. */
                if (ct.EventReceivers.EventReceiverDefinitionExist(EventReceiverID)){
                    SPEventReceiverDefinition DeletedER = ct.EventReceivers[EventReceiverID];
                    DeletedER.Delete();
                }

                /* Register the event receiver with the content type. */
                SPEventReceiverDefinition def = ct.EventReceivers.Add(EventReceiverID);
                def.Type = SPEventReceiverType.ItemUpdated;
                def.Assembly = AssemblyFullName;
                def.Class = typeof(EventReceivers.SubmissionsER).FullName;
                def.SequenceNumber = 100;
                def.Data = "";
                def.Update();
                ct.Update(true, false);


            }
        }
    }
}

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
        public static Guid EventReceiverID { get { return new Guid("A0AE1C61-D508-4E5C-99FA-31571905505C"); } }
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

            //if (!ct.Fields.Contains(SiteColumns.ClassName)){
            //    SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.ClassName]);
            //    ct.FieldLinks.Add(field);
            //}

            if (!ct.Fields.Contains(SiteColumns.LetterGrade)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.LetterGrade]);
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

            if (!ct.Fields.Contains(SiteColumns.Class)){
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Class]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Teacher))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Teacher]);
                ct.FieldLinks.Add(field);
            }

            if (!ct.Fields.Contains(SiteColumns.Student))
            {
                SPFieldLink field = new SPFieldLink(spWeb.AvailableFields[SiteColumns.Student]);
                ct.FieldLinks.Add(field);
            }
            ct.Update(true);
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

                /* Add event receiver to content type. */
                SPEventReceiverDefinition def = ct.EventReceivers.Add(EventReceiverID);
                def.Type = SPEventReceiverType.ItemUpdated;
                def.Assembly = AssemblyFullName;
                def.Class = typeof(EventReceivers.HomeworkAssignmentER).FullName;
                def.SequenceNumber = 100;
                def.Data = "";
                def.Update();
                ct.Update(true, false);
            }
        }
    }
}

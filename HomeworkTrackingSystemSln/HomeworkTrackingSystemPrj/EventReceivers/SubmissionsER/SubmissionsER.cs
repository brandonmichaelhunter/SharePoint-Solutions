using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using HomeworkTrackingSystemPrj.SiteColumns;

namespace EventReceivers.SubmissionsER
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class SubmissionsER : SPItemEventReceiver
    {
       /// <summary>
       /// An item was updated.
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);
           bool IsAssignmentComplete = (Boolean)properties.AfterProperties[properties.ListItem.Fields[SiteColumns.IsAssignmentComplete].InternalName];
           if (IsAssignmentComplete)
           {
               /* Access the associating homework assignment on the HomeworkAssignment list and update the IsAssignmentComplete field. */
               int AssignmentID = Convert.ToInt16(properties.AfterProperties[properties.List.Fields[SiteColumns.HomeworkAssignmentName].InternalName].ToString());
               SPList HomeworkAssignmentList = Lists.HomeworkAssignments.GetList(properties.Web);
               SPListItem HomeworkAssignment = HomeworkAssignmentList.Items.GetItemById(AssignmentID);

               /* Update the work assignment. */
               HomeworkAssignment[SiteColumns.IsAssignmentComplete] = true;
               HomeworkAssignment.Update();
               /* Need to add code to prevent DisableEventFiring to occur. */
           }
       }


    }
}

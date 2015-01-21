using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using HomeworkTrackingSystemPrj.SiteColumns;
using System.Runtime.InteropServices;
namespace EventReceivers
{
    /// <summary>
    /// List Item Events
    /// </summary>
    [Guid("C59DB2B1-6E41-426C-A329-148E05F85EEE")]
    public class SubmissionsER : SPItemEventReceiver
    {
       /// <summary>
       /// An item was updated.
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);
           bool IsAssignmentComplete = Convert.ToBoolean(properties.AfterProperties[properties.ListItem.Fields[SiteColumns.IsAssignmentComplete].InternalName]);
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

using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace HomeworkTrackingSystemPrj.EventReceivers.HomeworkAssignmentER
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class HomeworkAssignmentER : SPItemEventReceiver
    {
       /// <summary>
       /// An item was updated
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);
           
       }


    }
}

using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using HomeworkTrackingSystemPrj;
using HomeworkTrackingSystemPrj.SiteColumns;
using System.Runtime.InteropServices;
using HomeworkTrackingSystemPrj.Utility;
namespace EventReceivers
{
    [Guid("6224ACB7-C590-46DE-9C55-EE06E06A0C9D")]
    public class HomeworkAssignmentER: SPItemEventReceiver
    {
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            bool IsAssignmentComplete = Convert.ToBoolean(properties.AfterProperties[properties.ListItem.Fields[SiteColumns.IsAssignmentComplete].InternalName]);
            int HomeworkPointsReceived = Convert.ToInt16(properties.AfterProperties[properties.ListItem.Fields[SiteColumns.HomeworkAssignmentPointsRecievied].InternalName]);
            int HomeworkPointsAllowed = Convert.ToInt16(properties.AfterProperties[properties.ListItem.Fields[SiteColumns.HomeworkAssignmentPointsAllowed].InternalName]);

            if (IsAssignmentComplete)
            {
                int ClassID = Convert.ToInt16(properties.AfterProperties[properties.List.Fields[SiteColumns.Class].InternalName].ToString());
                int StudentID = Convert.ToInt16(properties.AfterProperties[properties.List.Fields[SiteColumns.Student].InternalName].ToString());
                
                /* Determine the letter grade for the assignment. */
                int RawGradeValue = (HomeworkPointsReceived / HomeworkPointsAllowed) * 100;
                string LetterGrade = Utility.GetLetterGrade(RawGradeValue);

                using(DisabledEventsScope scope = new DisabledEventsScope())
                {
                    SPListItem item = properties.ListItem;
                    item[SiteColumns.LetterGrade] = LetterGrade;
                    item.Update();
                }

                /* Get an instance of the ClassGrades list and update the students class grade. */
            }
        }
    }
}

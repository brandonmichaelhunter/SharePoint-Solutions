using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace HomeworkTrackingSystemPrj.Features.KS.ProvisionArtifacts
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("5c273a5a-b9ab-4d33-af61-8cd3d8db649b")]
    public class KSEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb spWeb = (SPWeb)properties.Feature.Parent;
            try
            {
                //System.Diagnostics.Debugger.Launch();
                /* Provision site columns, content types and list.  */
                using (SPWeb spNewWeb = spWeb.Site.OpenWeb(spWeb.ID))
                {
                    #region Create Site Columns
                    SiteColumns.SiteColumns.ProvisionSiteColumns(spNewWeb);
                    #endregion

                    #region Class Section
                    ContentTypes.Class.ProvisionContentType(spNewWeb);
                    Lists.Classes.AssociateCTWithList(spNewWeb);
                    #endregion

                    #region Class Grades Section
                    ContentTypes.ClassGrades.ProvisionContentType(spNewWeb);
                    Lists.ClassGrades.AssociateCTWithList(spNewWeb);
                    #endregion

                    #region Student Section
                    ContentTypes.Student.ProvisionContentType(spNewWeb);
                    Lists.Students.AssociateCTWithList(spNewWeb);
                    #endregion

                    #region Homework Assignments Section
                    ContentTypes.HomeworkAssignments.ProvisionContentType(spNewWeb);
                    Lists.HomeworkAssignments.AssociateCTWithList(spNewWeb);
                    #endregion

                    #region Submission Section
                    ContentTypes.Submission.ProvisionContentType(spNewWeb);
                    Lists.Submissions.AssociateCTWithList(spNewWeb);
                    #endregion

                    #region Create Security Groups
                    Security.SecurityGroups.ProvisionSecurityGroups(spNewWeb);
                    Security.SecurityGroups.GrantSecurityGroupsSiteLevelPermissions(spNewWeb);
                    Security.SecurityGroups.GrantSecurityGroupsListLevelPermissions(spNewWeb);
                    #endregion
                }
            }
            catch (Exception)
            {
                
                throw;
            }   
            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}

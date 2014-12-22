using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using HomeworkTrackingSystemPrj.Utility;

namespace Security
{
    public static class SecurityGroups
    {
        enum SecurityGroup { Teachers, Students, Parents, Adminstrators };

        public static void ProvisionSecurityGroups(SPWeb spWeb)
        {
            SPGroupCollection spGroupColl = spWeb.SiteGroups;
            SPUser spDefaultUser = spWeb.Users["SPDOM\\Administrator"];
            SPUser spGroupOwner = spWeb.Users["SPDOM\\Administrator"];
            
            //TODO: Add the groups to the site and to their respective lists.
            SPGroup AdmingGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.AdminstratorSecurityGroupName).FirstOrDefault();
            if (AdmingGroup != null)
            {
                spGroupColl.Add(AppConstants.AdminstratorSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for adminstrator users.");
            }

            SPGroup TeacherGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.TeacherSecurityGroupName).FirstOrDefault();
            if (TeacherGroup != null){
                spGroupColl.Add(AppConstants.TeacherSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for teacher users.");
            }

            SPGroup ParentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.ParentSecurityGroupName).FirstOrDefault();
            if (ParentGroup != null){
                spGroupColl.Add(AppConstants.ParentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for parent users.");
            }

            SPGroup StudentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.StudentSecurityGroupName).FirstOrDefault();
            if (StudentGroup !=null){
                spGroupColl.Add(AppConstants.StudentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for student users.");
            }

            /* Grant groups site and list level permissions. */
            SPRoleDefinition spRoleDef = spWeb.RoleDefinitions[""];
            SPRoleAssignment customRoleAssignment = new SPRoleAssignment(null);

        }
    }
}

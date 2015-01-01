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
            SPGroup AdminGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.SchoolAdminstratorSecurityGroupName).FirstOrDefault();
            if (AdminGroup == null){
                spGroupColl.Add(AppConstants.SchoolAdminstratorSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for school adminstrator users.");
            }

            SPGroup TeacherGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.TeacherSecurityGroupName).FirstOrDefault();
            if (TeacherGroup == null){
                spGroupColl.Add(AppConstants.TeacherSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for teacher users.");
            }

            SPGroup ParentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.ParentSecurityGroupName).FirstOrDefault();
            if (ParentGroup == null){
                spGroupColl.Add(AppConstants.ParentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for parent users.");
            }

            SPGroup StudentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.StudentSecurityGroupName).FirstOrDefault();
            if (StudentGroup == null){
                spGroupColl.Add(AppConstants.StudentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for student users.");
            }
            
            /* Grant groups site and list level permissions. */
            if (!CheckSiteLevelPermissions(spWeb, SPRoleType.Administrator, AdminGroup))
            {
                if (!spWeb.IsRootWeb)
                {
                    
                    if (spWeb.HasUniqueRoleAssignments == false)
                    {
                        spWeb.BreakRoleInheritance(true);
                    }

                    SPRoleAssignment roleAssign = new SPRoleAssignment(AdminGroup);
                    SPRoleDefinition roleDef = spWeb.RoleDefinitions.GetByType(SPRoleType.Administrator);
                    roleAssign.RoleDefinitionBindings.Add(roleDef);
                    spWeb.RoleAssignments.Add(roleAssign);
                                        
                    
                    
                }
            }
        }
        private static bool CheckSiteLevelPermissions(SPWeb spWeb, SPRoleType spRoleType, SPPrincipal spUser)
        {
            bool IsSiteLevlUser = false;
            foreach (SPRoleAssignment roleAssignment in spWeb.RoleAssignments)
            {
                if (roleAssignment.Member.Name == spUser.Name)
                {
                    SPRoleDefinition currentRoleDef = spWeb.RoleDefinitions.GetByType(spRoleType);
                    SPRoleDefinitionBindingCollection spRoleDefs = roleAssignment.RoleDefinitionBindings;
                    foreach (SPRoleDefinition spRoleDef in spRoleDefs)
                    {
                        if (spRoleDef == currentRoleDef)
                        {
                            IsSiteLevlUser = true;
                        }
                    }
                }
            }
            return IsSiteLevlUser;
        }
    }
}

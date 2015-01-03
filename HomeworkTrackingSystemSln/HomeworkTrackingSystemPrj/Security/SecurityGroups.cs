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
        enum SecurityGroup { HTS_Teachers, HTS_Students, HTS_Parents, HTS_Adminstrators };
        /// <summary>
        /// Provisions and grants the School Administrator, Teachers and Parents security group to the current site.
        /// </summary>
        /// <param name="spWeb">Represents an instance of the Homework Tracking Site as a SPWeb object. </param>
        public static void ProvisionSecurityGroups(SPWeb spWeb)
        {
            SPGroupCollection spGroupColl = spWeb.SiteGroups;
            SPUser spDefaultUser = spWeb.Users["SPDOM\\Administrator"];
            SPUser spGroupOwner = spWeb.Users["SPDOM\\Administrator"];

            SPGroup AdminGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.HTS_SchoolAdminstratorSecurityGroupName).FirstOrDefault();
            if (AdminGroup == null)
            {
                spGroupColl.Add(AppConstants.HTS_SchoolAdminstratorSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for school adminstrator users.");
            }

            SPGroup TeacherGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.HTS_TeacherSecurityGroupName).FirstOrDefault();
            if (TeacherGroup == null)
            {
                spGroupColl.Add(AppConstants.HTS_TeacherSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for teacher users.");
            }

            SPGroup ParentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.HTS_ParentSecurityGroupName).FirstOrDefault();
            if (ParentGroup == null)
            {
                spGroupColl.Add(AppConstants.HTS_ParentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for parent users.");
            }

            SPGroup StudentGroup = spWeb.SiteGroups.Cast<SPGroup>().Where(a => a.Name == AppConstants.HTS_StudentSecurityGroupName).FirstOrDefault();
            if (StudentGroup == null)
            {
                spGroupColl.Add(AppConstants.HTS_StudentSecurityGroupName, spGroupOwner, spDefaultUser, "This group is only for student users.");
            }
        }
        public static void GrantSecurityGroupsSiteLevelPermissions(SPWeb spWeb)
        {
            SPGroup AdminGroup = spWeb.SiteGroups[AppConstants.HTS_SchoolAdminstratorSecurityGroupName];
            GrantGroupSiteLevelPermissions(AdminGroup, SPRoleType.Administrator, spWeb);

            SPGroup TeacherGroup = spWeb.SiteGroups[AppConstants.HTS_TeacherSecurityGroupName];
            GrantGroupSiteLevelPermissions(TeacherGroup, SPRoleType.Contributor, spWeb);

            SPGroup ParentGroup = spWeb.SiteGroups[AppConstants.HTS_ParentSecurityGroupName];
            GrantGroupSiteLevelPermissions(ParentGroup, SPRoleType.Reader, spWeb);

            SPGroup StudentGroup = spWeb.SiteGroups[AppConstants.HTS_StudentSecurityGroupName];
            GrantGroupSiteLevelPermissions(StudentGroup, SPRoleType.Reader, spWeb);

        }
        public static void GrantSecurityGroupsListLevelPermissions(SPWeb spWeb)
        {
            SPGroup AdminGroup = spWeb.SiteGroups[AppConstants.HTS_SchoolAdminstratorSecurityGroupName];
            GrantGroupListLevelPermisisons(AdminGroup, SPRoleType.Administrator, Lists.HomeworkAssignments.GetList(spWeb));
            GrantGroupListLevelPermisisons(AdminGroup, SPRoleType.Administrator, Lists.Submissions.GetList(spWeb));
            GrantGroupListLevelPermisisons(AdminGroup, SPRoleType.Administrator, Lists.Classes.GetList(spWeb));
            GrantGroupListLevelPermisisons(AdminGroup, SPRoleType.Administrator, Lists.ClassGrades.GetList(spWeb));
            GrantGroupListLevelPermisisons(AdminGroup, SPRoleType.Administrator, Lists.Students.GetList(spWeb));

            SPGroup TeacherGroup = spWeb.SiteGroups[AppConstants.HTS_TeacherSecurityGroupName];
            GrantGroupListLevelPermisisons(TeacherGroup, SPRoleType.Contributor, Lists.HomeworkAssignments.GetList(spWeb));
            GrantGroupListLevelPermisisons(TeacherGroup, SPRoleType.Contributor, Lists.Submissions.GetList(spWeb));
            GrantGroupListLevelPermisisons(TeacherGroup, SPRoleType.Contributor, Lists.ClassGrades.GetList(spWeb));
            GrantGroupListLevelPermisisons(TeacherGroup, SPRoleType.Reader, Lists.Classes.GetList(spWeb));
            GrantGroupListLevelPermisisons(TeacherGroup, SPRoleType.Reader, Lists.Students.GetList(spWeb));

            SPGroup ParentGroup = spWeb.SiteGroups[AppConstants.HTS_ParentSecurityGroupName];
            GrantGroupListLevelPermisisons(ParentGroup, SPRoleType.Reader, Lists.HomeworkAssignments.GetList(spWeb));
            GrantGroupListLevelPermisisons(ParentGroup, SPRoleType.Reader, Lists.Submissions.GetList(spWeb));
            GrantGroupListLevelPermisisons(ParentGroup, SPRoleType.Reader, Lists.Classes.GetList(spWeb));
            GrantGroupListLevelPermisisons(ParentGroup, SPRoleType.Reader, Lists.ClassGrades.GetList(spWeb));
            GrantGroupListLevelPermisisons(ParentGroup, SPRoleType.Reader, Lists.Students.GetList(spWeb));

            SPGroup StudentGroup = spWeb.SiteGroups[AppConstants.HTS_StudentSecurityGroupName];
            GrantGroupListLevelPermisisons(StudentGroup, SPRoleType.Reader, Lists.HomeworkAssignments.GetList(spWeb));
            GrantGroupListLevelPermisisons(StudentGroup, SPRoleType.Contributor, Lists.Submissions.GetList(spWeb));
            GrantGroupListLevelPermisisons(StudentGroup, SPRoleType.Reader, Lists.Classes.GetList(spWeb));
            GrantGroupListLevelPermisisons(StudentGroup, SPRoleType.Reader, Lists.ClassGrades.GetList(spWeb));
            GrantGroupListLevelPermisisons(StudentGroup, SPRoleType.Reader, Lists.Students.GetList(spWeb));
        }
        #region Private Methods
        /// <summary>
        /// Grants list level permissions to a target sharepoint on the HTS site.
        /// </summary>
        /// <param name="spTargetGroup">Represents an instance of a HTS security group.</param>
        /// <param name="spAssignRoleType">Represents an instance of a target HTS role type.</param>
        /// <param name="spTargetList">Represents an instance of the target list where we grant the security group permission to.</param>
        private static void GrantGroupListLevelPermisisons(SPGroup spTargetGroup, SPRoleType spAssignRoleType, SPList spTargetList)
        {

            if (spTargetList.HasUniqueRoleAssignments == false)
            {
                spTargetList.BreakRoleInheritance(true);
            }
            /* Remove the user from the role assignment. */
            spTargetList.RoleAssignments.Remove(spTargetGroup);
            SPRoleAssignment roleAssign = new SPRoleAssignment(spTargetGroup);
            SPRoleDefinition roleDef = spTargetList.ParentWeb.RoleDefinitions.GetByType(spAssignRoleType);
            roleAssign.RoleDefinitionBindings.Add(roleDef);
            spTargetList.RoleAssignments.Add(roleAssign);
        }
        
        /// <summary>
        /// Grants permissions to a group for a specific site.
        /// </summary>
        /// <param name="spGroup">Represents a SharePoint group</param>
        /// <param name="sPRoleType">Represents the permission to grant to the target group.</param>
        /// <param name="spWeb">Represents an instance of the Homework Tracking Site as a SPWeb object.</param>
        private static void GrantGroupSiteLevelPermissions(SPGroup spTargetGroup, SPRoleType sPRoleType, SPWeb spWeb)
        {
            if (!spWeb.IsRootWeb)
            {

                if (spWeb.HasUniqueRoleAssignments == false)
                {
                    spWeb.BreakRoleInheritance(true);
                }
                /* Adding the School Administration group. */
                spWeb.RoleAssignments.Remove(spTargetGroup);
                SPRoleAssignment roleAssign = new SPRoleAssignment(spTargetGroup);
                SPRoleDefinition roleDef = spWeb.RoleDefinitions.GetByType(sPRoleType);
                roleAssign.RoleDefinitionBindings.Add(roleDef);
                spWeb.RoleAssignments.Add(roleAssign);

            }
        }
        
        #region No longer use methods, but keep just in case :)
        private static bool CheckListLevelPermissions(SPList spTargetList, SPRoleType spAssignRoleType, SPGroup spTargetGroup)
        {
            bool ListLevelPermissionsExists = false;
            foreach (SPRoleAssignment roleAssignment in spTargetList.RoleAssignments)
            {
                if (roleAssignment.Member.Name == spTargetGroup.Name)
                {
                    SPRoleDefinition currentRoleDef = spTargetList.ParentWeb.RoleDefinitions.GetByType(spAssignRoleType);
                    SPRoleDefinitionBindingCollection spRoleDefs = roleAssignment.RoleDefinitionBindings;
                    foreach (SPRoleDefinition spRoleDef in spRoleDefs)
                    {
                        if (spRoleDef == currentRoleDef)
                        {
                            ListLevelPermissionsExists = true;
                        }
                    }
                }
            }
            return ListLevelPermissionsExists;
        }
        /// <summary>
        /// Verifies if the target SharePoint group was granted permissions at the site level.
        /// </summary>
        /// <param name="spWeb">Represents an instance of the Homework Tracking Site as a SPWeb object.</param>
        /// <param name="spRoleType">Represents the permission to check </param>
        /// <param name="spTargetGroup">Represents an instance of the target SharePoint group.</param>
        /// <returns>True/Fase if group was granted permissions or not.</returns>
        private static bool CheckSiteLevelPermissions(SPWeb spWeb, SPRoleType spRoleType, SPPrincipal spTargetGroup)
        {
            bool SiteLevelUser = false;
            foreach (SPRoleAssignment roleAssignment in spWeb.RoleAssignments)
            {
                if (roleAssignment.Member.Name == spTargetGroup.Name)
                {
                    SiteLevelUser = true;
                    //SPRoleDefinition currentRoleDef = spWeb.RoleDefinitions.GetByType(spRoleType);
                    //SPRoleDefinitionBindingCollection spRoleDefs = roleAssignment.RoleDefinitionBindings;
                    //foreach (SPRoleDefinition spRoleDef in spRoleDefs)
                    //{
                    //    if (spRoleDef == currentRoleDef)
                    //    {
                    //        SiteLevelUser = true;
                    //    }
                    //}
                }
            }
            return SiteLevelUser;
        }
        #endregion

        #endregion
    }
}

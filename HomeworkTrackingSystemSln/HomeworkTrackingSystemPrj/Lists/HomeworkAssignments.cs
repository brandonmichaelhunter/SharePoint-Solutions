﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Lists
{
    public static class HomeworkAssignments
    {
        private const string listName = "HomeworkAssignments";
        public static string ListName { get { return listName; } }

        public static SPList GetList(SPWeb spCurrentWeb)
        {
            return ProvisionList(spCurrentWeb);
        }
        private static SPList ProvisionList(SPWeb spCurrentWeb)
        {
            Guid spListID;
            SPList spTargetList = spCurrentWeb.Lists.TryGetList(listName);
            if (spTargetList == null)
            {
                spListID = spCurrentWeb.Lists.Add(listName, "A list that stores all homework assignments", SPListTemplateType.DocumentLibrary);
                spTargetList = spCurrentWeb.Lists[spListID];

                spTargetList.ContentTypesEnabled = true;
                spTargetList.OnQuickLaunch = false;
                spTargetList.Update();
            }
            return spTargetList;
        }

        public static void AssociateCTWithList(SPWeb spWeb)
        {
            /* Check to see if the content type exists already on the list. */
            SPList targetList = GetList(spWeb);
            if (targetList != null)
            {
                SPContentType ct = spWeb.ContentTypes[ContentTypes.HomeworkAssignments.ContentTypeID];
                SPContentTypeId ctID = targetList.ContentTypes.BestMatch(ContentTypes.HomeworkAssignments.ContentTypeID);
                if (ctID != null && !ctID.IsChildOf(ct.Id))
                {
                    targetList.ContentTypes.Add(ct);
                    targetList.Update();
                }

            }
        }
    }
}

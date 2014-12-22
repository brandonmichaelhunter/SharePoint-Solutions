using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;


namespace Lists
{
    public static class Classes 
    {
        private const string listName = "Classes";
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
                spListID = spCurrentWeb.Lists.Add(listName, "A list that stores all metadata about school classes", SPListTemplateType.GenericList);
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
                /* Check to see if ability to use content types on this list is enabled. */
                if (targetList.ContentTypesEnabled == false)
                {
                    targetList.ContentTypesEnabled = true;
                }

                SPContentType ct = spWeb.ContentTypes[ContentTypes.Class.ContentTypeID];
                SPContentTypeId ctID = targetList.ContentTypes.BestMatch(ContentTypes.Class.ContentTypeID);
                if (ctID != null && !ctID.IsChildOf(ct.Id))
                {
                    targetList.ContentTypes.Add(ct);
                    targetList.Update();
                }
                targetList.Update();
            }
        }
    }
}

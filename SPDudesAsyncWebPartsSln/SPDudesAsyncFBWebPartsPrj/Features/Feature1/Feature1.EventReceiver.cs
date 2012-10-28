using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.WebPartPages;
using SPDudesAsyncFBWebPartsPrj.SPDudesAsyncWP;
using System.Collections.Generic;
namespace SPDudesAsyncFBWebPartsPrj.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("1f94e45b-3a40-4b33-bde3-e16aaca3416d")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        private SPFile _homePage;
        protected SPWeb _currentWeb;
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                /* Retrieve an instance of the current website to the SPWeb object.*/
                SPSite siteCollection = (SPSite)properties.Feature.Parent;
                this._currentWeb = siteCollection.RootWeb;
                /* Invoke the AddWebParts method. */
                AddWebParts(_currentWeb);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message.ToString());
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                System.Diagnostics.Debugger.Launch();
                /* Retrieve an instance of the current website object through the SPWeb object. */
                SPSite siteCollection = (SPSite)properties.Feature.Parent;
                this._currentWeb = siteCollection.RootWeb;
                RemoveWebparts(_currentWeb);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message.ToString());
            }
        }
        /// <summary>
        /// Adds new web parts to the default home page.
        /// </summary>
        /// 
        private void AddWebParts(SPWeb CurrentWeb)
        {
            try
            {
                /* Retrieve an instance of the default.aspx as a SPFile object. */
                this._homePage = CurrentWeb.GetFile("default.aspx");
                /* Retrieve an instance of the SPLimitedWebPartManager object. */
                SPLimitedWebPartManager wpm = this._homePage.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared);
                /* Create an instance ofthe AsyncWebPart1 object. */
                SPDudesAsyncWP.SPDudesAsyncWP _asyncWP = new SPDudesAsyncWP.SPDudesAsyncWP();
                
                _asyncWP.Title = "Asynchronous Web Part Example (SPDudes)";
                _asyncWP.ToolTip = "Asynchronous Web Part Example (SPDudes)";
                wpm.AddWebPart(_asyncWP, "left", 0);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Removes web parts that exists on the home page.
        /// </summary>
        private void RemoveWebparts(SPWeb CurrentWeb)
        {
            try
            {
                
                SPList WebPartGallery = CurrentWeb.Lists.TryGetList("Web Part Gallery");
                if (WebPartGallery != null)
                {
                    List<SPFile> FilesToDelete = new List<SPFile>();
                    foreach (SPListItem WebPartTemplateFile in WebPartGallery.Items)
                    {
                        if (WebPartTemplateFile.File.Name.Contains("SPDudesAsyncWP"))
                        {
                            FilesToDelete.Add(WebPartTemplateFile.File);
                            break;
                        }
                    }
                    foreach (SPFile file in FilesToDelete)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       


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

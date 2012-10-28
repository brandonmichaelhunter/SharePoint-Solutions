using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.WebPartPages;
using SPDudes.WebParts;
namespace SPDudes.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("bf53addc-8273-48e3-9b7a-19fb9742cae7")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        private SPFile _homePage;
        protected SPWeb _currentWeb;
        private bool _asyncWPExists = false;
        private AsyncWP1 _asyncWP;
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                /* Retrieve an instance of the current website to the SPWeb object.*/
                SPSite siteCollection =  (SPSite)properties.Feature.Parent;
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
                /* Retrieve an instance of the current website object through the SPWeb object. */
                SPSite siteCollection = (SPSite)properties.Feature.Parent;
                this._currentWeb = siteCollection.RootWeb;
                RemoveWebparts(_currentWeb);
            }
            catch (Exception ex)
            {
                
                throw  new ApplicationException(ex.Message.ToString());
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
                this._asyncWP = new AsyncWP1();
                this._asyncWPExists = false;
                /* Loop through the web part manager to see if the web part exists. */
                foreach (WebPart webPart in wpm.WebParts)
                {
                    if (webPart.GetType().FullName == this._asyncWP.GetType().FullName)
                    {
                        _asyncWPExists = true;
                        break;
                    }
                }
                /* Add the web part to the page if not found on the page. */
                if (this._asyncWPExists == false)
                {
                    this._asyncWP.Title = "Asynchronous Web Part Example (SPDudes)";
                    this._asyncWP.ToolTip = "Asynchronous Web Part Example (SPDudes)";
                    wpm.AddWebPart(this._asyncWP, "left", 0);
                }
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
                /* Retrieve an instance of the default.aspx page. */
                this._homePage = CurrentWeb.GetFile("default.aspx");
                /* Retrieve an instance of the web part manager on the home page. */
                SPLimitedWebPartManager wpm = this._homePage.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared);
                /* Location the AsyncWP1 web part. */
                this._asyncWPExists = false;
                this._asyncWP = new AsyncWP1();
                foreach (WebPart webPart in wpm.WebParts)
                {
                    if (webPart.GetType().FullName == this._asyncWP.GetType().FullName)
                    {
                        this._asyncWPExists = true;
                        break;
                    }
                }

                if (_asyncWPExists)
                {
                    wpm.DeleteWebPart(this._asyncWP);
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

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SPDudesAsyncFBWebPartsPrj.SPDudesAsyncWP
{
    [ToolboxItemAttribute(false)]
    public class SPDudesAsyncWP : WebPart
    {
        protected override void CreateChildControls()
        {
        }
        protected string localBuffer;
        protected string outputMessage;

        protected override void OnPreRender(EventArgs e)
        {
            /* Creates an instance of the PageAsyncTask object. The fourth parameter can be used to pass any arbitrary data to the second 
             thread that is processing the asynchronus task. */
            PageAsyncTask task1 = new PageAsyncTask(Task1Begin, Task1End, Task1Timeout, null);
            /* Register an asyncronous task on the current page. */
            this.Page.RegisterAsyncTask(task1);
        }
        /// <summary>
        /// Represents the begin event handler. Calls the GetDataFromNetwork method as a action for asynchronus operation to execute.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args">Contains events data.</param>
        /// <param name="callback">References a method to be called once the asynchronus operation is complete.</param>
        /// <param name="state">Represents the state of the asynchronus operation</param>
        /// <returns>The status of asyncrouns operation.</returns>
        IAsyncResult Task1Begin(object sender, EventArgs args, AsyncCallback callback, object state)
        {
            Action func1 = new Action(GetDataFromNetwork);
            return func1.BeginInvoke(callback, state);
        }
        /// <summary>
        /// Sleeps for 3 minutes.
        /// </summary>
        void GetDataFromNetwork()
        {
            // simulate calling across network
            System.Threading.Thread.Sleep(3000);
            localBuffer = "Testing 1, 2, 3...";
        }
        /// <summary>
        /// Represents the end event handler, which is called once the asynchronus operation has completed.
        /// </summary>
        /// <param name="result">Representst he status of asynchronus operation</param>
        void Task1End(IAsyncResult result)
        {
            outputMessage = "Data from accross network:" + localBuffer;
        }
        /// <summary>
        /// This method is called in case a time out has occur. 
        /// </summary>
        /// <param name="result">The status of asyncrouns operation</param>
        void Task1Timeout(IAsyncResult result)
        {
            outputMessage = "Oooooppps, there was a timeout";
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(outputMessage);
            writer.RenderEndTag();
        }
    }
}

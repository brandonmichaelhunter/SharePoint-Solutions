using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeworkTrackingSystemPrj.SiteColumns;
using Microsoft.SharePoint;

namespace Test.SolutionArtifact
{
    class Program
    {
        static void Main(string[] args)
        {
            string siteUrl = "http://sp2010/HTS/";
            using (SPSite spSite = new SPSite(siteUrl))
            {
                using (SPWeb spWeb = spSite.OpenWeb())
                {
                    SiteColumns.ProvisionSiteColumns(spWeb);
                }
            }
        }
    }
}

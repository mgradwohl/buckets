using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Buckets
{
    class UIF
    {
        WorkItemStore wiStore;

        public bool Connect()
        {
            Uri tfsUri = new Uri("https://userfeedback.visualstudio.com/DefaultCollection");
            
            TfsTeamProjectCollection projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
            wiStore = projectCollection.GetService<WorkItemStore>();
            Project teamProject = wiStore.Projects["UIF"];
            WorkItemType wiType = teamProject.WorkItemTypes["Bug"];

            return true;
        }

        public WorkItemCollection GetBugs(uint days)
        {

            string wiqlQuery = "SELECT [System.Id], [System.WorkItemType], [System.AreaPath], [System.Title], [System.Tags], [System.State], [System.CreatedDate]" +
                                " FROM WorkItems" +
                                //" WHERE [System.State] = 'Active'" +
                                " WHERE [System.CreatedDate] > @today-" + days.ToString() +
                                " AND [System.AreaPath] UNDER 'UIF\\Internet Explorer'";


            return wiStore.Query(wiqlQuery);
        }
    }
}

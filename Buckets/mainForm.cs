using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;


namespace Buckets
{
    public partial class mainForm : Form
    {
        UIF uif = new UIF();
        WorkItemCollection bugs;
        const int days = 30;

        public mainForm()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            buttonGo.Enabled = false;

            if (uif.Connect() == false)
            {
                MessageBox.Show("Couldn't connect");
            }
            else
            {
                toolStripLabel.Text = "Connecting...";

                bugs = uif.GetBugs(days);

                toolStripProgress.ProgressBar.Value = 0;
                toolStripProgress.Maximum = bugs.Count;
                workItemCollectionBindingSource.DataSource = bugs;

                toolStripLabel.Text = "Viewing " + bugs.Count + " bugs from the last " + days + " days.";
            }
            buttonGo.Enabled = true;
        } //buttonGo_Click
    }
}

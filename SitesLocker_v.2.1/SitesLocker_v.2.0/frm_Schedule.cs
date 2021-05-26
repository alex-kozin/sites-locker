using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SitesLocker_v._2._0
{
    public partial class frm_Schedule : Form
    {
        public frm_Schedule(ref string[,] ipmas, int IpQuantity)
        {
            InitializeComponent();
            for (int i = 0; i < IpQuantity; i++)
            {
                if (!lstIp.Items.Contains(ipmas[i,1]))
                {
                    lstIp.Items.Add(ipmas[i, 1]);
                }
            }            
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstIp.SelectedItem != null)
            {
                frm_Main main = new frm_Main();
                main.Text = lstIp.SelectedItem.ToString();
                lstIp.Items.Remove(lstIp.SelectedItem);
            }
        }

        private void frm_Schedule_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

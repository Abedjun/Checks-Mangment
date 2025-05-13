using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22211513App
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You Sure?","Alert",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReciveCheck_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            frmReciveCHeck frRec = new frmReciveCHeck();
            frRec.Show();
        }

        private void btnIssuedCheck_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmIssuedCHecks friss = new frmIssuedCHecks();
            friss.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReports frmRep = new frmReports();
            frmRep.Show();
        }
    }
}

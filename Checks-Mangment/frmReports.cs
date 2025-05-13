using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _22211513App
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHome fhome = new frmHome();
            fhome.Show();
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DAFFAWI\Desktop\Study\C#\22211513App\Checks-Mangment\Database.accdb");

        private void btnIssued_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand view = new OleDbCommand("select * from IssuedCheck where DueDate = @DueDate", conn);
                view.Parameters.AddWithValue("@DueDate", DateTime.Today.ToString());

                OleDbDataAdapter da = new OleDbDataAdapter(view);
                DataSet ds = new DataSet();
                da.Fill(ds, "rep");
                dgv.DataSource = ds.Tables[0];

                dgv.Columns[0].HeaderText = "Check ID";
                dgv.Columns[1].HeaderText = "Check Number";
                dgv.Columns[2].HeaderText = "Issue Date";
                dgv.Columns[3].HeaderText = "Due Date";
                dgv.Columns[4].HeaderText = "Amount";
                dgv.Columns[5].HeaderText = "Name";
                dgv.Columns[6].HeaderText = "Bank Name";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
            }
        }

        private void btnRecive_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand view = new OleDbCommand("select * from ReciveCheck where DueDate = @DueDate", conn);
                view.Parameters.AddWithValue("@DueDate", DateTime.Today.ToString());

                OleDbDataAdapter da = new OleDbDataAdapter(view);
                DataSet ds = new DataSet();
                da.Fill(ds, "rep");
                dgv.DataSource = ds.Tables[0];

                dgv.Columns[0].HeaderText = "Check ID";
                dgv.Columns[1].HeaderText = "Check Number";
                dgv.Columns[2].HeaderText = "Recived Date";
                dgv.Columns[3].HeaderText = "Due Date";
                dgv.Columns[4].HeaderText = "Amount";
                dgv.Columns[5].HeaderText = "Name";
                dgv.Columns[6].HeaderText = "Bank Name";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmReports_Load(object sender, EventArgs e)
        {

        }
    }
}

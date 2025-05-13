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
    public partial class frmIssuedCHecks : Form
    {
        public frmIssuedCHecks()
        {
            InitializeComponent();
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DAFFAWI\Desktop\Study\C#\22211513App\Checks-Mangment\Database.accdb");
        private void clearAll()
        {
            txtChID.Clear();
            txtChNum.Clear();
            txtName.Clear();
            txtBank.Clear();
            txtAmount.Clear();
            dgv.Rows.Clear();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHome fhome = new frmHome();
            fhome.Show();
        }

        private void frmIssuedCHecks_Load(object sender, EventArgs e)
        {
            txtName.KeyPress += TxtName_KeyPress;
            txtName.Leave += TxtName_Leave;
            txtChID.KeyPress += TxtChID_KeyPress;
        }
        private void TxtChID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                TextBox txtbox = sender as TextBox;
                errorProvider1.SetError(txtbox, "This Field Accept Only Digits");
                txtbox.Focus();
                e.Handled = true;
            }
            else errorProvider1.Clear();
        }

        private void TxtName_Leave(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(txtbox.Text))
            {
                errorProvider1.SetError(txtbox, "You Cannot Leave it Empty");
                txtbox.Focus();
            }
            else errorProvider1.Clear();
        }

        private void TxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                TextBox txtbox = sender as TextBox;
                errorProvider1.SetError(txtbox, "This Field Accept only Letters");
                txtbox.Focus();
                e.Handled = true;
            }
            else errorProvider1.Clear();
        }

  

       
        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            if (txtChID.Text == "" || txtBank.Text == "" || txtAmount.Text == "" || txtName.Text == "" || txtChNum.Text == "")
            {
                MessageBox.Show("There Is Fields Empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                OleDbCommand checkCmd = new OleDbCommand("SELECT COUNT(*) FROM IssuedCheck WHERE CheckID = @CheckID", conn);
                checkCmd.Parameters.AddWithValue("@CheckID", txtChID.Text);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();
                conn.Close();

                if (count > 0)
                {
                    MessageBox.Show("There Is A check ID With The same Number .", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (dtIssuedDate.Value >= dtDueDate.Value)
                {
                    MessageBox.Show("You cannot but the Due Date before or Equal the recive date.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                try
                {
                    OleDbCommand insertcmd = new OleDbCommand("INSERT INTO IssuedCheck VALUES (@CheckID, @CheckNumber, @IssuDate, @DueDate, @Amount, @PaName,@Bank)", conn);
                    insertcmd.Parameters.Add("@CheckID", OleDbType.Integer).Value = int.Parse(txtChID.Text);
                    insertcmd.Parameters.Add("@CheckNumber", OleDbType.Integer).Value = int.Parse(txtChNum.Text);
                    insertcmd.Parameters.Add("@IssuDate", OleDbType.Date).Value = dtIssuedDate.Value;
                    insertcmd.Parameters.Add("@DueDate", OleDbType.Date).Value = dtDueDate.Value;
                    insertcmd.Parameters.Add("@Amount", OleDbType.Currency).Value = decimal.Parse(txtAmount.Text);
                    insertcmd.Parameters.Add("@PaName", OleDbType.VarChar).Value = txtName.Text;
                    insertcmd.Parameters.Add("@Bank", OleDbType.VarChar).Value = txtBank.Text;
                    conn.Open();
                    int insertRows = insertcmd.ExecuteNonQuery();
                    conn.Close();
                    if (insertRows > 0)
                    {
                        MessageBox.Show("Inserted successfully");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Inserted Faild");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtChID.Text == "" || txtBank.Text == "" || txtAmount.Text == "" || txtName.Text == "" || txtChNum.Text == "")
            {
                MessageBox.Show("There Is Fields Empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (dtIssuedDate.Value >= dtDueDate.Value)
                {
                    MessageBox.Show("You cannot but the Due Date before or Equal the recive date.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                try
                {
                    OleDbCommand updateCmd = new OleDbCommand("UPDATE IssuedCheck SET CheckNumber = @CheckNumber, IssuDate = @IssuDate, DueDate = @DueDate, Amount = @Amount, PaName = @PaName, Bank = @Bank WHERE CheckID = @CheckID", conn);

                    updateCmd.Parameters.Add("@CheckNumber", OleDbType.Integer).Value = int.Parse(txtChNum.Text);
                    updateCmd.Parameters.Add("@IssuDate", OleDbType.Date).Value = dtIssuedDate.Value;
                    updateCmd.Parameters.Add("@DueDate", OleDbType.Date).Value = dtDueDate.Value;
                    updateCmd.Parameters.Add("@Amount", OleDbType.Currency).Value = decimal.Parse(txtAmount.Text);
                    updateCmd.Parameters.Add("@PaName", OleDbType.VarChar).Value = txtName.Text;
                    updateCmd.Parameters.Add("@Bank", OleDbType.VarChar).Value = txtBank.Text;
                    updateCmd.Parameters.Add("@CheckID", OleDbType.Integer).Value = int.Parse(txtChID.Text);



                    conn.Open();
                    int updatedRows = updateCmd.ExecuteNonQuery();
                    conn.Close();

                    if (updatedRows > 0)
                    {
                        MessageBox.Show("updated successfully.");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("No check ID found with that ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtChID.Text == "")
            {
                MessageBox.Show("Please Enter The Check ID", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            else
            {

                DialogResult res = MessageBox.Show("Are You Sure?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        OleDbCommand deleteCmd = new OleDbCommand("DELETE FROM IssuedCheck WHERE CheckID = @CheckID", conn);
                        deleteCmd.Parameters.Add("@CheckID", OleDbType.Integer).Value = int.Parse(txtChID.Text);
                        conn.Open();
                        int DeletedRows = deleteCmd.ExecuteNonQuery();
                        conn.Close();

                        if (DeletedRows > 0)
                        {
                            MessageBox.Show("deleted successfully.");
                            clearAll();
                        }
                        else
                        {
                            MessageBox.Show("No check found with that ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    
                }



            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand viewallcmd = new OleDbCommand("select * from IssuedCheck", conn);
                OleDbDataAdapter da = new OleDbDataAdapter(viewallcmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "IChk");
                dgv.DataSource = ds.Tables[0];
                DataTable tab = ds.Tables["IChk"];
                if (tab.Rows.Count == 0)
                {
                    MessageBox.Show("There Is no Fields To Show");
                    return;
                }
                dgv.Columns[0].HeaderText = "Check ID";
                dgv.Columns[1].HeaderText = "Check Number";
                dgv.Columns[2].HeaderText = "Issued Date";
                dgv.Columns[3].HeaderText = "Due Date";
                dgv.Columns[4].HeaderText = "Amount";
                dgv.Columns[5].HeaderText = "Name";
                dgv.Columns[6].HeaderText = "Bank";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            
        }
    }
}

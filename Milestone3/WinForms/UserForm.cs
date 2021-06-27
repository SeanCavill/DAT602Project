using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }


        public void EditUser(string userName)
        {
            btnDelete.Visible = true;
            var User = (DBAccess.FindPlayer(userName));
            
            foreach (DataRow aRow in User.Tables[0].Rows)
            {
                txtName.Enabled = false;
                txtName.Text = aRow["userName"].ToString();
                txtEmail.Text = aRow["email"].ToString();
                txtPassword.Text = aRow["password"].ToString();
                if(aRow["isLocked"].ToString() == "1")
                {
                    cbLocked.Checked = true;
                }
                if (aRow["isAdministrator"].ToString() == "1")
                {
                    cbAdmin.Checked = true;
                    cbAdmin.Enabled = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string result = DBAccess.DeletePlayer(txtName.Text);
            if (result == "Deleted")
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                Close();
            }
            else
            {
                lblError.Text = result;

            }
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            string result = DBAccess.EditPlayer(txtName.Text, txtEmail.Text, txtPassword.Text, cbAdmin.Checked, cbLocked.Checked);
            if (result == "Success")
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                Close();
            }
            else
            {
                lblError.Text = result;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            Close();
        }
    }
}

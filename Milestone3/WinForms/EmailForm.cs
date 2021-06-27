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
    public partial class EmailForm : Form
    {
        public EmailForm(string pUserName, string pPassword)
        {
            InitializeComponent();
            _username = pUserName;
            _password = pPassword;

        }

        private string _username;
        private string _password;


        private void button1_Click(object sender, EventArgs e)
        {

            string result = DBAccess.RegisterPlayer(_username, txtEmail.Text, _password);
            if(result == "registered " + _username)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();

            }
            else {
                lblError.Text = result;

            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EmailForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

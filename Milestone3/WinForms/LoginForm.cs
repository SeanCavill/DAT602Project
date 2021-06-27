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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string result = DBAccess.LoginPlayer(txtUserName.Text, txtPassword.Text);
            if (result == (txtUserName.Text + " Doesn't exist."))
                if(txtUserName.Text.Length >= 3 && txtPassword.Text.Length >= 5)
                {
                    using (EmailForm emailForm = new EmailForm(txtUserName.Text, txtPassword.Text))
                    {
                        if (emailForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            DBAccess.LoginPlayer(txtUserName.Text, txtPassword.Text);
                            Login(txtUserName.Text);
                            
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Name must be longer than 3 characters,  password must be longer than 5.";
                }

         
            else if(result == "Success")
            {
                Login(txtUserName.Text);
                          
            }

            else
            {
                lblMessage.Text = result;
            }


        }
        private void Login(string username)
        {

            DBAccess.Username = username;
            LobbyForm lobbyForm = new LobbyForm();
            lobbyForm.Show();
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}

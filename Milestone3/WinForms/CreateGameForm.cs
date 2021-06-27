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
    public partial class CreateGameForm : Form
    {
        public CreateGameForm()
        {
            InitializeComponent();

        }
        string _selectedText = "small";

        public string SelectedText { get => _selectedText; set => _selectedText = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = DBAccess.CreateGame(txtGameName.Text, SelectedText);
            if (result == "Created Game " + txtGameName.Text)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();

            }
            else
            {
                lblResult.Text = result;

            }

        }

        private void rbSmall_CheckedChanged(object sender, EventArgs e)
        {

            SelectedText = rbSmall.Text;
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            SelectedText = rbMedium.Text;
        }

        private void rbLarge_CheckedChanged(object sender, EventArgs e)
        {
            SelectedText = rbLarge.Text;
        }
    }
    
 }
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
    public partial class LobbyForm : Form
    {
        string _selectedGame;
        public LobbyForm()
        {
            InitializeComponent();
            this.Text = ("Logged in as " + DBAccess.Username);
            UpdateData();
           
        }

        public void UpdateData()
        {
            GetPlayers();
            GetActiveGames();
        }


        private void GetPlayers()
        {
            lstBoxPlayers.Items.Clear();
            var playerList = DBAccess.GetOnlinePlayers();
            foreach (DataRow aRow in playerList.Tables[0].Rows)
            {
                lstBoxPlayers.Items.Add(aRow["userName"]);
            }

        }

        private void GetActiveGames()
        {
            lstActiveGames.Items.Clear();
            var gamesList = DBAccess.GetActiveGames();
            foreach (DataRow aRow in gamesList.Tables[0].Rows)
            {
                lstActiveGames.Items.Add(aRow["name"]);
            }

        }
        private void LobbyForm_Load(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(DBAccess.AdminAcess(DBAccess.Username) == "Admin")
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You Do not have admin access");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBAccess.LogoutPlayer();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (CreateGameForm createForm = new CreateGameForm())
            {
                if (createForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UpdateData();

                }
            }

        }

        private void lstActiveGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstActiveGames.SelectedItem != null)
            {
                _selectedGame = lstActiveGames.SelectedItem.ToString();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (_selectedGame != null)
            {
                DBAccess.GameName = _selectedGame;
                GameForm gameForm = new GameForm();
                gameForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please Select a game first");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}

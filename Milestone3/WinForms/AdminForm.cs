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
    public partial class AdminForm : Form
    {

        string _selectedPlayer;
        string _selectedGame;


        public AdminForm()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        

        private void UpdateDisplay()
        {
            lstPlayers.Items.Clear();
            var Players = DBAccess.GetAllPlayers();
            foreach (DataRow aRow in Players.Tables[0].Rows)
            {
                lstPlayers.Items.Add(aRow["userName"]);
            }

            lstGames.Items.Clear();
            var Games = DBAccess.GetAllGames();
            foreach (DataRow aRow in Games.Tables[0].Rows)
            {
                lstGames.Items.Add(aRow["name"]);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LobbyForm lobbyForm = new LobbyForm();
            lobbyForm.Show();
            Close();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGames.SelectedItem != null)
            {
                _selectedGame = lstGames.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_selectedGame != null)
            {
                DBAccess.DeleteGame(_selectedGame);
                UpdateDisplay();
            }
            else
            {
                MessageBox.Show("Please Select a game first");
                UpdateDisplay();
            }
        }

        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItem != null)
            {
                _selectedPlayer = lstPlayers.SelectedItem.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_selectedPlayer != null)
            {
                UserForm userForm = new UserForm();
                userForm.EditUser(_selectedPlayer);
                userForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please Select a player to edit");
                UpdateDisplay();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
            Close();
        }
    }
}

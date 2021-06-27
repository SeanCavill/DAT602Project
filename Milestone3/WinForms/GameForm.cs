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
    public partial class GameForm : Form
    {
        private static GameForm _gameForm = new GameForm();
        private static int _xSize;
        private static int _ySize;
        private static int _xHome;
        private static int _yHome;
        private static string _mapName;
        private static string _selectedItem;


        public GameForm()
        {
            InitializeComponent();
            Run();
        }

        public void Run()
        {

            DBAccess.JoinGame(DBAccess.Username, DBAccess.GameName);
            var Map = DBAccess.MapSize(DBAccess.GameName);
            foreach (DataRow aRow in Map.Tables[0].Rows)
            {
                _mapName = (aRow["name"].ToString());
                _xSize = Int16.Parse(aRow["xLength"].ToString());
                _ySize = Int16.Parse(aRow["yLength"].ToString());
                _xHome = Int16.Parse(aRow["xHome"].ToString());
                _yHome = Int16.Parse(aRow["yHome"].ToString());

            }


            int ButtonHeight;
            int ButtonWidth;

            switch (_mapName)
            {
                case "Small":
                    ButtonWidth = 40;
                    ButtonHeight = 40;
                    break;
                case "Medium":
                    ButtonWidth = 30;
                    ButtonHeight = 30;
                    break;
                case "Large":
                    ButtonWidth = 25;
                    ButtonHeight = 25;
                    break;
                default:
                    ButtonWidth = 25;
                    ButtonHeight = 25;
                    break;

            }
            
            int Distance = 20;
            int start_x = 10;
            int start_y = 10;
            

            for (int y = 1; y <= _ySize; y++)
            {
                for (int x = 1; x <= _xSize; x++)
                {
                    Button newButton = new Button();
                    newButton.Top = start_x + (y * ButtonHeight + Distance);
                    newButton.Left = start_y + (x * ButtonWidth + Distance);
                    newButton.Width = ButtonWidth;
                    newButton.Height = ButtonHeight;
                    //newButton.Text = "X: " + x.ToString() + " Y: " + y.ToString();
                    newButton.Name = (x.ToString() + "," + y.ToString());
                    newButton.Tag = "hidden";

                    newButton.Click += new EventHandler(button_Click);
                    Controls.Add(newButton);
                    
                }

            }
            Button homeTile = (Button)Controls[(_xHome + "," + _yHome)];
            homeTile.Enabled = false;
            homeTile.FlatStyle = FlatStyle.Flat;
            UpdateDisplay();

            


        }
        private static void LoadItems()
        {
            
        }
        private void button_Click(object sender, EventArgs eventArgs)
        {

            Button button = ((Button)sender);
            string[] coords = button.Name.Split(',');
            int x = Int16.Parse(coords[0]);
            int y = Int16.Parse(coords[1]);


            string result = DBAccess.MovePlayer(DBAccess.Username, DBAccess.GameName, x, y);
            if (result == DBAccess.Username + " Moved successfully")
            {

                button.Tag = "Active";
                lblResult.Text = ("moved successfully to tile co-ords " + button.Name);

            }
            else if (result == DBAccess.Username + " Moved successfully bomb")
            {

                button.Tag = "Active";
                lblResult.Text = ("Oh no the tile you moved to had a bomb on it. Minus score. " + button.Name);



            }
            else
            {

                lblResult.Text = result;
            }
            UpdateDisplay();

        }
            private void UpdateDisplay() { 
            //
            var Tile = DBAccess.TileVisibility(DBAccess.GameName);
            foreach (DataRow aRow in Tile.Tables[0].Rows)
            {
                string tile = aRow["isVisible"].ToString();
                if (tile == "1")
                {
                    Button visibleButton = (Button)Controls[(aRow["xValue"].ToString() + "," + aRow["yValue"].ToString())];
                    visibleButton.Text = aRow["number"].ToString();
                    visibleButton.Tag = "visible";
                    visibleButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    

                }

            }
            var Bomb = DBAccess.TileBombs(DBAccess.GameName);
            foreach (DataRow aRow in Bomb.Tables[0].Rows)
            {

                Button bombButton = (Button)Controls[(aRow["xValue"].ToString() + "," + aRow["yValue"].ToString())];
                if (bombButton.Tag.ToString() == "visible")
                {
                    bombButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                }


            }
            var Item = DBAccess.TileItems(DBAccess.GameName);
            foreach (DataRow aRow in Item.Tables[0].Rows)
            {

                Button tileButton = (Button)Controls[(aRow["xValue"].ToString() + "," + aRow["yValue"].ToString())];
                if (tileButton.Tag.ToString() == "visible")
                {
                    tileButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#D4AF37");
                }
            }

            var GamePlayers = DBAccess.GetGamePlayers(DBAccess.GameName);
            foreach (DataRow aRow in GamePlayers.Tables[0].Rows)
            {
                
                Button gamePlayerButton = (Button)Controls[(aRow["tileXValue"].ToString() + "," + aRow["tileYValue"].ToString())];
                gamePlayerButton.BackColor = System.Drawing.ColorTranslator.FromHtml(aRow["colour"].ToString());
            }

            var User = DBAccess.GetStartingPosition(DBAccess.Username, DBAccess.GameName);
            foreach (DataRow aRow in User.Tables[0].Rows)
            {
                Button playerButton = (Button)Controls[(aRow["tileXValue"].ToString() + "," + aRow["tileYValue"].ToString())];
                playerButton.BackColor = System.Drawing.ColorTranslator.FromHtml(aRow["colour"].ToString());

            }

            UpdateChat();
            UpdateInventory();
            UpdatePlayers();

        }


        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            
            DBAccess.SendMessage(DBAccess.Username, txtMessage.Text);
            UpdateChat();
        }

        private void UpdatePlayers()
        {
            lstPlayers.Items.Clear();
            var GamePlayers = DBAccess.GetGamePlayers(DBAccess.GameName);
            foreach (DataRow aRow in GamePlayers.Tables[0].Rows)
            {
                ListViewItem player = new ListViewItem();
                player.ForeColor = System.Drawing.ColorTranslator.FromHtml(aRow["colour"].ToString());
                player.Text = (aRow["userName"] + ": " + aRow["score"]);
                lstPlayers.Items.Add(player);
            }
        }

        private void UpdateChat()
        {
            lstChat.Items.Clear();
            var Chat = DBAccess.GetChat();
            foreach (DataRow aRow in Chat.Tables[0].Rows)
            {
                
                lstChat.Items.Add(aRow["userName"] + ": " + aRow["text"]);
            }
        }

        private void UpdateInventory()
        {
            lstInventory.Items.Clear();
            var Items = DBAccess.GetItems(DBAccess.Username, DBAccess.GameName);

            foreach (DataRow aRow in Items.Tables[0].Rows)
            {
                lstInventory.Items.Add(aRow["itemName"] + "(" + aRow["quantity"] + ")");
            }
        }

        private void btnUseItem_Click(object sender, EventArgs e)
        {

            if (_selectedItem != null)
                {
                string[] item = _selectedItem.Split('(');
                string itemName = item[0];
                string result = DBAccess.ConsumeItem(DBAccess.Username, DBAccess.GameName, itemName);
                UpdateDisplay();
                lblResult.Text = result;
                }
        }

        private void lstInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (lstInventory.SelectedItem != null)
                {
                _selectedItem = lstInventory.SelectedItem.ToString();
                }
            
        }

        private void btnLobby_Click(object sender, EventArgs e)
        {
            DBAccess.LeaveGame(DBAccess.Username, DBAccess.GameName);
            LobbyForm lobbyForm = new LobbyForm();
            lobbyForm.Show();
            Hide();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}

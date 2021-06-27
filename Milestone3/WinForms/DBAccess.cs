using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WinForms
{
    public class DBAccess
    {
        

        private static string connectionString
        {
            //Set connection details here
            get { return "Server=localhost;Port=3306;Database=DAT602SeanCavill;Uid=root;password=;"; }

        }

        private static MySqlConnection _mySqlConnection = null;

        private static string _username;
        private static string _mapName;
        private static string _gameName;

        public static MySqlConnection mySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                {
                    _mySqlConnection = new MySqlConnection(connectionString);
                }

                return _mySqlConnection;

            }
        }

        public static string Username { get => _username; set => _username = value; }
        public static string MapName { get => _mapName; set => _mapName = value; }
        public static string GameName { get => _gameName; set => _gameName = value; }


        //tests if connection to MYSQL is open
        public static String TestConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return "Successfully connected to the database";
                }
                catch (Exception ex)
                {
                    return (":( Cannot connect to the database: " + ex.Message);
                }
            }
        }

        //public static void testingRead()
        //{

        //    MySqlCommand cmd = new MySqlCommand("GetChat", mySqlConnection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    mySqlConnection.Open();
        //    using (MySqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            testform.Instance.lstBox.Items.Add(reader["text"].ToString());
        //        }
        //        reader.Close();
        //    }
        //    mySqlConnection.Close();
                
            
        //}

        //Finds a player by username
        public static DataSet FindPlayer(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call FindPlayer(@UserName)", parUserName);

            return aDataSet;
        }

        public static DataSet GetStartingPosition(string pUserName, string pGameName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;
            parGameName.Value = pGameName;

            parList.Add(parUserName);
            parList.Add(parGameName);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetStartingPosition(@UserName, @GameName)", parList.ToArray());

            return aDataSet;
        }

        //gets the size of the map
        public static DataSet MapSize(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call MapSize(@GameName)", parGameName);

            return aDataSet;
        }

        //registers player
        public static String RegisterPlayer(string pUserName, string pEmail, string pPassword)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
            var parPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 25);

            parUserName.Value = pUserName;
            parEmail.Value = pEmail;
            parPassword.Value = pPassword;

            parList.Add(parUserName);
            parList.Add(parEmail);
            parList.Add(parPassword);
    
            try
            {
                var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call RegisterPlayer(@UserName, @Email, @Password)", parList.ToArray());

                return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
            }
            catch (MySqlException e)
            {
                return (e.Message);
            }
        }

        //Logs a player in
        public static String LoginPlayer(string pUserName, string pPassword)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 25);

            parUserName.Value = pUserName;
            parPassword.Value = pPassword;

            parList.Add(parUserName);
            parList.Add(parPassword);


            try
            {
                var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call LoginPlayer(@UserName, @Password)", parList.ToArray());
                return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
            }
            catch (MySqlException e)
            {
                return (e.Message);
            }
        }

        //logs a player out
        public static String LogoutPlayer()
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = Username;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call LogoutPlayer(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //checks if user has admin privileges
        public static String AdminAcess(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call AdminAccess(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //edits a player(will be done only by admin)
        public static String EditPlayer(string pUserName, string pNewEmail, string pNewPassword, bool pNewAdminStatus, bool pNewLockedStatus)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parNewEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
            var parNewPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 25);
            var parNewAdminStatus = new MySqlParameter("@AdminStatus", MySqlDbType.Bit);
            var parNewLockedStatus = new MySqlParameter("@LockedStatus", MySqlDbType.Bit);

            parUserName.Value = pUserName;
            parNewEmail.Value = pNewEmail;
            parNewPassword.Value = pNewPassword;
            parNewAdminStatus.Value = pNewAdminStatus;
            parNewLockedStatus.Value = pNewLockedStatus;

            parList.Add(parUserName);
            parList.Add(parNewEmail);
            parList.Add(parNewPassword);
            parList.Add(parNewAdminStatus);
            parList.Add(parNewLockedStatus);



            try
            {
                var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call EditPlayer(@UserName, @Email, @Password, @AdminStatus, @LockedStatus)", parList.ToArray());

                return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
            }
            catch (Exception ex)
            {
                return ("Failed: " + ex.Message);
            }
        }

        //deletes a player(will be done only by admin)
        public static String DeletePlayer(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;

            try
            {

                var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call DeletePlayer(@UserName)", parUserName);

                return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
            }
            catch (Exception ex)
            {
                return ("Failed: " + ex.Message);
            }
        }

        //edits a game(will be done only by admin)
        public static String DeleteGame(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call DeleteGame(@GameName)", parGameName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //deactiavates game
        public static String DeactivateGame(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call DeactivateGame(@GameName)", parGameName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //creating a game and all assets
        public static String CreateGame(string pGameName, string pMapName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);
            var parMapName = new MySqlParameter("@MapName", MySqlDbType.VarChar, 6);


            parGameName.Value = pGameName;
            parMapName.Value = pMapName;


            parList.Add(parGameName);
            parList.Add(parMapName);



            try
            {
                var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call CreateGame(@GameName, @MapName)", parList.ToArray());

                return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
            }
            catch (MySqlException e)
            {
                return (e.Message);
            }
        }
        //joining a game
        public static String JoinGame(string pPlayerName, string pGameName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);


            parUserName.Value = pPlayerName;
            parGameName.Value = pGameName;


            parList.Add(parUserName);
            parList.Add(parGameName);





            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call JoinGame(@UserName, @GameName)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //moving in game
        public static String MovePlayer(string pPlayerName, string pGameName, int pXValue, int pYValue)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);
            var parXValue = new MySqlParameter("@xValue", MySqlDbType.Int32);
            var parYValue = new MySqlParameter("@yValue", MySqlDbType.Int32);


            parUserName.Value = pPlayerName;
            parGameName.Value = pGameName;
            parXValue.Value = pXValue;
            parYValue.Value = pYValue;


            parList.Add(parUserName);
            parList.Add(parGameName);
            parList.Add(parXValue);
            parList.Add(parYValue);




            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call MovePlayer(@UserName, @GameName, @xValue, @yValue)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //using an item
        public static String ConsumeItem(string pPlayerName, string pGameName, string pItemName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parPlayerName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);
            var parItemName = new MySqlParameter("@ItemName", MySqlDbType.VarChar, 10);



            parPlayerName.Value = pPlayerName;
            parGameName.Value = pGameName;
            parItemName.Value = pItemName;



            parList.Add(parPlayerName);
            parList.Add(parGameName);
            parList.Add(parItemName);


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call ConsumeItem(@UserName, @GameName, @ItemName)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //leaving the game
        public static String LeaveGame(string pPlayerName, string pGameName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);


            parUserName.Value = pPlayerName;
            parGameName.Value = pGameName;


            parList.Add(parUserName);
            parList.Add(parGameName);





            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call LeaveGame(@UserName, @GameName)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //sending messages in game
        public static String SendMessage(string pPlayerName, string pText)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parText = new MySqlParameter("@Text", MySqlDbType.VarChar, 255);


            parUserName.Value = pPlayerName;
            parText.Value = pText;


            parList.Add(parUserName);
            parList.Add(parText);


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call SendMessage(@UserName, @Text)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        public static DataSet GetChat()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetChat()");

            return aDataSet;
        }

        public static DataSet GetAllPlayers()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetAllPlayers()");

            return aDataSet;
        }



        public static DataSet GetOnlinePlayers()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetOnlinePlayers()");

            return aDataSet;
        }
        public static DataSet GetGamePlayers(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetGamePlayers(@GameName)", parGameName);

            return aDataSet;
        }

        public static DataSet GetItems(string pPlayerName, string pGameName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parPlayerName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parPlayerName.Value = pPlayerName;
            parGameName.Value = pGameName;

            parList.Add(parPlayerName);
            parList.Add(parGameName);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetItems(@UserName, @GameName)", parList.ToArray());

            return aDataSet;
        }

        public static DataSet GetAllGames()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetAllGames()");

            return aDataSet;
        }

        public static DataSet GetActiveGames()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetActiveGames()");

            return aDataSet;
        }

        //needed to add for ms3
        public static DataSet TileVisibility(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call TileVisibility(@GameName)", parGameName);

            return aDataSet;
        }

        public static DataSet TileBombs(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call TileBombs(@GameName)", parGameName);

            return aDataSet;
        }
        public static DataSet TileItems(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call TileItems(@GameName)", parGameName);

            return aDataSet;
        }
    }
}

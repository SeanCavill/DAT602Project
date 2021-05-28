using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Milestone2Code
{
    class Test
    {
        private static string connectionString
        {
            //NOTE PASSWORD NOT SET TO ANYTHING ATM
            get { return "Server=localhost;Port=3306;Database=DAT602SeanCavill;Uid=root;password=password;"; }

        }

        private static MySqlConnection _mySqlConnection = null;
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

        //tests if connection to MYSQL is open
        public String TestConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return "yay. You are connected to database";
                }
                catch (Exception ex)
                {
                    return (":( You has error: "+ex.Message);
                }
            }
        }

        //Finds a player by username
        public String FindPlayer(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call FindPlayer(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //registers player
        public String RegisterPlayer(string pUserName, string pEmail, string pPassword)
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



            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call RegisterPlayer(@UserName, @Email, @Password)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //Logs a player in
        public String LoginPlayer(string pUserName, string pPassword)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 25);

            parUserName.Value = pUserName;
            parPassword.Value = pPassword;

            parList.Add(parUserName);
            parList.Add(parPassword);



            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call LoginPlayer(@UserName, @Password)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //logs a player out
        public String LogoutPlayer(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call LogoutPlayer(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //checks if user has admin privileges
        public String AdminAcess(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call AdminAcess(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //edits a player(will be done only by admin)
        public String EditPlayer(string pUserName, string pNewUserName, string pNewEmail, string pNewPassword, bool pNewAdminStatus, bool pNewLockedStatus)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);
            var parNewUserName = new MySqlParameter("@NewUserName", MySqlDbType.VarChar, 20);
            var parNewEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 50);
            var parNewPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 25);
            var parNewAdminStatus = new MySqlParameter("@AdminStatus", MySqlDbType.Bit);
            var parNewLockedStatus = new MySqlParameter("@LockedStatus", MySqlDbType.Bit);

            parUserName.Value = pUserName;
            parNewUserName.Value = pNewUserName;
            parNewEmail.Value = pNewEmail;
            parNewPassword.Value = pNewPassword;
            parNewAdminStatus.Value = pNewAdminStatus;
            parNewLockedStatus.Value = pNewLockedStatus;

            parList.Add(parUserName);
            parList.Add(parNewUserName);
            parList.Add(parNewEmail);
            parList.Add(parNewPassword);
            parList.Add(parNewAdminStatus);
            parList.Add(parNewLockedStatus);



            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call EditPlayer(@UserName,@NewUserName, @Email, @Password, @AdminStatus, @LockedStatus)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //deletes a player(will be done only by admin)
        public String DeletePlayer(string pUserName)
        {
            var parUserName = new MySqlParameter("@UserName", MySqlDbType.VarChar, 20);

            parUserName.Value = pUserName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call DeletePlayer(@UserName)", parUserName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        //edits a game(will be done only by admin)
        public String DeleteGame(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call DeleteGame(@GameName)", parGameName);

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //creating a game and all assets
        public String CreateGame(string pGameName, string pMapName)
        {
            List<MySqlParameter> parList = new List<MySqlParameter>();
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);
            var parMapName = new MySqlParameter("@MapName", MySqlDbType.VarChar, 6);
          

            parGameName.Value = pGameName;
            parMapName.Value = pMapName;


            parList.Add(parGameName);
            parList.Add(parMapName);
 



            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call CreateGame(@GameName, @MapName)", parList.ToArray());

            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }
        //joining a game
        public String JoinGame(string pPlayerName, string pGameName)
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
        public String MovePlayer(string pPlayerName, string pGameName, int pXValue, int pYValue)
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
        public String ConsumeItem(string pPlayerName, string pGameName, string pItemName)
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
        public String LeaveGame(string pPlayerName, string pGameName)
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
        public String SendMessage(string pPlayerName, string pText)
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

        public DataSet GetChat()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetChat()");

            return aDataSet;
        }

        public DataSet GetAllPlayers()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetAllPlayers()");

            return aDataSet;
        }

        public DataSet GetOnlinePlayers()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetOnlinePlayers()");

            return aDataSet;
        }
        public DataSet GetGamePlayers(string pGameName)
        {
            var parGameName = new MySqlParameter("@GameName", MySqlDbType.VarChar, 20);

            parGameName.Value = pGameName;

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetGamePlayers(@GameName)", parGameName);

            return aDataSet;
        }

        public DataSet GetAllGames()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetAllGames()");

            return aDataSet;
        }

        public DataSet GetActiveGames()
        {


            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call GetActiveGames()");

            return aDataSet;
        }
    }
}

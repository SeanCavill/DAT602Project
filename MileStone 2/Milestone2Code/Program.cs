using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Milestone2Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Test aTest = new Test(); //new test class

            //test db Connection
            Console.WriteLine(aTest.TestConnection());
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            // Tests Finding a User.
            Console.WriteLine("Testing Find Player");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(aTest.FindPlayer("Sean"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            // Tests registering a user procedure.
            Console.WriteLine("Testing Register Player");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(aTest.RegisterPlayer("hi", "myname", "jeff"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Logout
            Console.WriteLine("Testing Logout Player");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.LogoutPlayer("Sean"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            // Tests Logging in Procedure.
            Console.WriteLine("Testing Login Player");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.LoginPlayer("Sean", "Password1"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");


            //Tests Users Admin Acess
            Console.WriteLine("Testing Admin Access");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.AdminAcess("Sean"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Editing Player
            Console.WriteLine("Testing Edit Player");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.EditPlayer("Sean", "Sean", "NewEmail@gmail.com", "Password4", true, true));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Deleting Player
            Console.WriteLine("Testing Delete Player");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.DeletePlayer("Sarah"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Creating Game
            Console.WriteLine("Testing Creating Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.CreateGame("NewGame", "Small"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Joining Game
            Console.WriteLine("Testing Joining Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.JoinGame("Sean", "NewGame"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Move In Game
            Console.WriteLine("Testing Moving In Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.MovePlayer("Sean", "NewGame", 10, 10));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Get Players Active In Game
            Console.WriteLine("Testing Getting Active Game Players");
            Console.WriteLine("===============================================");
            var GamePlayers = aTest.GetGamePlayers("NewGame");
            foreach (DataRow aRow in GamePlayers.Tables[0].Rows)
            {
                Console.WriteLine(aRow["userName"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests getting item list in Game
            Console.WriteLine("Testing Getting item list");
            Console.WriteLine("===============================================");
            var Items = (aTest.GetItems("Sean", "NewGame"));
            foreach (DataRow aRow in Items.Tables[0].Rows)
            {
                Console.WriteLine(aRow["name"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");


            //Tests Using Item in Game
            Console.WriteLine("Testing Using Item In Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.ConsumeItem("Sean", "NewGame", "Gold"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Leaving Game
            Console.WriteLine("Testing Leaving Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.LeaveGame("Sean", "NewGame"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Deleting Game
            Console.WriteLine("Testing Delete Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.DeleteGame("New"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Deactivating Game
            Console.WriteLine("Testing Deactivating Game");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.DeactivateGame("This Is Game3"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Sending message Game
            Console.WriteLine("Testing Sending message");
            Console.WriteLine("===============================================");
            Console.WriteLine(aTest.SendMessage("Sean", "boo"));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");


            //Tests Get Chat
            Console.WriteLine("Testing Getting Chat Messages");
            Console.WriteLine("===============================================");
            var Chat = aTest.GetChat();
            foreach (DataRow aRow in Chat.Tables[0].Rows)
            {
                Console.WriteLine(aRow["userName"] + ": " + aRow["text"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");


            //Tests Get All players
            Console.WriteLine("Testing Getting All Players");
            Console.WriteLine("===============================================");
            var Players = aTest.GetAllPlayers();
            foreach (DataRow aRow in Players.Tables[0].Rows)
            {
                Console.WriteLine(aRow["userName"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Get All Games
            Console.WriteLine("Testing Getting All Games");
            Console.WriteLine("===============================================");
            var Games = aTest.GetAllGames();
            foreach (DataRow aRow in Games.Tables[0].Rows)
            {
                Console.WriteLine(aRow["name"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Get Active Games
            Console.WriteLine("Testing Getting Active(Joinable) Games");
            Console.WriteLine("===============================================");
            var ActiveGames = aTest.GetActiveGames();
            foreach (DataRow aRow in ActiveGames.Tables[0].Rows)
            {
                Console.WriteLine(aRow["name"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

            //Tests Get Online Players
            Console.WriteLine("Testing Getting Online Players");
            Console.WriteLine("===============================================");
            var OnlinePlayers = aTest.GetOnlinePlayers();
            foreach (DataRow aRow in OnlinePlayers.Tables[0].Rows)
            {
                Console.WriteLine(aRow["userName"]);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("===============================================");

           


        }
    }
    
}

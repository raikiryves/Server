using HiddenServer.game;
using HiddenServer.game.characters;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ServerApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HiddenServer.Database
{
    public static class database
    {

        public static IMongoDatabase db;

        public static void initialDatabase(IMongoDatabase dbs)
        {
           db = dbs;
    
        }

        public static void createAccount(string username)
        {
            Account acc = new Account(username);
            Player player = new Player(1001,username);
            db.GetCollection<Account>("accounts").InsertOne( acc );
            db.GetCollection<Player>("players").InsertOne(player);
        }

         public static void deleteAccount(string username) 
        {
        
        }

        public static void updateAccount(string username)
        {


        }

        public static Character getCharacters()
        {
            Character chars = db.GetCollection<Character>("characters").Find(x => x.name == "laura").FirstOrDefault();
            return chars;
        }

            

    }
}

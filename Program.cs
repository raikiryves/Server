using System;
using HiddenServer.game;
using MongoDB.Driver;
using HiddenServer.Database;
using ServerApplication;

namespace HelloWorld
{
    class Program
    {
        private static Thread threadconsole;
        private static bool consolerunning;

        static void Main(string[] args)
        {
            string HostUrl = "mongodb://localhost:27017/";
            string Dbname = "hiddenland";
            MongoClient clientDB = new MongoClient(HostUrl);
            IMongoDatabase db = clientDB.GetDatabase(Dbname);
            database.initialDatabase(db);

            threadconsole = new Thread(new ThreadStart(ConsoleThread));
            threadconsole.Start();
            Network.instance.ServerStart();
        }

        private static void ConsoleThread()
        {
            string line;
            consolerunning = true;
            DateTime _nextLoop = DateTime.Now;

            while (consolerunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    gamelogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK) ;

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }


    }
}
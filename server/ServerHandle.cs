using HiddenServer.Database;
using HiddenServer.game.props;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApplication
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Network.Clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                //Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
                Console.WriteLine(_username);
            }
            // TODO: send player into game
        }

        public static void RegisterAccount(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            
            database.createAccount(_username);
            
        }
    }
}

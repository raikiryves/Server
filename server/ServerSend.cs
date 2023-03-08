using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerApplication
{
    class ServerSend
    {
        private static void SendTCPData(int tcpClient,Packet _packet)
        {
            
            _packet.WriteLength();
            Network.Clients[tcpClient].tcp.SendData(_packet);
            
        }

        public static void Welcome(int _tcpClient, string msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(msg);
                _packet.Write(_tcpClient);
                SendTCPData(_tcpClient,_packet);
            }
            
        }

        public static void SendTeam2Client(int _tcpClient, string teamJson)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendTeam))
            {
                _packet.Write(teamJson);
                SendTCPData(_tcpClient, _packet);
            }
            
        }

        public static void SpawnPlayer(int idClient, Player player)
        {
            using(Packet _packet = new Packet((int)ServerPackets.spawnPosition ))
            {
                _packet.Write(player.id);
                _packet.Write(player.username);
                _packet.Write(player.position);
                _packet.Write(player.rotation);
                SendTCPData(idClient, _packet);
            }
            Console.WriteLine(player.username);
        }


    }
}

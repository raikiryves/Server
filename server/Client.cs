using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using HiddenServer.game.characters;
using System.Text.Json;
using HiddenServer.Database;

namespace ServerApplication
{
    class Client
    {
        public static int dataBufferSize = 4096;
        public Player player;
        public int id;
        public TCP tcp;

        public Client(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                Console.WriteLine("ID : " + id);
                ServerSend.Welcome(id, "Welcome to the server!");
                Character chars = database.getCharacters();
                chars.name = "laura";
                string json = JsonSerializer.Serialize(chars);
                ServerSend.SendTeam2Client(id, json);
            }

            

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
                }
            }


            bool HandleData(byte[] data)
            {
                
                int packetLengh = 0;
                receivedData.SetBytes(data);


                if (receivedData.UnreadLength() >= 4)
                {
                    packetLengh = receivedData.ReadInt();
                    if (packetLengh <= 0)
                    {
                        return true;
                    }
                }

                while (packetLengh > 0 && packetLengh <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(packetLengh);
                    
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Network.packetHandlers[_packetId](id, _packet);
                        }
                    });

                    packetLengh = 0;
                    if (receivedData.UnreadLength() >= 4)
                    {
                        packetLengh = receivedData.ReadInt();
                        if (packetLengh <= 0)
                        {
                            return true;
                        }
                    }
                   
                }

                if (packetLengh <= 1)
                {
                   
                    return true;
                }

                
                return false;
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                Console.Write("finally");
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        // TODO: disconnect
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);
                    receivedData.Reset(HandleData(_data));
                    
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data: {_ex}");
                    // TODO: disconnect
                }
            }

            
        }

        public void Send2Game(string name)
        {
            player = new Player(id, name, new Vector3(0, 0, 0));

            foreach(Client _client in Network.Clients.Values)
            {
                if(_client.player != null)
                {
                    if(_client.id != id)
                    {
                        Console.WriteLine("Start 1");
                        ServerSend.SpawnPlayer(id, _client.player);
                    }
                }
            }


            foreach (Client _client in Network.Clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.id != id)
                    {
                        Console.WriteLine("Start 2");
                        ServerSend.SpawnPlayer(_client.id, player);
                    }
                }
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApplication
{
    class ServerHandleData
    {

        public static ServerHandleData instance = new ServerHandleData();
        private delegate void Packet_(int index, byte[] Data);
        private Dictionary<int, Packet_> Packets;


        public void IniMessages()
        {
            Packets = new Dictionary<int, Packet_>();
            //Packets.Add(1)
        }

        public void HandleData(int index,byte[] data)
        {
            int packetnum;
            Packet_ Packet;
          //  buffer.WriteBytes(data);
            //packetnum = buffer.readIntegrer();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Network.instance.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Network.instance.myId);
            _packet.Write("I'm here");
            //_packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void register(string username)
    {

        using (Packet _packet = new Packet((int)ClientPackets.register))
        {
            _packet.Write(Network.instance.myId);
            _packet.Write(username);
            SendTCPData(_packet);
        }
    }
    #endregion
}

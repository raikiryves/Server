using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    
    public static void Welcome(Packet _packet)
    {
        Debug.Log("Msg receive");
        string msg = _packet.ReadString();
        int _myUid = _packet.ReadInt();
       
        Debug.Log("Server Msg : " + msg);
        ClientSend.WelcomeReceived();
        
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string username = _packet.ReadString();
        Vector3 position = _packet.readVector3();
        Quaternion rotation = _packet.readQuaternion();

        Debug.Log("Player " + username + " spawned" );

        GameManager.instance.SpawnPlayers(_id, username, position, rotation);
    }

    public static void GetTeamfromServer(Packet _packet)
    {
        string json = _packet.ReadString();
        Debug.Log(json);
        Character character = JsonUtility.FromJson<Character>(json);

        GameManager.instance.myCharacters.Add(character);
        BattleManager.instance.StartBattle();
    }

    
}

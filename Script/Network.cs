using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class Network : MonoBehaviour
{
    public static Network instance;
    public static int dataBufferSize = 4096;

    [Header("Server Settings")]
    public string ServerIp = "127.0.0.1";
    public int ServerPort = 5500;
    public bool isConnected;
    public string msgTest = "ERT";

    public int myId = 0;

    private delegate void PacketHandler(Packet _packet);
    private Dictionary<int, PacketHandler> Packets;


    public TcpClient playerSocket;
    public NetworkStream myStream;
    public Packet receivedData;
    private byte[] receiveBuffer;
    public StreamReader myReader;
    public StreamWriter myWriter;

    private byte[] asyncBuff;
    public bool shouldHandleData;

    public TMP_InputField usernametxtUI;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        InitializeClientData();
        
        Connect2GameServer();
    }

 
    public void login()
    {
        ClientSend.register(usernametxtUI.text);
    }

    void Connect2GameServer()
    {
        
        if(playerSocket != null)
          {

            if (playerSocket.Connected || isConnected)
                return;

            playerSocket.Close();
            playerSocket = null;

        }

        playerSocket = new TcpClient();
        playerSocket.ReceiveBufferSize = 4096;
        playerSocket.SendBufferSize = 4096;
        playerSocket.NoDelay = false;
        Array.Resize(ref asyncBuff, 8192);
        receiveBuffer = new byte[dataBufferSize];
        playerSocket.BeginConnect(ServerIp, ServerPort, new AsyncCallback(ConnectCallBack) , playerSocket );
        isConnected = true;
    }

    void ConnectCallBack(IAsyncResult result)
    {
        Debug.Log(playerSocket.GetStream().ToString());
        playerSocket.EndConnect(result);

        if (playerSocket != null)
        {

            if (playerSocket.Connected == false)
            {
                isConnected = false;
                return;
            }
            else
            {

            }
                 

           // playerSocket.Close();
            //playerSocket = null;

        }


        try
        {
            myStream = playerSocket.GetStream();
        }
        catch(Exception e)
        {
            Debug.Log( e );
        }

        receivedData = new Packet();
       
        myStream.BeginRead(receiveBuffer, 0, dataBufferSize, OnReceive, null);
        
    }

    bool HandleData(byte[] data)
    {
        Debug.Log(data);
        int packetLengh = 0;
        receivedData.SetBytes(data);

        
        if (receivedData.UnreadLength() >= 4 )
        {
            packetLengh = receivedData.ReadInt();
            if(packetLengh <= 0)
            {
                return true;
            }
        }

        while(packetLengh > 0 && packetLengh <= receivedData.UnreadLength() )
        {
            byte[] _packetBytes = receivedData.ReadBytes(packetLengh);
            Debug.Log("Packets");
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_packetBytes))
                {
                    int _packetId = _packet.ReadInt();
                    Packets[_packetId](_packet);
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
            Debug.Log("While");
        }

        if (packetLengh <= 1)
        {
            Debug.Log("True");
            return true;
        }

        Debug.Log("False");
        return false; 
    }

    public void SendData(Packet _packet)
    {
        try
        {
            if (playerSocket != null)
            {
                myStream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error sending data to server via TCP: {_ex}");
        }
    }


    void OnReceive(IAsyncResult result)
    {
        
        if (playerSocket != null)
        {
            Debug.Log("there's a player socket");
            
            if (playerSocket == null)
                return;

            int byteArray = myStream.EndRead(result);
            byte[] myBytes = null;
            byte[] _data = new byte[byteArray];
            Array.Copy(receiveBuffer, _data, byteArray);
            //Array.Resize(ref myBytes, byteArray);

            //Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteArray);

            Debug.Log(byteArray);
            if (byteArray <= 0)
            {
                Debug.Log("you got disconnected from the server");
                playerSocket.Close();
                return;
            }

            if (playerSocket == null)
                return;

            receivedData.Reset(HandleData(_data));
            //myStream.BeginRead(asyncBuff);
            myStream.BeginRead(receiveBuffer, 0, dataBufferSize, OnReceive, null);
        }
    }

    private void InitializeClientData()
    {
        

        Packets = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandle.Welcome },
            { (int)ServerPackets.spawnPosition, ClientHandle.SpawnPlayer },
            { (int)ServerPackets.sendTeam, ClientHandle.GetTeamfromServer }
        };

        Debug.Log(Packets.Values);

        Debug.Log("Initialized packets.");
        
    }


}

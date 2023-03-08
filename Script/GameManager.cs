using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static Dictionary<int, PlayerManager> _players = new Dictionary<int, PlayerManager>();
    public PlayerManager player = new PlayerManager();

    public List<Character> myCharacters = new List<Character>();
    public List<GameEntity> gameEntities = new List<GameEntity>();

    public GameObject localPlayer;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void Start()
    {
       
    }

    public void StartBattle()
    {
        Enemy ene = new Enemy();
        ene.spd = 50;
        

        gameEntities.AddRange(myCharacters);
        gameEntities.Add(ene);
        gameEntities.OrderBy(o => o.spd).ToList();
    }

    public void Battling()
    {
        int nbTurn;

        foreach(GameEntity entity in gameEntities)
        {

        }
    }

    public void SpawnPlayers(int _id,string username,Vector3 position,Quaternion rotation)
    {
        GameObject _player;
        if (_id == Network.instance.myId)
        {
            _player = Instantiate(localPlayer, position, rotation);
        }
        else
        {
            _player = Instantiate(localPlayer, position, rotation);
        }

    }
}

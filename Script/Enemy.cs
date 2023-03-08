 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameEntity
{
    // Start is called before the first frame updates
    public EnemyState state;
    public PlayerManager target;
    public CharacterController controller;
    public float gravity = -9.81f;
    public float health;
    public float maxHp;
    public float detectionRange = 30f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum EnemyState
    {
        idle,
        patrol,
        chase,
        attack
    }
}

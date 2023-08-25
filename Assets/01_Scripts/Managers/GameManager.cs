using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private GameObject _player;
    public GameObject Player
    {
        get { return _player;}
        set { _player = value; }
    }
    // 게임 지속 시간
    

    // 총 게임 시간
    
    public int level;
    public float maxLevel = 5;

    public float gameTime;

    public float maxGameTime;
    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        maxLevel = 5;
        maxGameTime = maxLevel * 10.0f;

    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        level = Mathf.FloorToInt(gameTime / 10f);

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}

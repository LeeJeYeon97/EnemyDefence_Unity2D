using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

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
    private PlayerController _player;
    public PlayerController Player
    {
        get { return _player;}
        set { _player = value; }
    }
    
    
    public int level;
    public float maxLevel;

    // 게임 시간
    public float gameTime;

    // 총 게임 시간
    public float maxGameTime;

    public bool IsPause = false;

    private void Awake()
    {
        _instance = this;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxLevel = Enum.GetValues(typeof(Define.MonsterName)).Length;
        maxGameTime = maxLevel * 10.0f;

        Managers.UI.ShowPopupUI<UI_SelectPopup>("SelectPopup");

        IsPause = true;
    }
    private void Update()
    {
        if(IsPause)
        {
            Time.timeScale = 0;
        }
        if(!IsPause)
        {
            
            Time.timeScale = 1;
            gameTime += Time.deltaTime;

            level = Mathf.FloorToInt(gameTime / 1000f);
            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }            
        }
        
    }
}

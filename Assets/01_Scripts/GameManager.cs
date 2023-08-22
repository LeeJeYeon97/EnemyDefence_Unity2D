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
    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}

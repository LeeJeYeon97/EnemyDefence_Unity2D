using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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

    // ���� �ð�
    public float gameTime;

    // �� ���� �ð�
    public float maxGameTime;

    public bool IsPause = false;

    public Image fadeImage;
    public bool fadeIn = true;
    public bool GameStart = false;
    private void Awake()
    {
        _instance = this;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //maxLevel = Enum.GetValues(typeof(Define.MonsterName)).Length;
        level = 1;
        maxLevel = 10;
        maxGameTime = maxLevel * 60.0f;
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        fadeIn = true;
        
    }
    private void Update()
    {
        if (GameStart == false && fadeIn)
        {
            if (fadeImage.color.a <= 0)
            {
                fadeIn = false;
                Managers.UI.ShowPopupUI<UI_SelectPopup>("SelectPopup");
                IsPause = true;
                GameStart = true;

            }
            FadeIn();
        }
        else if(GameStart == false && !fadeIn)
        {
            FadeOut();
            if(fadeImage.color.a >= 1)
            {
                Managers.Pool.ClearPool();
                SceneManager.LoadScene("LobbyScene");
            }
        }
        if(IsPause)
        {
            Time.timeScale = 0;
        }
        if(!IsPause)
        {
            
            Time.timeScale = 1;
            gameTime += Time.deltaTime;

            level = Mathf.FloorToInt(gameTime / 120.0f);
            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }            
        }
    }
    void FadeIn()
    {
        UnityEngine.Color color = fadeImage.color;

        if (color.a > 0)
            color.a -= Time.deltaTime;

        fadeImage.color = color;

    }
    void FadeOut()
    {
        // image�� color ������Ƽ�� a(���İ�)������ ���� ������ �Ұ����� ��ü���� ����

        UnityEngine.Color color = fadeImage.color;
        // ���İ��� 0���� ũ�� ���İ� ����
        if (color.a < 1)
        {
            color.a += Time.deltaTime;
        }
        fadeImage.color = color;
    }
}

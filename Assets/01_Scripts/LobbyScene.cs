using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{

    private Button startButton;
    private GameObject player;
    private Image fadeImage;
    Vector3 dir;
    Vector3 randPos;
    float randomTime;
    bool fadeIn;
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(() => OnClickStartButton());

        player = GameObject.Find("LobbyPlayer");

        fadeImage = GameObject.Find("FadeImg").GetComponent<Image>();

        randPos = GetRandomPos();
        fadeIn = true;
    }

    void Update()
    {
        if(fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }

        if(fadeImage.color.a >= 1 && fadeIn ==false)
        {
            SceneManager.LoadScene("GameScene");
        }
        PlayerMove();
    }
    void PlayerMove()
    {
        dir = randPos - player.transform.position;
        if(dir.x > 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(dir.magnitude > 0.01f)
        {
            player.transform.Translate(dir.normalized * Time.deltaTime * 2.0f);
            player.GetComponent<Animator>().SetFloat("Speed", 2.0f);
        }
        else
        {
            player.GetComponent<Animator>().SetFloat("Speed", 0.0f);
            randomTime -= Time.deltaTime;
            if(randomTime<=0)
                randPos = GetRandomPos();
        }
    }
    Vector3 GetRandomPos()
    {
        randomTime = Random.Range(0.5f, 2.0f);
        float randomX = Random.Range(-5.0f, 5.0f);
        float randomY = Random.Range(-5.0f, 5.0f);

        Vector3 randPos = new Vector3(player.transform.position.x + randomX
            , player.transform.position.y + randomY);
        return randPos;
    }
    void OnClickStartButton()
    {
        fadeIn = false;
    }
    void FadeIn()
    {
        UnityEngine.Color color = fadeImage.color;
        
        if(color.a > 0)
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
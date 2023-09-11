using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResultPopup : UI_Popup
{
    enum Images
    {
        Image
    }
    enum Buttons
    {
        QuitButton,
    }
    public Button button;
    public Image image;
    public Sprite[] sprites;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        button = Get<Button>((int)Buttons.QuitButton);
        image = Get<Image>((int)Images.Image);

        

        if (GameManager.Instance.gameTime >=  GameManager.Instance.maxGameTime)
        {
            image.sprite = sprites[0];
        }
        else
        {
            image.sprite = sprites[1];
        }
        button.onClick.AddListener(() => OnClickQuiButton());
    }
    public void OnClickQuiButton()
    {
        GameManager.Instance.GameStart = false;
        GameManager.Instance.fadeIn = false;
        GameManager.Instance.IsPause = false;
        SoundManager.instance.PlayBgm(false, Define.Bgm.GameBgm);
    }

}

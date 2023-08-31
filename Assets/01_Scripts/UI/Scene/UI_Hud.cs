using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{

    Slider _expBar;
    Text _killText;
    Text _timeText;
    Text _levelText;

    PlayerController _player;

    enum GameObjects
    {
        ExpBar,
    }
    enum Texts
    {
        KillText,
        TimeText,
        LevelText,
    }

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        //base.Init();

        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(GameObjects));

        _expBar = Get<Slider>((int)GameObjects.ExpBar);

        _killText = Get<Text>((int)Texts.KillText);
        _timeText = Get<Text>((int)Texts.TimeText);
        _levelText = Get<Text>((int)Texts.LevelText);

        _player = GameManager.Instance.Player;

        _player.GetExpAction -= SetHud;
        _player.GetExpAction += SetHud;

        _player.GetExpAction -= SetExpBar;
        _player.GetExpAction += SetExpBar;

        SetExpBar();
        SetHud();
    }
    public void FixedUpdate()
    {
        int min = Mathf.FloorToInt(GameManager.Instance.gameTime / 60);
        int sec = Mathf.FloorToInt(GameManager.Instance.gameTime % 60);
        // D : 자리수를 지정
        _timeText.text = $"{min:D2} : {sec:D2}";
    }
    private void SetHud()
    {
        _killText.text = $"{_player.kill}";
        _levelText.text = $"Lv:{_player.level}";
    }

    private void SetExpBar()
    {
        _expBar.value = _player.curExp / _player.maxExP;
    }

}

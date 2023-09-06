using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{

    Slider _expBar;
    Slider _hpBar;

    Text _killText;
    Text _timeText;
    Text _levelText;

    PlayerController _player;

    

    enum GameObjects
    {
        ExpBar,
        HpBar,

    }
    enum Texts
    {
        KillText,
        TimeText,
        LevelText,
    }

    private void OnEnable()
    {
        Init();
    }
    public override void Init()
    {
        //base.Init();

        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(GameObjects));

        _expBar = Get<Slider>((int)GameObjects.ExpBar);
        _hpBar = Get<Slider>((int)GameObjects.HpBar);

        _killText = Get<Text>((int)Texts.KillText);
        _timeText = Get<Text>((int)Texts.TimeText);
        _levelText = Get<Text>((int)Texts.LevelText);

        _player = GameManager.Instance.Player;

        _player._stat.GetExpAction -= SetHud;
        _player._stat.GetExpAction += SetHud;

        _player._stat.GetExpAction -= SetExpBar;
        _player._stat.GetExpAction += SetExpBar;

    }
    public void FixedUpdate()
    {
        int min = Mathf.FloorToInt(GameManager.Instance.gameTime / 60);
        int sec = Mathf.FloorToInt(GameManager.Instance.gameTime % 60);
        // D : 자리수를 지정
        _timeText.text = $"{min:D2} : {sec:D2}";
        Vector3 pos = Camera.main.WorldToScreenPoint(GameManager.Instance.Player.transform.position);

        _hpBar.GetComponent<RectTransform>().position = pos + new Vector3(0, -230, 0);
    }
    private void SetHud()
    {
        _killText.text = $"{_player.kill}";
        _levelText.text = $"Lv:{_player._stat.level}";
        _hpBar.value = _player.CurHp / _player.MaxHp;
    }
    private void SetExpBar()
    {
        _expBar.value = _player._stat.curExp / _player._stat.maxExP;
    }
    private void SetHpBar()
    {
        _hpBar.value = _player.CurHp / _player.MaxHp;
    }

}

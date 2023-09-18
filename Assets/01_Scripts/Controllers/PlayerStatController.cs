using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Mathematics;

public class PlayerStatController : MonoBehaviour
{
    public StatData _data;

    public Action GetExpAction;

    public int level;
    public int healthLevel;
    public int speedLevel;

    public float maxExP;
    public float curExp;

    public float moveSpeed;

    public float maxHp;
    public float curHp;

    public int statLength;
    public string[] statNames;

    public Slider hpBar;

    void Start()
    {
        _data = Managers.Data.GetData<StatData>("Base");
        
        level = _data.Level;

        healthLevel = _data.hpLevel;
        maxHp = _data.MaxHp;
        curHp = _data.MaxHp;
        GameManager.Instance.Player.MaxHp = maxHp;
        GameManager.Instance.Player.CurHp = curHp;

        maxExP = _data.maxExp;

        speedLevel = _data.speedLevel;
        moveSpeed = _data.speedDatas[_data.speedLevel - 1];

    }
    private void Update()
    {
        hpBar.value = curHp / maxHp;
    }
    private void LevelUp()
    {
        level++;
        GameManager.Instance.IsPause = true;

        SoundManager.instance.PlaySfx(Define.Sfx.LevelUp1);
        Managers.UI.ShowPopupUI<UI_LevelUpPopup>("LevelUpPopup");
    }
    public void GetExp(float exp)
    {
        curExp += exp;
        if (curExp >= maxExP)
        {
            LevelUp();
            curExp = 0;
            maxExP *= (float)1.5;
        }
    }
    public void LevelUpStat()
    {
        moveSpeed = _data.speedDatas[speedLevel - 1];
        maxHp = _data.healthDatas[healthLevel - 1];

        GameManager.Instance.Player.MaxHp = maxHp;
    }
}

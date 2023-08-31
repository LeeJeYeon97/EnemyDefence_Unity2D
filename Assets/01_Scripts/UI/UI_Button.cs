using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    public enum ButtonType
    {
        Select,
        LevelUp,
    }
    public ButtonType type;
    
    enum Texts
    {
        WeaponText
    }
    enum Images
    {
        WeaponIcon,   
    }

    private int level;
    private Image _image;
    private Text _text;

    public override void Init()
    {
        //Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        
        _image = Get<Image>((int)Images.WeaponIcon);
        _text = Get<Text>((int)Texts.WeaponText);
    }
    public void SetButton(WeaponData data,GameObject go = null)
    {
        Init();
        Button button = gameObject.GetComponent<Button>();
        switch (type)
        {
            case ButtonType.Select:
                _image.sprite = data.Icon;
                _text.text = data.WeaponDesc;
                button.onClick.AddListener(() => SelectWeapon(data));
                break;
            case ButtonType.LevelUp:
                _image.sprite = data.Icon;
                int level = go.GetComponent<WeaponController>().Level;
                _text.text = $"Lv : {level}";
                button.onClick.AddListener(() => LevelUpWeapon(go));
                break;
        }
    }
    public void SelectWeapon(WeaponData data)
    {
        GameObject go;
        GameObject weapon;
        switch (data.Type)
        {
            case Define.WeaponType.Saw:
                go = Resources.Load<GameObject>("Weapon/SawWeapon");
                weapon = Instantiate(go);
                GameManager.Instance.Player.GetComponent<PlayerController>()._weaponList.Add(weapon);
                weapon.transform.SetParent(GameManager.Instance.Player.transform);
                break;
            case Define.WeaponType.Sword:
                break;
            case Define.WeaponType.Gun:
                go = Resources.Load<GameObject>("Weapon/GunWeapon");
                weapon = Instantiate(go);
                GameManager.Instance.Player.GetComponent<PlayerController>()._weaponList.Add(weapon);
                weapon.transform.SetParent(GameManager.Instance.Player.transform);
                break;
        }
        GameManager.Instance.IsPause = false;
        Managers.UI.ClosePopupUI();
    }
    public void LevelUpWeapon(GameObject weapon)
    {
        GameManager.Instance.IsPause = false;
        // TODO
        weapon.GetComponent<WeaponController>().LevelUp();

        Managers.UI.ClosePopupUI();
    }
}

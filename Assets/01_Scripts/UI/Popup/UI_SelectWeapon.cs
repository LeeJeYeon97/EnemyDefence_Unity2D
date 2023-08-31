using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_SelectWeapon : UI_Base
{
    enum GameObjects
    {
        Panels
    }

    private GameObject buttonPrefab;
    private GameObject panel;

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        panel = Get<GameObject>((int)GameObjects.Panels);

        buttonPrefab = Resources.Load<GameObject>("UI/SelectButton");

        int length = Enum.GetValues(typeof(Define.WeaponType)).Length;

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < length; i++)
        {
            GameObject go = Instantiate(buttonPrefab);
            go.transform.SetParent(panel.transform);
            go.transform.localScale = Vector3.one;

            string key = Enum.GetName(typeof(Define.WeaponType), i);
            WeaponData data = Managers.Data.GetWeaponData(key);

            go.GetComponent<UI_Button>().SetButton(data);
        }
    }

}

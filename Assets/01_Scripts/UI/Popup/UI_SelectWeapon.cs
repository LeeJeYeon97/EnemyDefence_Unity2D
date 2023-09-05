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

        List<GameObject> weaponList = GameManager.Instance.Player._weaponList;

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < length; i++)
        {
            bool check = false;
            foreach(var weapon in weaponList)
            {
                WeaponData data = weapon.GetComponent<WeaponController>()._data;
                if(Enum.GetName(typeof(Define.WeaponType),data.Type) == 
                    Enum.GetName(typeof(Define.WeaponType),i))
                {
                    check = true;
                }
            }
            if (check) continue;

            
            GameObject go = Instantiate(buttonPrefab);
            go.transform.SetParent(panel.transform);
            go.transform.localScale = Vector3.one;

            string key = Enum.GetName(typeof(Define.WeaponType), i);

            go.GetComponent<UI_Button>().SetWeaponButton(key);
        }
    }

}

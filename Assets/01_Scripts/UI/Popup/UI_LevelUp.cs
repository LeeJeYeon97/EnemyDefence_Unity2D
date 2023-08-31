using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelUp : UI_Base
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

        buttonPrefab = Resources.Load<GameObject>("UI/LevelUpButton");

        int length = GameManager.Instance.Player._weaponList.Count;

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < length; i++)
        {
            // 버튼 생성
            int randIdx = UnityEngine.Random.Range(0, length);
            GameObject go = Instantiate(buttonPrefab);
            go.transform.SetParent(panel.transform);
            go.transform.localScale = Vector3.one;

            Define.WeaponType type = GameManager.Instance.Player._weaponList[randIdx].GetComponent<WeaponController>()._data.Type;

            string key = Enum.GetName(typeof(Define.WeaponType), type);

            WeaponData data = Managers.Data.GetWeaponData(key);
            go.GetComponent<UI_Button>().SetButton(data, GameManager.Instance.Player._weaponList[randIdx]);
        }
    }
}

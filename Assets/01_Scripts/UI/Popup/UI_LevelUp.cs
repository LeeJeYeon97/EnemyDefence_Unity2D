using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UI_LevelUp : UI_Base
{
    enum GameObjects
    {
        Panels
    }

    private GameObject buttonPrefab;
    private GameObject panel;

    public List<GameObject> buttons = new List<GameObject>();

    private void Start()
    {
        Init();
        ShowButton();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        panel = Get<GameObject>((int)GameObjects.Panels);

        buttonPrefab = Resources.Load<GameObject>("UI/LevelUpButton");

        int weaponLength = GameManager.Instance.Player._weaponList.Count;
        int statLength = Enum.GetValues(typeof(Define.LevelUpStats)).Length;
        string[] statKeys = Enum.GetNames(typeof(Define.LevelUpStats));

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        // 스탯 버튼 생성
        for(int i = 0; i < statLength; i++)
        {
            GameObject go = Instantiate(buttonPrefab);

            go.GetComponent<UI_Button>().SetStatButton(statKeys[i]);
        
            // 버튼 리스트에 추가
            buttons.Add(go);
        }
        // 무기버튼 생성
        for (int i = 0; i < weaponLength; i++)
        {
            if (GameManager.Instance.Player._weaponList[i].GetComponent<WeaponController>().Level >=
                GameManager.Instance.Player._weaponList[i].GetComponent<WeaponController>().MaxLevel)
                continue;
            
            // 버튼 생성
            GameObject go = Instantiate(buttonPrefab);

            // Player의 Weapon정보 가져오기
            Define.WeaponType type = GameManager.Instance.Player._weaponList[i].GetComponent<WeaponController>()._data.Type;
            string key = Enum.GetName(typeof(Define.WeaponType), type);

            // Weapon정보를 참조하여 Button 설정
            go.GetComponent<UI_Button>().SetWeaponButton(key, GameManager.Instance.Player._weaponList[i]);

            // 버튼 리스트에 추가
            buttons.Add(go);
        }
    }
    // 생성한 버튼중에서 랜덤하게 3개 표시
    private void ShowButton()
    {
        int count = 0;
        if (buttons.Count < 3)  count = buttons.Count;
        else count = 3;
        
        List<GameObject> temp = new List<GameObject>();

        while(count > 0)
        {
            int randIdx = UnityEngine.Random.Range(0, buttons.Count);
            if (temp.Contains(buttons[randIdx]))
            {
                continue;
            }

            buttons[randIdx].transform.SetParent(panel.transform);
            buttons[randIdx].transform.localScale = Vector3.one;
            temp.Add(buttons[randIdx]);
            count--;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager 
{
    public Dictionary<string, ScriptableObject> _datas = new Dictionary<string, ScriptableObject>();

    public void Init()
    {
        SetWeaponData();
        SetMonsterData();
        SetStatData();
    }

    private void SetWeaponData()
    {
        int length = Enum.GetValues(typeof(Define.WeaponType)).Length;

        for (int i = 0; i < length; i++)
        {
            string name = Enum.GetName(typeof(Define.WeaponType), i);
            ScriptableObject data = Resources.Load<ScriptableObject>($"Data/WeaponData/{name}Data");
            _datas.Add(name, data);
        }
    }
    private void SetStatData() 
    {
        int length = Enum.GetValues(typeof(Define.Stats)).Length;

        for (int i = 0; i < length; i++)
        {
            string name = Enum.GetName(typeof(Define.Stats), i);
            ScriptableObject data = Resources.Load<ScriptableObject>($"Data/StatData/{name}Data");
            _datas.Add(name, data);
        }
    }
    private void SetMonsterData()
    {
        int length = Enum.GetValues(typeof(Define.MonsterName)).Length;

        for (int i = 0; i < length; i++)
        {
            string name = Enum.GetName(typeof(Define.MonsterName), i);
            ScriptableObject data = Resources.Load<ScriptableObject>($"Data/MonsterData/{name}Data");
            _datas.Add(name, data);
        }
    }
    public T GetData<T>(string key) where T : ScriptableObject
    {
        if (!_datas.ContainsKey(key))
            return null;
    
        return _datas[key] as T;
    }
}

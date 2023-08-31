using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager 
{
    public Dictionary<string, WeaponData> _weaponDatas = new Dictionary<string, WeaponData>();

    public void Init()
    {
        int length = Enum.GetValues(typeof(Define.WeaponType)).Length;

        for (int i = 0; i < length; i++)
        {
            string name = Enum.GetName(typeof(Define.WeaponType), i);
            WeaponData obj = Resources.Load<WeaponData>($"Data/WeaponData/{name}Data");
            _weaponDatas.Add(name, obj);
        }
    }

    public WeaponData GetWeaponData(string key)
    {
        if (!_weaponDatas.ContainsKey(key))
            return null;

        return _weaponDatas[key];
    }
}

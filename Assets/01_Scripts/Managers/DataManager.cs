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
        SetData(typeof(Define.WeaponType));
        SetData(typeof(Define.MonsterName));
        SetData(typeof(Define.Stats));
    }

    private void SetData(Type t)
    {
        int length = Enum.GetValues(t).Length;

        for (int i = 0; i < length; i++)
        {
            string name = Enum.GetName(t, i);
            ScriptableObject data = Resources.Load<ScriptableObject>($"Data/{name}Data");
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

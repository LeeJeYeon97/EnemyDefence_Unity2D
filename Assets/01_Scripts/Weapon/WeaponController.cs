using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponController : MonoBehaviour
{
    protected int _maxLevel;
    public int MaxLevel
    {
        get { return _maxLevel; }
        set { _maxLevel = value; }
    }
    protected int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }
    
    public WeaponData _data;
    public float _attackDelay;

    protected float _damage;


    protected virtual void Init()
    {
        _level = 1;
        _maxLevel = 5;
    }
    public virtual void LevelUp()
    {
        Level++;
        _damage = _data.Damages[Level - 1];

        _attackDelay = _data.attackDelay[Level - 1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public class MonsterData
    {
        public MonsterName name;
        public float moveSpeed;
        public float maxHp;
        public float attackPower;
    }
    public enum PoolObject
    {
        Monster,
        Item,
        Bullet,
    }
    public enum MonsterName
    {
        Bat = 0,
        Chicken = 1,
        Bunny = 2,
        Rino = 3,
    }
    public enum ItemList
    {
        Exp0,
        Exp1,
        Exp2,
        Exp3,
        Box,
        Health,
        Mag,
        Chest,
    }
    public enum MonsterType
    {
        Melee,
        Ranged,
        Boss,
    }
    public enum WeaponType
    {
        Saw,
        Sword,
        Gun,
    }
    public enum Stats
    {
        Base,

    }
    public enum LevelUpStats
    {
        Speed,
        Health,
    }
}

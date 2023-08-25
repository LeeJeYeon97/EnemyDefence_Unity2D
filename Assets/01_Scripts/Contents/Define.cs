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
    public enum MonsterName
    {
        Bat = 0,
        Chicken = 1,
        Bunny = 2,
        Rino = 3,
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
    }
}

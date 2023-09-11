using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum PoolObject
    {
        Monster,
        Item,
        Bullet,
    }
    public enum MonsterName
    {
        Bat,
        Chicken,
        Bunny,
        Rino,
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
    public enum Sfx
    {
        Dead,
        Hit0,
        Hit1,
        LevelUp,
        LevelUp1,
        Lose,
        Select,
        Win,
        GetItem,
        Shot,
    }
    public enum Bgm
    {
        LobbyBgm,
        GameBgm,
    }
}

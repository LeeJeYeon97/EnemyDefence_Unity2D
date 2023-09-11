using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Object/MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("Main Info")]
    public Define.MonsterName _name;
    public float MaxHp;
    public float MoveSpeed;
    public AnimatorOverrideController anim;
    public float Damage;

}

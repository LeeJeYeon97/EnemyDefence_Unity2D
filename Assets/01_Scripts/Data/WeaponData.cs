using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName ="Scriptable Object/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Main Info")]
    public Define.WeaponType Type;
    public string WeaponName;
    public string WeaponDesc;
    public Sprite Icon;


    [Header("Level Data")]
    public float Damage;
    public float[] Damages;
    public float[] attackDelay;



}

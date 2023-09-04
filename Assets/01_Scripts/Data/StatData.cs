using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/StatData")]
public class StatData : ScriptableObject
{
    [Header("Main Info")]
    public Define.Stats Type;
    public Sprite Icon;

    [Header("Level Info")]
    public int Level;
    public float maxExp;

    

    [Header("Health Data")]
    public float MaxHp;
    public int hpLevel;
    public float healthRatio;
    public Sprite healthIcon;

    [Header("Speed Data")]
    public int speedLevel;
    public int maxSpeedLevel;
    public float[] speedDatas;
    public Sprite SpeedIcon;
}

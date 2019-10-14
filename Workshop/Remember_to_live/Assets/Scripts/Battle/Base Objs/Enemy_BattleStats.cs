using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy_BattleStats
{
    // Enemy stats
    public string enemyName;

    public float enemyHP, curHP;
    public float playerMP, curMP;

    public int enemyStr;
    public int enemyInt;
    public int enemySpd;

    // Enemy Type
    public enum Type
    {
        Conversational,
        Nonsentient,
        Monster
    }

    public Type enemyType;

    public List<Attack_Base> attacks = new List<Attack_Base>();

}

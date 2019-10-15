using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_OverworldData : MonoBehaviour
{
    // Max duplicates that can spawn per battle
    public int maxEnemies;

    // Name of battle scene for this enemy
    public string battleScene;

    // Stores list of spawned enemies during battle here
    public List<GameObject> enemyVariations = new List<GameObject>();
}

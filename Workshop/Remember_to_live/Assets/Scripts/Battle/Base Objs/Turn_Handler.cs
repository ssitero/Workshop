using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turn_Handler
{
    // Attacker
    public string attacker;
    public string type;

    public GameObject attackerObj;
    public GameObject targetObj;

    public Attack_Base chosenAtk;
}

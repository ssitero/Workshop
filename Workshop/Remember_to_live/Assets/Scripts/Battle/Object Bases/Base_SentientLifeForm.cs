using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_SentientLifeForm
{
    // A blueprint for all actors that require battle stats

    // Name
    public string name = " ";

    // HP & MP
    public float baseHP;
    public float curHP;

    public float baseMP;
    public float curMP;

    // Stats --> can add more later as needs arise
    public float atk;
    public float def;

    // List of skills known by this actor
    public List<Base_Attack> attacksKnown = new List<Base_Attack>();

}

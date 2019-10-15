using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Base_Enemy : Base_SentientLifeForm
{
    public enum Type
    {
        CONVERSATION,
        FIGHT
    }

    public Type enemyType;
    
    // Add drop rate
}

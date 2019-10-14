using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Battle_StateMachine : MonoBehaviour
{
    public enum BattleStates
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Win,
        Loss
    }

    private BattleStates battleState;

    void Start()
    {
        battleState = BattleStates.Start;
    }

    void Update()
    {
        switch(battleState)
        {
            case (BattleStates.Start):
                break;
            case (BattleStates.PlayerTurn):
                break;
            case (BattleStates.EnemyTurn):
                break;
            case (BattleStates.Win):
                break;
            case (BattleStates.Loss):
                break;
        }
    }
}

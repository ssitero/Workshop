using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StateMachine : MonoBehaviour
{
  public Enemy_BattleStats enemy;
  private Battle_StateMachine BSM;

  public enum TurnState
  {
    Processing,
    ChooseAction,
    Wait,
    Action,
    Dead
  }

  public TurnState enemyState;

  public GameObject selector;

  private bool alive = true;

  void Start()
  {
    selector.SetActive(false);
    enemyState = TurnState.Processing;
    BSM = GameObject.Find("BattleManager").GetComponent<Battle_StateMachine>();
  }

  void Update()
  {
    Debug.Log(enemyState);
    switch (enemyState)
    {
      case (TurnState.Processing):
        // ????
        break;

      case (TurnState.ChooseAction):
        if (!BSM.playerIsAlive)
        {
          Debug.Log("Player dead");
          break;
        }
        else
        {
          chooseAction();
          enemyState = TurnState.Wait;
        }
        break;

      case (TurnState.Wait):
        break;

      case (TurnState.Action):
        StartCoroutine(enemyAction());
        break;

      case () //WORKING ON THIS

    }
  }

  void chooseAction()
  {

  }


}

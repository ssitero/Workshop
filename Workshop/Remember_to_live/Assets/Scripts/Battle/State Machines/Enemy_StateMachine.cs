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
    private float curCooldown = 0.0f;
    private float maxCooldown = 10.0f;

    public GameObject targetPlayer;
    private bool actionStarted = false;

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
                updateBar();
                break;

            case (TurnState.ChooseAction):
                if (BSM.playerList.Count == 0)
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
                StartCoroutine(actionTimer());
                break;

            case (TurnState.Dead):
                if (!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = "DeadEnemy";
                    BSM.enemyList.Remove(this.gameObject);
                    selector.SetActive(false);

                    if (BSM.enemyList.Count > 0)
                    {
                        for (int i = 0; i < BSM.turnList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (BSM.turnList[i].attackerObj == this.gameObject)
                                {
                                    BSM.turnList.Remove(BSM.turnList[i]);
                                }
                                if (BSM.turnList[i].targetObj == this.gameObject)
                                {
                                    BSM.turnList[i].targetObj = BSM.enemyList[Random.Range(0, BSM.enemyList.Count)];
                                }
                            }
                        }
                    }

                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(155, 155, 155, 255);
                    alive = false;
                    BSM.TargetButtons();

                    BSM.battleState = Battle_StateMachine.BattleStates.CheckAlive;
                }
                break;

        }
    }

    void updateBar()
    {
        curCooldown = curCooldown + Time.deltaTime;

        if (curCooldown >= maxCooldown)
        {
            enemyState = TurnState.ChooseAction;
        }
    }

    void chooseAction()
    {
        Turn_Handler thisAttack = new Turn_Handler();
        thisAttack.attacker = enemy.enemyName;
        thisAttack.type = "Enemy";
        thisAttack.attackerObj = this.gameObject;
        thisAttack.targetObj = BSM.playerList[Random.Range(0, BSM.playerList.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        thisAttack.chosenAtk = enemy.attacks[num];
        Debug.Log(this.gameObject.name + " uses " + thisAttack.chosenAtk.attackName + ", dealing " + thisAttack.chosenAtk.attackDmg + " damage.");

        BSM.CollectActions(thisAttack);
    }

    private IEnumerator actionTimer()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        doDamage();

        BSM.turnList.RemoveAt(0);

        BSM.battleState = Battle_StateMachine.BattleStates.Wait;

        actionStarted = false;

        curCooldown = 0f;
        enemyState = TurnState.Processing;
    }

    void doDamage()
    {
        float damageDone = enemy.enemyStr + BSM.turnList[0].chosenAtk.attackDmg;
        targetPlayer.GetComponent<Player_StateMachine>().takeDamage(damageDone);
    }

    public void takeDamage(float dmg)
    {
        enemy.curHP -= dmg;

        if (enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            enemyState = TurnState.Dead;
        }
    }
}

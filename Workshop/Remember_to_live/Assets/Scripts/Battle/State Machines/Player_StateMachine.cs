using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_StateMachine : MonoBehaviour
{
    public Player_BattleStats player;

    private Battle_StateMachine BSM;

    public enum TurnState
    {
        Processing,
        AddToList,
        Waiting,
        Selecting,
        Action,
        Dead
    }

    public TurnState playerState;

    private float curCooldown = 0.0f;
    private float maxCooldown = 5.0f;
    private Image progressBar;

    public GameObject selector;

    public GameObject targetEnemy;
    private bool actionStarted = false;

    private bool alive = true;

    private Player_Panel stuff;
    public GameObject playerPanel;
    private Transform playerPanelSpacer;

    void Start()
    {
        curCooldown = Random.Range(0, 2.5f);
        playerState = TurnState.Processing;
        BSM = GameObject.Find("BattleManager").GetComponent<Battle_StateMachine>();
        selector.SetActive(false);

        playerPanelSpacer = GameObject.Find("Battle UI").transform.Find("Player Panel").transform.Find("PlayerPanelSpacer");
        createPlayerPanel();
    }

    void Update()
    {
        Debug.Log(playerState);
        switch (playerState)
        {
            case (TurnState.Processing):
                updateProgressBar();
                break;

            case (TurnState.AddToList):
                BSM.playerManagerList.Add(this.gameObject);
                playerState = TurnState.Waiting;
                break;

            case (TurnState.Waiting):
                //idle
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
                    this.gameObject.tag = "DeadPlayer";
                    BSM.playerList.Remove(this.gameObject);
                    BSM.playerManagerList.Remove(this.gameObject);

                    selector.SetActive(false);

                    BSM.attackPanel.SetActive(false);
                    BSM.targetPanel.SetActive(false);

                    if (BSM.playerList.Count > 0)
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
                                    BSM.turnList[i].targetObj = BSM.playerList[Random.Range(0, BSM.playerList.Count)];
                                }
                            }
                        }
                    }

                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(155, 155, 155, 255);
                    BSM.battleState = Battle_StateMachine.BattleStates.CheckAlive;

                    alive = false;
                }
                break;
        }
    }

    void updateProgressBar()
    {
        curCooldown = curCooldown + Time.deltaTime;
        float calcualtion = curCooldown / maxCooldown;
        progressBar.transform.localScale = new Vector3(Mathf.Clamp(calcualtion, 0, 1), progressBar.transform.localScale.y, progressBar.transform.localScale.z);

        if (curCooldown >= maxCooldown)
        {
            playerState = TurnState.AddToList;
        }
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

        if (BSM.battleState != Battle_StateMachine.BattleStates.Win && BSM.battleState != Battle_StateMachine.BattleStates.Loss)
        {
            BSM.battleState = Battle_StateMachine.BattleStates.Wait;

            curCooldown = 0f;
            playerState = TurnState.Processing;
        }
        else
        {
            playerState = TurnState.Waiting;
        }
        actionStarted = false;
    }

    public void takeDamage(float dmg)
    {
        player.curHP -= dmg;

        if (player.curHP <= 0)
        {
            player.curHP = 0;
            playerState = TurnState.Dead;
        }

        updatePlayerPanel();
    }

    void doDamage()
    {
        float damageDone = player.playerStr + BSM.turnList[0].chosenAtk.attackDmg;
        targetEnemy.GetComponent<Enemy_StateMachine>().takeDamage(damageDone);
    }

    void createPlayerPanel()
    {
        playerPanel = Instantiate(playerPanel) as GameObject;
        stuff = playerPanel.GetComponent<Player_Panel>();

        stuff.playerName.text = player.playerName;
        stuff.playerHP.text = "HP: " + player.curHP + "/" + player.playerHP;
        stuff.playerMP.text = "MP: " + player.curMP + "/" + player.playerMP;
        progressBar = stuff.progressbar;

        //playerPanel.transform.parent = playerPanelSpacer;
        playerPanel.transform.SetParent(playerPanelSpacer, false);
    }

    void updatePlayerPanel()
    {
        stuff.playerHP.text = "HP: " + player.curHP + "/" + player.playerHP;
        stuff.playerMP.text = "MP: " + player.curMP + "/" + player.playerMP;
    }
}

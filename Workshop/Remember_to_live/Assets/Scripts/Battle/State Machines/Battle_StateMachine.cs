using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Battle_StateMachine : MonoBehaviour
{
    public enum BattleStates
    {
        Wait,
        Action,
        PerformAction,
        CheckAlive,
        Win,
        Loss
    }

    public BattleStates battleState;

    // List of turn order
    public List<Turn_Handler> turnList = new List<Turn_Handler>();

    // List of enemies
    public List<GameObject> enemyList = new List<GameObject>();

    // Player
    public List<GameObject> playerList = new List<GameObject>();

    public GameObject targetButton;
    public Transform Spacer;

    public enum PlayerGUI
    {
        Activate,
        Waiting,
        Input1,
        Input2,
        Done
    }

    public PlayerGUI playerInput;

    public List<GameObject> playerManagerList = new List<GameObject>();
    private Turn_Handler playerChoice;

    // Player GUI panels
    public GameObject attackPanel;
    public GameObject targetPanel;
    public GameObject skillPanel;

    // Attack skills
    public Transform actionSpacer;
    public Transform skillSpacer;
    public GameObject actionButton;
    public GameObject skillsButton;

    private List<GameObject> attackButtons = new List<GameObject>();
    private List<GameObject> targetButtons = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();

    public bool playerIsAlive;

    void Awake()
    {
        for (int i = 0; i < Game_Manager.instance.numEnemies; i++)
        {
            GameObject newEnemy = Instantiate(Game_Manager.instance.enemiesToFight[i], spawnPoints[i].position, Quaternion.identity) as GameObject;
            newEnemy.name = newEnemy.GetComponent<Enemy_StateMachine>().enemy.enemyName + "_" + (i + 1);
            newEnemy.GetComponent<Enemy_StateMachine>().enemy.enemyName = newEnemy.name;
            enemyList.Add(newEnemy);
        }
    }
    void Start()
    {
        battleState = BattleStates.Wait;
        playerList.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        playerInput = PlayerGUI.Activate;

        attackPanel.SetActive(false);
        targetPanel.SetActive(false);
        skillPanel.SetActive(false);

        TargetButtons();
    }

    void Update()
    {
        switch (battleState)
        {
            case (BattleStates.Wait):
                if (turnList.Count > 0)
                {
                    battleState = BattleStates.Action;
                }
                break;

            case (BattleStates.Action):
                GameObject performer = GameObject.Find(turnList[0].attacker);

                if (turnList[0].type == "Enemy")
                {
                    Enemy_StateMachine ESM = performer.GetComponent<Enemy_StateMachine>();
                    for (int i = 0; i < playerList.Count; i++)
                    {
                        if (turnList[0].targetObj == playerList[i])
                        {
                            ESM.targetPlayer = turnList[0].targetObj;
                            ESM.enemyState = Enemy_StateMachine.TurnState.Action;
                            break;
                        }
                        else
                        {
                            turnList[0].attackerObj = playerList[Random.Range(0, playerList.Count)];
                            ESM.targetPlayer = turnList[0].attackerObj;
                            ESM.enemyState = Enemy_StateMachine.TurnState.Action;

                        }
                    }
                }

                if (turnList[0].type == "Player")
                {
                    Player_StateMachine PSM = performer.GetComponent<Player_StateMachine>();
                    PSM.targetEnemy = turnList[0].targetObj;
                    PSM.playerState = Player_StateMachine.TurnState.Action;
                }

                battleState = BattleStates.PerformAction;
                break;

            case (BattleStates.PerformAction):
                break;

            case (BattleStates.CheckAlive):
                if (playerList.Count < 1)
                {
                    battleState = BattleStates.Loss;
                }
                else if (enemyList.Count < 1)
                {
                    battleState = BattleStates.Win;
                }
                else
                {
                    clearAttackPanel();
                    playerInput = PlayerGUI.Activate;
                }
                break;

            case (BattleStates.Win):
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].GetComponent<Player_StateMachine>().playerState = Player_StateMachine.TurnState.Waiting;
                }

                Game_Manager.instance.LoadAfterBattleScene();
                Game_Manager.instance.gameState = Game_Manager.GameState.Overworld;
                Game_Manager.instance.enemiesToFight.Clear();
                break;

            case (BattleStates.Loss):
                Debug.Log("Loss.jpg");
                SceneManager.LoadScene("Main Menu");
                break;
        }

        switch(playerInput)
        {
            case (PlayerGUI.Activate):
                if(playerManagerList.Count > 0)
                {
                    playerManagerList[0].transform.Find("selector").gameObject.SetActive(true);
                    playerChoice = new Turn_Handler();

                    attackPanel.SetActive(true);
                    createAttackButtons();
                    playerInput = PlayerGUI.Waiting;
                }
                break;

            case (PlayerGUI.Waiting):
                break;

            case (PlayerGUI.Done):
                PlayerInputDone();
                break;
        }
    }

    public void CollectActions(Turn_Handler turns)
    {
        turnList.Add(turns);
    }

    public void TargetButtons()
    {
        foreach (GameObject targetBut in targetButtons)
        {
            Destroy(targetBut);
        }
        targetButtons.Clear();

        foreach (GameObject enemy in enemyList)
        {
            GameObject newButton = Instantiate(targetButton) as GameObject;
            Target_SelectButtons button = newButton.GetComponent<Target_SelectButtons>();

            Enemy_StateMachine cur_enemy = enemy.GetComponent<Enemy_StateMachine>();

            TextMeshProUGUI buttonText = newButton.transform.Find("TMP Text").gameObject.GetComponent<TextMeshProUGUI>();

            buttonText.text = cur_enemy.enemy.enemyName;
            button.enemyObj = enemy;

            newButton.transform.SetParent(Spacer, false);
            targetButtons.Add(newButton);
        }
    }

    public void input1()
    {
        playerChoice.attacker = playerManagerList[0].name;
        playerChoice.attackerObj = playerManagerList[0];
        playerChoice.type = "Player";

        playerChoice.chosenAtk = playerManagerList[0].GetComponent<Player_StateMachine>().player.attacks[0];

        attackPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void input2(GameObject targetEnemy)
    {
        playerChoice.targetObj = targetEnemy;
        playerInput = PlayerGUI.Done;
    }

    void PlayerInputDone()
    {
        turnList.Add(playerChoice);

        clearAttackPanel();

        playerManagerList[0].transform.Find("selecter").gameObject.SetActive(false);
        playerManagerList.RemoveAt(0);
        playerInput = PlayerGUI.Activate;
    }

    void clearAttackPanel()
    {
        targetPanel.SetActive(false);
        attackPanel.SetActive(false);
        skillPanel.SetActive(false);

        foreach (GameObject atkButton in attackButtons)
        {
            Destroy(atkButton);
        }
        attackButtons.Clear();
    }

    void createAttackButtons()
    {
        GameObject attackButton = Instantiate(actionButton) as GameObject;
        TextMeshProUGUI attackButtonText = attackButton.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        attackButtonText.text = "Attack";
        attackButton.GetComponent<Button>().onClick.AddListener(() => input1());
        attackButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(attackButton);

        GameObject skillButton = Instantiate(actionButton) as GameObject;
        TextMeshProUGUI skillbuttonText = skillButton.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        skillbuttonText.text = "Skill";
        skillButton.GetComponent<Button>().onClick.AddListener(() => input3());
        skillButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(skillButton);

        if (playerList[0].GetComponent<Player_StateMachine>().player.attacks.Count > 0)
        {
            foreach (Attack_Base skillAttack in playerList[0].GetComponent<Player_StateMachine>().player.attacks)
            {
                GameObject skills = Instantiate(skillsButton) as GameObject;
                TextMeshProUGUI skillText = skills.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

                skillText.text = skillAttack.attackName;
                Skill_Buttons skillB = skills.GetComponent<Skill_Buttons>();
                skillB.skillAttackToPerform = skillAttack;

                skillB.transform.SetParent(skillSpacer, false);
                attackButtons.Add(skills);

            }
        }
        else
        {
            skillButton.GetComponent<Button>().interactable = false;
        }
    }

    public void input4(Attack_Base chosenSkill)
    {
        playerChoice.attacker = playerManagerList[0].name;
        playerChoice.attackerObj = playerManagerList[0];
        playerChoice.type = "Player";

        playerChoice.chosenAtk = chosenSkill;
        skillPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void input3()
    {
        attackPanel.SetActive(false);
        skillPanel.SetActive(true);
    }
}

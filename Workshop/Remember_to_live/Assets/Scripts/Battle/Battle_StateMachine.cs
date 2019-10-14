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

    // List of turn order
    public List<Turn_Handler> turnList = new List<Turn_Handler>();

    // List of enemies
    public List<GameObject> enemyList = new List<GameObject>();

    public GameObject targetButton;

    public enum PlayerGUI
    {
      Activate,
      Input1,
      Input2,
      Done
    }

    public PlayerGUI playerInput;

    public List<GameObject> playerManagerList = new List<GameObject>();

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
      }
    }
    void Start()
    {
        battleState = BattleStates.Start;
        playerIsAlive = true;
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

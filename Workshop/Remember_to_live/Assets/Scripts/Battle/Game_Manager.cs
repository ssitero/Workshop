using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Enemy_OverworldData curEnemy;
    public GameObject playerCharacter;

    // Spawns player
    public Vector3 spawnPlayer;
    // Returning from battle
    public Vector3 returnPlayer;

    public string loadScene;
    public string prevScene;

    public bool playerIsWalking = false;
    public bool enemyCollisionPossible = false;
    public bool enemyCollided = false;

    public enum GameState
    {
        Overworld,
        Safe,
        Battle,
        Idle
    }

    public List<GameObject> enemiesToFight = new List<GameObject>();
    public GameState gameState;

    public int numEnemies;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        if (!GameObject.Find("PlayerCharacter"))
        {
            GameObject Player = Instantiate(playerCharacter, Vector3.zero, Quaternion.identity) as GameObject;
            Player.name = "PlayerCharacter";
        }
    }

    void Update()
    {
        switch (gameState)
        {
            case (GameState.Overworld):
                if (playerIsWalking)
                {
                    Encounter();
                }
                if (enemyCollided)
                {
                    gameState = GameState.Battle;
                }
                break;

            case (GameState.Safe):
                break;

            case (GameState.Battle):
                startBattle();
                gameState = GameState.Idle;
                break;

            case (GameState.Idle):
                break;
        }
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(loadScene);
    }

    public void LoadAfterBattleScene()
    {
        SceneManager.LoadScene(prevScene);
    }

    void Encounter()
    {
        if (enemyCollisionPossible)
        {
            Debug.Log("Enemy Collision");
            enemyCollided = true;
        }
    }

    void startBattle()
    {
        numEnemies = Random.Range(1, curEnemy.maxEnemy + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            enemiesToFight.Add(curEnemy.enemiesList[Random.Range(0, curEnemy.enemiesList.Count)]);
        }

        returnPlayer = GameObject.Find("PlayerCharacter").gameObject.transform.position;

        spawnPlayer = returnPlayer;
        prevScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(curEnemy.loadScene);

        playerIsWalking = false;
        enemyCollisionPossible = false;
        enemyCollided = false;
    }
}

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

    // Spawn enemies to map
    public int enemiesOnMap;
    public GameObject enemyCollider;
    public Transform spawnPrefab;
    public List<GameObject> populateMapWEnemies = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<Transform> enemySpawn = new List<Transform>();

    public int initSize;

    void Awake()
    {
        initSize = spawnPoints.Count;

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

        /*for (int i = 0; i < initSize; i++)
        {
            if (spawnPoints[i] == null)
            {
                spawnPoints.Insert(i, GameObject.FindGameObjectWithTag("Spawn"));
            }
        }*/

        /*Debug.Log("Count: " + spawnPoints.Count);
        if (spawnPoints.Count < initSize)
        {
            spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Spawn"));
        }*/

        if (!GameObject.Find("Enemy"))
        {
            for (int i = 0; i < enemiesOnMap; i++)
            {
                Transform Spawn = Instantiate(spawnPrefab, spawnPoints[i].transform.position, Quaternion.identity) as Transform;
                Spawn.name = "Spawn_" + (i + 1);
                enemySpawn.Add(Spawn);

                GameObject Enemy = Instantiate(enemyCollider, enemySpawn[i].position, Quaternion.identity) as GameObject;
                Enemy.name = "Enemy_" + (i + 1);
                populateMapWEnemies.Add(Enemy);
            }
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
        numEnemies = Random.Range(1, curEnemy.maxEnemy);

        for (int i = 0; i < numEnemies; i++)
        {
            Debug.Log(curEnemy.enemiesList[Random.Range(0, curEnemy.enemiesList.Count)]);
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

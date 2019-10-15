using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{
    public static Manager_Game instance;

    public Enemy_OverworldData currentEnemy;
    public GameObject playerCharacter;

    // Saving player position between scenes
    public Vector3 playerPosNext;
    public Vector3 playerPosPrev;

    // Turn-based battle scene
    public string battleScene;
    public string previousScene;
    public string nextSpawnPoint;

    // Check if player collision with enemy
    public bool enemyEncountered = false;

    // States for where the player is
    public enum GameStates
    {
        OVERWORLD,
        BATTLE,
        IDLE
    }
    public GameStates curGameState;

    // Randomly generates # of enemies to fight on collision, holds them here
    public int numEnemies;
    public List<GameObject> enemiesToBattle = new List<GameObject>();

    void Awake()
    {
        // Check if instance exists
        // Check if instance is set to this GameObject
        if (instance == null)
        {
            instance = this;
        }
        // If instance exists but is not this instance, delete to avoid dupes
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Set current instance to not destroyable
        // Save game data in this instance ---> Figure out how to input and output this to a save file eventually
        DontDestroyOnLoad(gameObject);

        // Spawns player if player doesn't already exist
        if (!GameObject.Find("Player"))
        {
            GameObject Player = Instantiate(playerCharacter, Vector3.zero, Quaternion.identity) as GameObject;
            Player.name = "Player";
        }
    }

    void Update()
    {
        switch(curGameState)
        {
            case (GameStates.OVERWORLD):
                if (enemyEncountered)
                {
                    curGameState = GameStates.BATTLE;
                }
                break;
            case (GameStates.BATTLE):
                loadBattle();
                curGameState = GameStates.IDLE;
                break;
            case (GameStates.IDLE):
                // Idle
                break;
        }
    }

    void loadBattle()
    {
        // Random number of enemies encountered based on type
        // IE some monster types come in multiples, some come alone
        numEnemies = Random.Range(1, currentEnemy.maxEnemies + 1);

        // Generates random variation of enemies to list and is sent to battle
        for (int i = 0; i < numEnemies; i++)
        {
            enemiesToBattle.Add(currentEnemy.enemyVariations[Random.Range(0, currentEnemy.enemyVariations.Count)]);
        }

        // Save player position to load after battle
        playerPosPrev = GameObject.Find("Player").gameObject.transform.position;
        playerPosNext = playerPosPrev;

        // Save name of scene where player was
        previousScene = SceneManager.GetActiveScene().name;

        // Load battle scene for encountered monster
        SceneManager.LoadScene(currentEnemy.battleScene);

        // Reset player character
        enemyEncountered = false;

    }

}

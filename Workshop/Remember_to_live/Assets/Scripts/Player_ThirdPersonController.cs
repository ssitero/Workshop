using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ThirdPersonController : MonoBehaviour
{
    // Input deadzone
    public float inputDelay = 0.1f;
    public float playerSpd;

    public float forwardVelocity = 12f;
    public float rotateVelocity = 100f;
    
    // Holding rotation
    Quaternion targetRotation;

    // Saving position
    Vector3 curPos;
    Vector3 lastPos;

    void Start()
    {
        if (Game_Manager.instance.returnPlayer != Vector3.zero)
        {
            transform.position = Game_Manager.instance.returnPlayer;
            Game_Manager.instance.returnPlayer = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
        curPos = transform.position;

        if (curPos == lastPos)
        {
            Game_Manager.instance.playerIsWalking = false;
        }
        else
        {
            Game_Manager.instance.playerIsWalking = true;
        }
        lastPos = curPos;
    }

    /*void Update()
    {
        PlayerMovement();
    }*/

    void PlayerMovement()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(xMove, 0f, yMove).normalized * playerSpd * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    // DELETE LATER -------------------------------------------

    private void OnTriggerEnter(Collider other)
    {

        /*if(other.tag == "enterexit")
        {

            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            Gmanager.instance.nextspawnpoint = col.spawnpointName;
            Gmanager.instance.sceneToLoad = col.sceneToLoad;
            Gmanager.instance.LoadNewScene();
        }*/

        if(other.tag == "Enemy")
        {
            Enemy_OverworldData enemy = other.gameObject.GetComponent<Enemy_OverworldData>();
            Game_Manager.instance.curEnemy = enemy;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Game_Manager.instance.enemyCollisionPossible = true;
            Game_Manager.instance.populateMapWEnemies.Remove(other.gameObject);
            Game_Manager.instance.enemiesOnMap--;

            //other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Game_Manager.instance.enemyCollisionPossible = false;
        }
    }
}
   

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
        if (Gmanager.instance.prevPlayerPos != Vector3.zero)
        {
            transform.position = Gmanager.instance.prevPlayerPos;
            Gmanager.instance.prevPlayerPos = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
        curPos = transform.position;

        //check if player is moving
        if (curPos == lastPos)
        {
            Gmanager.instance.isWalking = false;
        }
        else
        {
            Gmanager.instance.isWalking = true;
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

        if (other.tag == "dangerzone")
        {
            RegionData region = other.gameObject.GetComponent<RegionData>();
            Gmanager.instance.curRegion = region;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "dangerzone")
        {
            Gmanager.instance.canEncounterEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "dangerzone")
        {
            Gmanager.instance.canEncounterEnemy = false;
        }
    }
}
   

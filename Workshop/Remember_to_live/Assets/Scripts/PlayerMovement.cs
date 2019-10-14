using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private string xInput;
    [SerializeField] private string yInput;
    [SerializeField] private float moveSpd;

    // Checking player movement
    Vector3 curPosition;
    Vector3 prevPosition;

    private CharacterController charController;

    public void Start()
    {
        if (Game_Manager.instance.returnPlayer != Vector3.zero)
        {
            transform.position = Game_Manager.instance.returnPlayer;
            Game_Manager.instance.returnPlayer = Vector3.zero;
        }
    }

    public void FixedUpdate()
    {
        PlayerMove();

        curPosition = transform.position;
        if (curPosition == prevPosition)
        {
            Game_Manager.instance.playerIsWalking = false;
        }
        else
        {
            Game_Manager.instance.playerIsWalking = true;
        }
    }

    private void PlayerMove()
    {
        float xMove = Input.GetAxis(xInput);
        float yMove = Input.GetAxis(yInput);

        Vector3 playerMove = new Vector3(xMove, 0.0f, yMove);

        GetComponent<Rigidbody>().velocity = playerMove * moveSpd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
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
            Debug.Log("Fight possible");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Game_Manager.instance.enemyCollisionPossible = false;
            Debug.Log("Fight impossible");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_SelectButtons : MonoBehaviour
{
    public GameObject enemyObj;


    public void SelectTarget()
    {
        //take info of target objects
        GameObject.Find("BattleManager").GetComponent<Battle_StateMachine>().input2(enemyObj);

    }

    public void toggleOn()
    {
        enemyObj.transform.Find("selector").gameObject.SetActive(true);
    }

    public void toggleOff()
    {
        enemyObj.transform.Find("selector").gameObject.SetActive(false);
    }
}

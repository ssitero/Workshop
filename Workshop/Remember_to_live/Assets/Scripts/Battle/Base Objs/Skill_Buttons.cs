using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Buttons : MonoBehaviour
{
    public Attack_Base skillAttackToPerform;

    public void useSkillAttack()
    {
        GameObject.Find("BattleManager").GetComponent<Battle_StateMachine>().input4(skillAttackToPerform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimStateMachine : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerControl pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        pc.ps.fixedJoystick.armRotate = true;
    }
}

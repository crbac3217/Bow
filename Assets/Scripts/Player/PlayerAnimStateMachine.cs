using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimStateMachine : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerControl pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
#if UNITY_ANDROID
    var joystick = pc.ps.fixedJoystick as FixedJoystick;
#elif UNITY_STANDALONE_WIN
        var joystick = pc.ps.dynamicJoystick as DynamicJoystick;
#endif
        joystick.armRotate = true;
    }
}

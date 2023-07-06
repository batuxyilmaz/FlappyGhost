using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimScript : StateMachineBehaviour
{
    internal bool jumpNow;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 0.30)
            jumpNow = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAnimController : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size = new Vector2(0.2f,GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size.y);
    }
}

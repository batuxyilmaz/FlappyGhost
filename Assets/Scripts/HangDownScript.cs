using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangDownScript : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D _rigidbody2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("Lose");
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
    }
}

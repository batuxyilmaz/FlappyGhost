using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class test : StateMachineBehaviour
{
    private GameObject _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<BoxCollider2D>().enabled = false;
        DOTween.Init();
        BoxCollider2D _boxCollider2D = _player.GetComponent<Test>().platformTransform.GetComponent<BoxCollider2D>();
        _player.transform.position = new Vector3(_player.transform.position.x, _boxCollider2D.bounds.center.y - _boxCollider2D.size.y * 2, -0.9f);
        _player.GetComponent<BoxCollider2D>().size = new Vector2(0.2f,_player.GetComponent<BoxCollider2D>().size.y);
        _player.transform.DOMoveY(_player.GetComponent<Test>().platformTransform.position.y+0.13f,4.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D _rigidbody2D = _player.GetComponent<Rigidbody2D>();
        Animator _animator = _player.GetComponent<Animator>();
        _animator.SetBool("Hang", false);
        _animator.SetBool("Climb", false);
        _animator.SetBool("Jump", false);
        Test _test = _player.GetComponent<Test>();
        _test.isInAir = false;
        _test.test = true;
        _player.GetComponent<BoxCollider2D>().enabled = true;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        //x => GameObject.Find("Player").transform.position.x > 0 ? GameObject.Find("Player").transform.position.x-0.125f : GameObject.Find("Player").transform.position.x+0.125f
        //GameObject.Find("Player").transform.position = new Vector3(GameObject.Find("Player").transform.position.x,_test.platformTransform.position.y, -0.9f);
        _animator.SetBool("Idle", true);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    
    
}

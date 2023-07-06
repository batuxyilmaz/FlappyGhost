using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadTest : MonoBehaviour
{
    public void Test(float force)
    {
        GameObject player = GameObject.Find("Player");
        if (player.GetComponent<Test>().GroundCheck() != null)
        {
            if (player.GetComponent<Test>().GroundCheck().name.Contains("jumppad"))
            {
                player.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(player.GetComponent<Test>().RBForce(new Vector2(player.transform.position.x, force)));
                GameObject.Find("Player").GetComponent<Animator>().SetBool("Falling", false);
                Invoke("SetJump",0f);
            }
        }
    }

    private void SetJump()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("Jump", true);
        if(!GameObject.Find("Canvas").transform.GetChild(1).gameObject.activeInHierarchy)
            GameObject.FindGameObjectWithTag("Controller").GetComponent<AudioScript>().PlayJumpSound();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollision : MonoBehaviour
{
    [SerializeField] private float burnTime;
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<Test>().isBurning)
            {
                //Particle Collides Player
                other.GetComponent<Test>().isBurning = true;
                other.GetComponent<Animator>().SetTrigger("isBurning");
                StartCoroutine(Burning(other));
            }
        }
    }

    private IEnumerator Burning(GameObject other)
    {
        yield return new WaitForSeconds(burnTime);
        Test _test = other.GetComponent<Test>();
        _test.SetFalseAllAnimBools();
        _test.isBurning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.instance.generateScript.GeneratePlatform(PlayerMovement.instance.generateScript.platforms[PlayerMovement.instance.generateScript.platformCount]);
           
        }
       
    }
}

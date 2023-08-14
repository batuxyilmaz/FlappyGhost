using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    private int hitCount;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.gamestate = GameManager.GameState.pause;

            GameManager.instance.End();
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            StartCoroutine(EndDelay());

        }
      
      
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Platform"))
        //{
        //    Destroy(other.gameObject);
        //}
        if (other.gameObject.CompareTag("Bg"))
        {
         
            if(hitCount>0) 
            {
                Destroy(other.transform.parent.gameObject, 0.2f);
            }
         
            hitCount++;
        }
       
    }
        IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.OpenEndGame();
    }
}

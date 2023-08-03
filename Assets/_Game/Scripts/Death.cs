using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
           if(!GameManager.instance.playerEvents.speedActive&&!GameManager.instance.playerEvents.immunity)
            {
                GameManager.instance.hitSound.Play();
                GameManager.instance.flySound.Stop();
                collision.gameObject.GetComponent<Collider>().enabled = false;
                collision.gameObject.GetComponentInParent<Collider>().enabled = false;
                GameManager.instance.gamestate = GameManager.GameState.pause;
                PlayerMovement.instance.hitEffect.Play();
                PlayerMovement.instance.hitEffect.transform.parent = null;
                GameManager.instance.End();
                //collision.transform.parent.transform.GetChild(0).transform.DOScaleY(0.11f, 0.2f).SetEase(Ease.OutSine);
                PlayerMovement.instance.isFalling = true;
                for (int i = 0; i < GameManager.instance.closedThings.Count; i++)
                {
                    GameManager.instance.closedThings[i].SetActive(false);
                }
                GameManager.instance.openObject.SetActive(true);
                StartCoroutine(FailDelay());
                StartCoroutine(EndDelay());
            }
         
        }
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.OpenEndGame();
    }
    IEnumerator FailDelay()
    {
        yield return new WaitForSeconds(0.2f);

        GameManager.instance.player.GetComponent<Collider>().enabled = false;
        GameManager.instance.player.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.8f);

    }
 
}
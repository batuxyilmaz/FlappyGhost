using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlatformMove : MonoBehaviour
{
   
    public float speed;
    private Collider col;
    private int hitCount;
   
  
    
    void Start()
    {
        col=GetComponent<Collider>();
        speed = GameManager.instance.speedObject;

    }

    void Update()
    {
    
        transform.Translate(0, -speed * Time.deltaTime, 0);
      
         
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            GameManager.instance.failed = true;
            PlayerMovement.instance.hitEffect.Play();
            PlayerMovement.instance.hitEffect.transform.parent=null;
            //collision.transform.parent.transform.GetChild(0).transform.DOScaleY(0.11f, 0.2f).SetEase(Ease.OutSine);
            
            StartCoroutine(FailDelay());
            StartCoroutine(EndDelay());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
            StartCoroutine(ColliderOnOff());
            PlayerMovement.instance.stopped = true;
            PlayerMovement.instance.screenBorder.transform.parent = null;
            PlayerMovement.instance.isFalling = false; 

        }


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ColliderOnOff());
            collision.gameObject.transform.parent = null;
            PlayerMovement.instance.stopped = false;
            PlayerMovement.instance.screenBorder.transform.parent = GameManager.instance.player.transform;
            PlayerMovement.instance.screenBorder.transform.localPosition = new Vector3(PlayerMovement.instance.screenBorder.transform.localPosition.x, -5.55f, PlayerMovement.instance.screenBorder.transform.localPosition.z);

        }


    }


    IEnumerator ColliderOnOff()
    {
        //col.enabled= false;
        PlayerMovement.instance.groundControl = true;
        yield return new WaitForSeconds(1);
        PlayerMovement.instance.groundControl = false;
        //col.enabled= true;
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.End();
    }
    IEnumerator FailDelay()
    {
        yield return new WaitForSeconds(0.2f);
       
        GameManager.instance.player.GetComponent<Collider>().enabled = false;
        GameManager.instance.player.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.8f);
       
    }
      
    
   
}

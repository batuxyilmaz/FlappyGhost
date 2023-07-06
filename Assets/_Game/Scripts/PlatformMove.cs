using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlatformMove : MonoBehaviour
{
   
    public float speed;
    private Collider col;
    void Start()
    {
        col=GetComponent<Collider>();
       
        speed = 0.2f;
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
            collision.transform.parent.transform.GetChild(0).transform.DOScaleY(0.11f, 0.2f).SetEase(Ease.OutSine);
            GameManager.instance.playerCam.Follow = null;
            GameManager.instance.playerCam.LookAt = null;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.transform.parent.GetComponent<Collider>().enabled = false;
            StartCoroutine(EndDelay());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ColliderOnOff());
         
           
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
    
   
}

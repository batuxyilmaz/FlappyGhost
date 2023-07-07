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
            PlayerMovement.instance.hitEffect.Play();
            PlayerMovement.instance.hitEffect.transform.parent=null;
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
            GameManager.instance.playerCam.Follow = null;
            GameManager.instance.playerCam.LookAt = null;
            PlayerMovement.instance.screenBorder.transform.parent = null;
         

        }
       

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ColliderOnOff());
            GameManager.instance.playerCam.Follow = GameManager.instance.player.transform;
            GameManager.instance.playerCam.LookAt = GameManager.instance.player.transform;
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
    
   
}

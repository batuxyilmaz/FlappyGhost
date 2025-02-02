using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
   
    public float speed;
    private Collider col;
  
    void Start()
    {
        col=GetComponent<Collider>();
        speed = GameManager.instance.speedObject;

    }

    void Update()
    {

        if (GameManager.instance.gamestate == GameManager.GameState.start)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
   
         
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GameManager.instance.playerEvents.speedActive)
            {
                collision.gameObject.transform.parent = gameObject.transform;
                StartCoroutine(ColliderOnOff());
                PlayerMovement.instance.stopped = true;
                PlayerMovement.instance.screenBorder.transform.parent = null;
                PlayerMovement.instance.isFalling = false;
            }
           
          

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GameManager.instance.playerEvents.speedActive)
            {
                StartCoroutine(ColliderOnOff());
                collision.gameObject.transform.parent = null;
                PlayerMovement.instance.stopped = false;
                PlayerMovement.instance.screenBorder.transform.parent = GameManager.instance.player.transform;
                PlayerMovement.instance.screenBorder.transform.localPosition = new Vector3(PlayerMovement.instance.screenBorder.transform.localPosition.x, -9f, PlayerMovement.instance.screenBorder.transform.localPosition.z);
            }

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
   
      
   
}

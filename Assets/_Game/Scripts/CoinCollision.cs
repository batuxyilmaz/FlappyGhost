using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;


public class CoinCollision : MonoBehaviour
{
    public ParticleSystem coinEffect;
    private bool isTaken;
    float followSpeed;
    private void Start()
    {
        followSpeed = 0.2f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gather"))
        {
            if(GameManager.instance.gamestate==GameManager.GameState.start)
            {
                if (!UiManager.instance.hapticActive)
                {
                    Haptic.LightTaptic();
                  
                }
               
                GameManager.instance.collectSound.Play();
                GameManager.instance.playerEvents.isTaken = true;
                GameManager.instance.point += 1;
                GameManager.instance.currentMoney += 1;
                GameManager.instance.coinText.text = GameManager.instance.point.ToString();
                PlayerPrefs.SetInt("Point", GameManager.instance.point);
                Destroy(gameObject, 1f);
                //coinEffect.transform.parent = null;
                coinEffect.Play();
                transform.GetChild(1).gameObject.SetActive(false);
            }
            
        }
        if (other.gameObject.CompareTag("Platform"))
        {
            if (!isTaken)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
            }
           
        }
      
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
          
           // transform.Translate(other.gameObject.transform.position*2f*Time.deltaTime);
            isTaken = true;
            if (!GameManager.instance.playerEvents.speedActive)
            {
                transform.DOMove(other.gameObject.transform.position, followSpeed).SetEase(Ease.Linear);
                followSpeed -= 0.005f;
            }
            if (!isTaken)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
            }

        }
    }
   
}

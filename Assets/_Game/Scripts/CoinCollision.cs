using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;


public class CoinCollision : MonoBehaviour
{
    public ParticleSystem coinEffect;
    private bool isTaken;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gather"))
        {
            Debug.Log("dasda");
            GameManager.instance.playerEvents.isTaken = true;
            GameManager.instance.point += 10;
            GameManager.instance.coinText.text = GameManager.instance.point.ToString();
            PlayerPrefs.SetInt("Point", GameManager.instance.point);
            coinEffect.transform.parent = null;
            coinEffect.Play();
            transform.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Platform"))
        {
            if (!isTaken)
            {
                gameObject.SetActive(false);
            }
           
        }
      
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
           // transform.Translate(other.gameObject.transform.position*2f*Time.deltaTime);
            isTaken = true;
            transform.DOMove(other.gameObject.transform.position, 0.2f).SetEase(Ease.Linear);
           
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CoinCollision : MonoBehaviour
{
    public ParticleSystem coinEffect;
    private bool isTaken;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.point += 10;
            GameManager.instance.coinText.text = GameManager.instance.point.ToString();
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
            isTaken = true;
            transform.DOMove(other.gameObject.transform.position, 0.2f).SetEase(Ease.OutSine);
           
        }
    }
}

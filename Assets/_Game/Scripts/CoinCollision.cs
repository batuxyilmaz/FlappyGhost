using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public ParticleSystem coinEffect;
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
    }
}

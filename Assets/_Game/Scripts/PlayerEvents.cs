using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public bool magnetActive;
    private float magnetTimer;
    public GameObject magnetCol;

    public bool speedActive;
    private float speedTimer;
 
    void Update()
    {
        if (magnetActive)
        {
            magnetTimer += Time.deltaTime;
            UiManager.instance.images[0].fillAmount -= Time.deltaTime / 5;
        }
        if(magnetTimer > 5f)
        {
            magnetCol.SetActive(false);
            magnetActive = false;
            magnetTimer = 0f;
            UiManager.instance.images[0].fillAmount = 1f;
            DOTween.Restart("MagnetClose");
            UiManager.instance.images[0].gameObject.SetActive(false);
        }
        if(speedActive)
        {
            speedTimer += Time.deltaTime;
            UiManager.instance.images[1].fillAmount -= Time.deltaTime / 5;
        }
        if(speedTimer > 5f)
        {
            speedActive = false;
            speedTimer = 0f;
            PlayerMovement.instance.forcePower = 10f;
            DOTween.Restart("SpeedClose");
            UiManager.instance.images[1].fillAmount = 1f;
            UiManager.instance.images[1].gameObject.SetActive(false);
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvents : MonoBehaviour
{
    public bool magnetActive;
    public float magnetTimer;
    public GameObject magnetCol;

    public bool speedActive;
    public float speedTimer;
    public bool immunity;
    private bool fadeActive;
    private float fadeValue;
    
    public float speedTime;
    public float magnetTime;
    
   

    public int magnetId;
    public int speedId;
    public bool magnetUpgraded;
    public bool speedUpgraded;
    public bool startDecrease;
    public bool isTaken;
    public bool waitBoost;

    public GameObject lightningTrail;
    private int increaseValue;
    public float barIncreaseValue;
    public GameObject spawnPos;
    private Collider playerCol;
    private void Awake()
    {
     
        magnetId = PlayerPrefs.GetInt("MagnetId");
        speedId = PlayerPrefs.GetInt("SpeedId");
        if (magnetId >= 1)
        {
            magnetUpgraded = true;
        }
        if (speedId >= 1)
        {
            speedUpgraded = true;
        }
    }
    private void Start()
    {
        playerCol=GetComponent<Collider>();

        increaseValue = 40;
        if (magnetUpgraded)
        {
            magnetTime = PlayerPrefs.GetFloat("MagnetTime");
        }
        else
        {
            magnetTime = 5f;
        }
      
        if (speedUpgraded)
        {
            speedTime = PlayerPrefs.GetFloat("SpeedTime");
        }
        else
        {
            speedTime = 5f;
        }
    
       
        fadeValue = 0.2f;
      
       
    }
    void Update()
    {
       
        if (isTaken)
        {

            StartCoroutine(BarFill());
            isTaken = false;
        }
        if (magnetActive)
        {
            magnetTimer += Time.deltaTime;
           
        }
        //if (magnetTimer > 2.5f)
        //{

        //    if (!fadeActive)
        //    {
        //        StartCoroutine(Fade());
        //        fadeActive = true;
        //    }

        //}
        if (magnetTimer > 5f)
        {
            fadeActive = false; 
            magnetCol.SetActive(false);
            magnetActive = false;
            GameManager.instance.magnetEffect.Stop();
            magnetTimer = 0f;
          
        }
        if(speedActive)
        {
            speedTimer += Time.deltaTime;
            
        }
        //if (speedTimer > 2.5f)
        //{
          
            //if(!fadeActive)
            //{
            //    StartCoroutine(Fade());
            //    fadeActive = true;
            //}
          
       // }
        if(speedTimer > 5f)
        {
            fadeActive = false;
            speedActive = false;
            speedTimer = 0f;
            StartCoroutine(ImmunityDealy());
            startDecrease = true;
            waitBoost = false;
            PlayerMovement.instance.currentSpeed = 5;
            DOTween.Restart("RotateSlow");
            lightningTrail.SetActive(false);
            GameManager.instance.boostSound.Stop();
          

        }
        if(startDecrease)
        {
            if (PlayerMovement.instance.currentSpeed > PlayerMovement.instance.oldSpeed)
            {
                PlayerMovement.instance.currentSpeed -=4f*Time.deltaTime;
                
            }
            else
            {
                startDecrease=false;
            }
           

        }
    }
    //private IEnumerator Fade()
    //{
    //    yield return new WaitForSeconds(0.4f);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //    yield return new WaitForSeconds(fadeValue);
    //    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

    //}
  
    private IEnumerator ImmunityDealy()
    {
        yield return new WaitForSeconds(2f);
        playerCol.enabled = true;
        immunity = false;
    }
    public IEnumerator BarFill()
    {
      
        for (int i = 0; i < 5; i++)
        {
            UiManager.instance.speedbarImage.fillAmount += barIncreaseValue;
            
            yield return new WaitForSeconds(0.005f);
        }
        if (!waitBoost)
        {
            if (UiManager.instance.speedbarImage.fillAmount >= 0.99f)
            {
                GameManager.instance.speedUiSound.Play();
                playerCol.enabled = false;
                UiManager.instance.fadeText.SetActive(true);
                GameObject FadeText=Instantiate(UiManager.instance.fadeText, spawnPos.transform.position, Quaternion.identity);
                FadeText.transform.parent = spawnPos.transform;
                Destroy(FadeText, 2f);
                //barIncreaseValue -= 0.005f;
                speedActive = true;
                waitBoost = true;
                DOTween.Restart("BarFinish");
                GameManager.instance.boostSound.Play();
                yield return new WaitForSeconds(1.5f);
                UiManager.instance.speedbarImage.fillAmount = 0f;
                lightningTrail.SetActive(true);
                DOTween.Pause("BarFinish");
                speedTimer = 0f;
                DOTween.Restart("RotateFast");
                PlayerMovement.instance.oldSpeed = PlayerMovement.instance.currentSpeed;
                GameManager.instance.playerEvents.immunity = true;
                UiManager.instance.speedImage.transform.Rotate(0, 0, -10f * Time.deltaTime);
                PlayerMovement.instance.currentSpeed = increaseValue;
                GameManager.instance.speedText.text = Mathf.RoundToInt(PlayerMovement.instance.currentSpeed*4).ToString();
                PlayerMovement.instance.newSpeed = PlayerMovement.instance.currentSpeed;

            }
        }
     
       
    }
    
}

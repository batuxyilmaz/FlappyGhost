using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    private bool startDecrease;
    public bool isTaken;
    public bool waitBoost;
    
    public ParticleSystem effect;
    private int increaseValue;
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
        if (magnetTimer > 2.5f)
        {

            if (!fadeActive)
            {
                StartCoroutine(Fade());
                fadeActive = true;
            }

        }
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
        if (speedTimer > 2.5f)
        {
          
            if(!fadeActive)
            {
                StartCoroutine(Fade());
                fadeActive = true;
            }
          
        }
        if(speedTimer > 5f)
        {
            fadeActive = false;
            speedActive = false;
            speedTimer = 0f;
            StartCoroutine(ImmunityDealy());
            startDecrease = true;
            waitBoost = false;
            PlayerMovement.instance.currentSpeed = 12;
        
        }
        if(startDecrease)
        {
            if (PlayerMovement.instance.currentSpeed > PlayerMovement.instance.oldSpeed)
            {
                PlayerMovement.instance.currentSpeed -= Time.deltaTime * 40;
            }
            else
            {
                startDecrease=false;
            }
           

        }
    }
    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(fadeValue);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

    }
  
    private IEnumerator ImmunityDealy()
    {
        yield return new WaitForSeconds(2f);
        immunity = false;
    }
    public IEnumerator BarFill()
    {
      
        for (int i = 0; i < 5; i++)
        {
            UiManager.instance.speedbarImage.fillAmount += 0.01f;
            
            yield return new WaitForSeconds(0.005f);
        }
        if (!waitBoost)
        {
            if (UiManager.instance.speedbarImage.fillAmount >= 0.99f)
            {
                waitBoost = true;
                DOTween.Restart("BarFinish");
                Debug.Log("dasd");
                yield return new WaitForSeconds(1.5f);
                UiManager.instance.speedbarImage.fillAmount = 0f;
                effect.Play();
                effect.gameObject.transform.parent = null;
                DOTween.Pause("BarFinish");
                speedTimer = 0f;
                speedActive = true;
                PlayerMovement.instance.oldSpeed = PlayerMovement.instance.currentSpeed;
                GameManager.instance.playerEvents.immunity = true;
                PlayerMovement.instance.currentSpeed = increaseValue;
                PlayerMovement.instance.newSpeed = PlayerMovement.instance.currentSpeed;

                if (PlayerMovement.instance.currentSpeed >= 20)
                {
                    increaseValue = 60;
                }


            }
        }
       
       
       
    }
    //private void BarColorChange()
    //{
    //    if (UiManager.instance.speedbarImage.fillAmount >= 0.5f && UiManager.instance.speedbarImage.fillAmount < 0.69f)
    //    {
    //        DOTween.Restart("BarYellow");
    //    }
    //    if (UiManager.instance.speedbarImage.fillAmount >= 0.7f)
    //    {
    //        DOTween.Restart("BarGreen");
    //    }

    //}
    
}

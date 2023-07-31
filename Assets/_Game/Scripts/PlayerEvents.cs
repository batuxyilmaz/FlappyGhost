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
    private float speedDecreasetimeValue;
    public float speedTime;
    public float magnetTime;

    public int magnetId;
    public int speedId;
    public bool magnetUpgraded;
    public bool speedUpgraded;
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
      
       
        if(magnetUpgraded)
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
        speedDecreasetimeValue = 0.05f;
    }
    void Update()
    {
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
            StartCoroutine(SpeedDecrease());
        
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
    private IEnumerator SpeedDecrease()
    {
        PlayerMovement.instance.forcePower -= 5f;
        yield return new WaitForSeconds(speedDecreasetimeValue);
        PlayerMovement.instance.forcePower -= 5f;
        yield return new WaitForSeconds(speedDecreasetimeValue);
        PlayerMovement.instance.forcePower -= 5f;
        yield return new WaitForSeconds(speedDecreasetimeValue);
        PlayerMovement.instance.forcePower -= 5f;
        yield return new WaitForSeconds(speedDecreasetimeValue);
        PlayerMovement.instance.forcePower -= 5f;
        yield return new WaitForSeconds(speedDecreasetimeValue);
        PlayerMovement.instance.forcePower -= 5f;
       

    }
    private IEnumerator ImmunityDealy()
    {
        yield return new WaitForSeconds(1f);
        immunity = false;
    }
}

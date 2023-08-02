using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using EKTemplate;
using TMPro;
using ToonyColorsPro;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
   
    public BoxCollider ground;
    public Rigidbody rb;
    public GenerateObjects generateScript;
    public ParticleSystem hitEffect;
    public GameObject screenBorder;
    public static PlayerMovement instance;
    public Animator playerAnim;
    public TextMeshProUGUI playerHeightText;
    public TextMeshProUGUI changeValue;
    public TextMeshProUGUI secondchangeValue;
    public int level;
    public int secondLevel;
    public GameObject trail;

    private float xValOffset;
    public float multiplier;
    public float speedVal;
    private int value;
    public float delayTime;
    public float currentSpeed;
    private float slidetimer;
    public int touchCount;
    private int heightCount;
    private float holdTimer;

    public bool slideControl;
    public bool groundControl;
    public bool tap;
    public bool isFalling;
    public bool stopped;
    public bool changed;
    private bool holding;
    private bool onOff;
    private float speedIncreaseValue;
    public float newSpeed;
    public float oldSpeed;
    public int changeCount;
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        value = 50;
        level = 1;
        secondLevel = 1;
        speedIncreaseValue = 0.5f;
        changeCount = 260;
       
    }
    private void Update()
    {
        if (holding)
        {
            holdTimer += Time.deltaTime;
            if(holdTimer > 0.5f)
            {
                holdTimer = 0f;
                if (currentSpeed <= 25)
                {
                    currentSpeed += 0.4f;
                }
               
            }
        }

        if (GameManager.instance.playerEvents.speedActive)
        {
            transform.Translate(0, currentSpeed * Time.deltaTime, 0);
        }
        if (UiManager.instance.barImage.fillAmount >= 1)
        {
            UiManager.instance.barImage.fillAmount = 0f;
            UiManager.instance.ghostImage.rectTransform.anchoredPosition = new Vector3(-6.1f, -234f,0f); 
            if (level <= 8)
            {
                level++;
            }
            else
            {
                level = 1;
                secondLevel++;
                secondchangeValue.text = secondLevel.ToString();
            }
              
            changeValue.text = level.ToString();
           
        }
        ChangeLocation(offsetX, -6f, 6f);
        if (heightCount >= 1)
        {
            if (heightCount % 30 == 0 && !onOff)
            {
             
                generateScript.GenerateBg();
                generateScript.GeneratePlatform(generateScript.platforms[generateScript.platformCount]);
                StartCoroutine(Delay());
            }
            if (heightCount % 200 == 0&& !onOff)
            {
               
                if (currentSpeed < 20f)
                {
                    
                    currentSpeed += speedIncreaseValue;
                    StartCoroutine(Delay());
                   
                }

            }

            if(heightCount % 40 == 0&& !onOff)
            {
              
                generateScript.GeneratePowerUp();
                StartCoroutine(Delay());
           
            }
            if (heightCount % 820 == 0 && !onOff)
            {
                StartCoroutine(Delay());
                generateScript.bgsCurrentCount = 0;
                generateScript.platformCount=0;
                generateScript.sugarCount = 0;

            }
            if (heightCount % changeCount == 0 && !onOff)
            {
             
                StartCoroutine(Delay());
                generateScript.bgsCurrentCount++;
                generateScript.platformCount++;
                generateScript.sugarCount++;
                changeCount += 260;
            
            }
            if (heightCount % 100 == 0 && !onOff)
            {
                StartCoroutine(Delay());
                generateScript.GenerateGif();
             

            }


        }

        if (isFalling)
        {
            if (!GameManager.instance.playerEvents.speedActive)
            {
                transform.Translate(0, -currentSpeed / 2 * Time.deltaTime, 0); //Decent
                //UiManager.instance.barImage.fillAmount -= Time.deltaTime / 20;
                //if (UiManager.instance.ghostImage.rectTransform.anchoredPosition.y >= -234f)
                //{
                //    UiManager.instance.ghostImage.rectTransform.Translate(-24 * Time.deltaTime, 0, 0);
                //}
          
            }
        }


        heightCount = Mathf.RoundToInt(transform.position.y);
        if (heightCount <= 0)
        {
            heightCount = 0;
            playerHeightText.text = heightCount.ToString();
        }
        else
        {
            playerHeightText.text = heightCount.ToString();
        }
      

        if (Input.GetMouseButtonDown(0))
        {
            if(GameManager.instance.gamestate==GameManager.GameState.start)
            {
                Vector3 firstPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                xValOffset = (firstPos.x - .5f) * multiplier - transform.position.x;


                isFalling = false;
                tap = true;
                screenBorder.transform.parent = GameManager.instance.player.transform;
                screenBorder.transform.localPosition = new Vector3(screenBorder.transform.localPosition.x,-9f, screenBorder.transform.localPosition.z);

                playerAnim.SetBool("Falling", false);

            }
         

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.instance.gamestate == GameManager.GameState.start)
            {
                holding = false;
                slideControl = false;
                slidetimer = 0f;
                tap = false;
                if (!GameManager.instance.playerEvents.speedActive)
                {
                    isFalling = true;

                }
                currentSpeed = 12f;
                screenBorder.transform.parent = null;
                playerAnim.SetBool("Falling", true);
            }
            

        }
        if (Input.GetMouseButton(0))
        {
        
            if (GameManager.instance.gamestate==GameManager.GameState.start)
            {
                holding = true;
                slidetimer += Time.deltaTime;
                //if(GameManager.instance.playerEvents.speedActive)
                //{
                //    if (currentSpeed >= 20)
                //    {
                //        UiManager.instance.barImage.fillAmount += Time.deltaTime / 20 * 5;
                //        UiManager.instance.ghostImage.rectTransform.Translate(120 * Time.deltaTime, 0, 0);
                //    }
                //    else
                //    {
                //        UiManager.instance.barImage.fillAmount += Time.deltaTime / 20 * 3;
                //        UiManager.instance.ghostImage.rectTransform.Translate(72 * Time.deltaTime, 0, 0);
                //    }
                  
                //}
                //else
                //{
                //    UiManager.instance.barImage.fillAmount += Time.deltaTime / 20;
                //    UiManager.instance.ghostImage.rectTransform.Translate(24 * Time.deltaTime, 0, 0);
                //}
              
                if (!isFalling)
                {
                    if (!GameManager.instance.playerEvents.speedActive)
                    {
                        transform.Translate(0, currentSpeed * Time.deltaTime, 0); //Ascend
                    }
                }


                if (slidetimer >= 0.2f)
                {
                    slideControl = true;
                    slidetimer = 0;
                }
                if (slideControl)
                {
                    SlideControl();
                }
            }
           

        }   
    }
    private float offsetX = 0f;
    private void SlideControl()
    {
        if(!changed)
        {
            Vector3 currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, new Vector3((currentPos.x - 0.5f) * multiplier - xValOffset, transform.position.y, transform.position.z), value * Time.deltaTime);
            Vector3 pos = transform.position;
            offsetX = Mathf.Clamp(pos.x, ground.bounds.min.x + 0.5f, ground.bounds.max.x - 0.5f);
            pos.x = offsetX;

            if (GameManager.instance.tutorial.activeSelf)
            {
                if(transform.position.x>=3f || transform.position.x <= -3f)
                {
                    GameManager.instance.tutId = 1;
                    PlayerPrefs.SetInt("TutId", GameManager.instance.tutId);

                    GameManager.instance.tutorial.SetActive(false);
                }
      
            }
        }
      
    }
    private void ChangeLocation(float positionX, float leftPos, float rightPos)
    {

        positionX = transform.position.x;

        if (positionX <= -9f)
        {
            StartCoroutine(ChangeChange());
            transform.position = new Vector3(rightPos, transform.position.y, transform.position.z);
        }
        if (positionX >= 9f)
        {
            StartCoroutine(ChangeChange());
            transform.position = new Vector3(leftPos, transform.position.y, transform.position.z);
        }
    }
    IEnumerator ChangeChange()
    {
        changed = true;
        trail.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(0.5f);
        changed = false;
        trail.GetComponent<ParticleSystem>().Play();
    }
    IEnumerator Delay()
    {
        onOff = true;
        yield return new WaitForSeconds(0.5f);
        onOff = false;
    }
   
}



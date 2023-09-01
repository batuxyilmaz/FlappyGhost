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
using Unity.VisualScripting;
using GoogleMobileAds.Api;

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
    public float changeCount;
    public float platformchangeCount;
    private bool soundStart;
    public Animator mainAnim;
    public float speedLimit;
    private float eyechangeTimer;
    public int eyeChangeValue;
    private float startCount;
    private int multiply;
    private float spawnHeight;
    private bool eyeOpened;
    private int rateCount;
    private bool windActive;
    public ParticleSystem windEffect;
    public ParticleSystem shieldEffect;
    public bool Death;
    private float tempOffset;
    private bool leftRight;
    private bool left;
    private bool right;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainAnim = GetComponent<Animator>();
        value = 30;
        level = 1;
        secondLevel = 1;
        speedIncreaseValue = 0.5f;
        changeCount = 230f;
        speedLimit = 0;
        platformchangeCount = 80f;
        startCount = 6000;
        multiply = 2;
        spawnHeight = 30f;
        eyeChangeValue = 5;
        rateCount = 5;
    }
    private void Update()
    {
        if (heightCount >= 1)
        {
          
            if (generateScript.spawnedPrefabsCount < 5)
            {
                generateScript.GenerateBg();
                generateScript.spawnedPrefabsCount++;
            }
            if (heightCount % 200 == 0 && !onOff)
            {
                generateScript.GeneratePowerUp();
                StartCoroutine(Delay());

            }

            if (transform.position.y >= startCount)
            {
                if (!onOff)
                {
                    StartCoroutine(Delay());

                    generateScript.bgsCurrentCount = 0;

                    if (generateScript.childCount < 2)
                    {
                        generateScript.childCount++;
                    }
                    else
                    {
                        generateScript.childCount=0;
                    }

                    startCount += startCount;

                }

            }
            if (transform.position.y >= changeCount)
            {
                if (!onOff)
                {
                    StartCoroutine(Delay());

                    if (generateScript.bgsCurrentCount < 18)
                    {
                        generateScript.bgsCurrentCount++;
                      
                        if (generateScript.bgsCurrentCount > 1)
                        {
                            changeCount += 500 * multiply;
                            multiply += 3;
                        }
                        else
                        {
                            changeCount += 500;
                        }
                        if (generateScript.bgsCurrentCount >= 2)
                        {

                        }

                    }

                }

            }

        }
        if (!GameManager.instance.playerEvents.speedActive && currentSpeed>=10f && !windActive)
        {
            windEffect.Play();
            windActive = true;
        }
        if (!GameManager.instance.playerEvents.speedActive && GameManager.instance.gamestate==GameManager.GameState.start)
        {
            
            GameManager.instance.speedText.text = Mathf.RoundToInt(speedLimit*4).ToString()+" "+"km";
           
        }
   
        DeathControl();
        if (holding)
        {
            var emission = windEffect.emission;
        
            if(GameManager.instance.gamestate == GameManager.GameState.start)
            {
                holdTimer += Time.deltaTime;
                if (holdTimer > 0.5f)
                {
                    holdTimer = 0f;
                    if (currentSpeed <= 40 && !UiManager.instance.speedLocked)
                    {
                        if (windActive)
                        {
                            emission.rateOverTime = (rateCount + 1);
                            if (rateCount < 12 && !GameManager.instance.playerEvents.speedActive)
                            {
                                rateCount++;
                            }
                        }


                        currentSpeed += 0.4f;
                        speedLimit += 0.4f;
                        if (multiplier < 25f)
                        {
                            multiplier += 0.2f;
                        }

                    }

                }
            }
          
        }

        if (GameManager.instance.playerEvents.speedActive||UiManager.instance.speedLocked)
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
   
       

        if (isFalling)
        {
            if (!GameManager.instance.playerEvents.speedActive)
            {
                if (Death)
                {
                    transform.Translate(0, -currentSpeed / 2 * Time.deltaTime, 0); //Decent
                }

                //eyechangeTimer += Time.deltaTime;
                if (windActive)
                {
                    windEffect.Stop();
                    rateCount = 5;
                    windActive = false;
                }

                //if (eyechangeTimer >= 0.25f)
                //{
                //    GameManager.instance.eyeObject.transform.GetChild(eyeChangeValue).gameObject.SetActive(false);
                //    if (eyeChangeValue > 0)
                //    {
                //        eyeChangeValue--;
                //    }

                //    GameManager.instance.eyeObject.transform.GetChild(eyeChangeValue).gameObject.SetActive(true);
                //    eyechangeTimer = 0;
                //}

                if (currentSpeed >= 5f)
                {
                    currentSpeed -= 4f * Time.deltaTime * 2;
                    if (speedLimit < 0)
                    {
                        GameManager.instance.speedText.text = 0.ToString() + " " + "km";
                    }
                    else
                    {
                        speedLimit -= 4f * Time.deltaTime * 2;
                    }

                    if (multiplier > 15)
                    {
                        multiplier -= 0.2f;
                    }


                }

            }
        }


        heightCount = Mathf.RoundToInt(transform.position.y);
        if (heightCount <= 0)
        {
            heightCount = 0;
            playerHeightText.text = (heightCount.ToString()+"M");
        }
        else
        {
            playerHeightText.text = (heightCount.ToString()+"M");
        }
      

        if (Input.GetMouseButtonDown(0) && !tap)
        {
          
            if (GameManager.instance.gamestate==GameManager.GameState.start)
            {
                //if(!eyeOpened)
                //{
                //    for (int i = 0; i < GameManager.instance.eyeObject.transform.childCount; i++)
                //    {
                //        GameManager.instance.eyeObject.transform.GetChild(i).gameObject.SetActive(false);
                //    }
                //    GameManager.instance.eyeObject.transform.GetChild(5).gameObject.SetActive(true);
                //    eyeOpened = true;
                //}
               
                GameManager.instance.eyeObject.GetComponent<Animator>().enabled = false;
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
                GameManager.instance.flySound.GetComponent<Animator>().SetTrigger("Off");
                StartCoroutine(VolumeChange());
                soundStart = false;
                holding = false;
                slideControl = false;
                if (leftRight)
                {
                    playerAnim.SetBool("Hold", false);
                    if (playerAnim.GetBool("Left"))
                    {
                        playerAnim.SetBool("Left", true);
                    }
                    else
                    {

                        playerAnim.SetBool("Left", false);
                    }
                    leftRight = false;
                }
          
              
                slidetimer = 0f;
                tap = false;
                playerAnim.SetBool("Falling", true);
                if (!GameManager.instance.playerEvents.speedActive)
                {
                    isFalling = true;
                }



            }
            

        }
        if (Input.GetMouseButton(0))
        {
          
            if (GameManager.instance.gamestate==GameManager.GameState.start)
            {
                if (!soundStart)
                {
                    GameManager.instance.flySound.Play();
                    GameManager.instance.flySound.volume=1f;
                    if (!UiManager.instance.soundActive)
                    {
                        GameManager.instance.flySound.mute = false;
                    }
                  

                    soundStart = true;
                }
              
                holding = true;
                slidetimer += Time.deltaTime;
               

              
                if (!isFalling)
                {
                    if (!GameManager.instance.playerEvents.speedActive)
                    {
                        if (!UiManager.instance.speedLocked)
                        {
                            transform.Translate(0, currentSpeed * Time.deltaTime, 0); //Ascend


                            eyechangeTimer += Time.deltaTime;
                            if (eyechangeTimer >= 1.6f)
                            {
                                GameManager.instance.eyeObject.transform.GetChild(eyeChangeValue).gameObject.SetActive(false);
                                if (eyeChangeValue <= 28)
                                {
                                    eyeChangeValue++;
                                }

                                GameManager.instance.eyeObject.transform.GetChild(eyeChangeValue).gameObject.SetActive(true);
                                eyechangeTimer = 0;
                            }
                        }
                       

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
    IEnumerator LastPos()
    {
        yield return new WaitForSeconds(0.1f);
        tempOffset = offsetX;
        
    }
    private void SlideControl()
    {
        if(!changed)
        {
            Vector3 currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, new Vector3((currentPos.x - 0.5f) * multiplier - xValOffset, transform.position.y, transform.position.z), value * Time.deltaTime);
            Vector3 pos = transform.position;
            offsetX = Mathf.Clamp(pos.x, ground.bounds.min.x + 0.5f, ground.bounds.max.x - 0.5f);
            pos.x = offsetX;
            StartCoroutine(LastPos());
         
           
            if (Mathf.RoundToInt(tempOffset) > Mathf.RoundToInt(offsetX) )
            {
                //left
                leftRight = true;
                if (!playerAnim.GetBool("Left"))
                {
                    playerAnim.SetBool("Left", false);
                    playerAnim.SetBool("Hold", true);
                }
                
                playerAnim.SetBool("Left", true);


            }
            if(Mathf.RoundToInt(tempOffset) < Mathf.RoundToInt(offsetX))
            {
                if (playerAnim.GetBool("Left"))
                {
                    playerAnim.SetBool("Left", true);
                    playerAnim.SetBool("Hold", true);
                }

                playerAnim.SetBool("Left", false);
                leftRight = true;
                //right

            }
            if (tempOffset== offsetX)
            {
                if (leftRight)
                {
                    playerAnim.SetBool("Hold", false);
                    if (playerAnim.GetBool("Left"))
                    {
                        playerAnim.SetBool("Left", true);
                    }
                    else
                    {

                        playerAnim.SetBool("Left", false);
                    }
                    leftRight = false;
                }
            }

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
   
    IEnumerator Delay()
    {
        onOff = true;
        yield return new WaitForSeconds(0.5f);
        onOff = false;
    }
    private void DeathControl()
    {
        if (transform.position.x >= 10f || transform.position.x<=-10f)
        {
            GameManager.instance.End();
            StartCoroutine(EndDelay());
            StartCoroutine(FailDelay());
            GameManager.instance.gamestate = GameManager.GameState.pause;
        }
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.OpenEndGame();
    }
    IEnumerator FailDelay()
    {
        yield return new WaitForSeconds(0.2f);

        GameManager.instance.player.GetComponent<Collider>().enabled = false;
        GameManager.instance.player.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.8f);

    }
    IEnumerator VolumeChange()
    {
        yield return new WaitForSeconds(0.5f);
        if (!UiManager.instance.soundActive)
        {
            GameManager.instance.flySound.mute = true;
        }
      
    }
   
}



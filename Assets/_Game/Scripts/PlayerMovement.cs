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
    public float forcePower;
    private float slidetimer;
    public int touchCount;
    private int heightCount;

    public bool slideControl;
    public bool groundControl;
    public bool tap;
    public bool isFalling;
    public bool stopped;
    public bool changed;
    private bool onOff;
   
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        value = 50;
        level = 1;
        secondLevel = 1;
       
    }
    private void Update()
    {
        if (GameManager.instance.playerEvents.speedActive)
        {
            transform.Translate(0, forcePower * Time.deltaTime, 0);
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
            if (heightCount % 20 == 0 && !onOff)
            {
             
                generateScript.GenerateBg();
                generateScript.GeneratePlatform(generateScript.platform);
                StartCoroutine(Delay());
            }
            if (heightCount % 500 == 0&& !onOff)
            {
                if (forcePower < 20f)
                {
                    forcePower += 0.5f;
                    StartCoroutine(Delay());

                }

            }
            if(heightCount % 155 == 0&& !onOff)
            {
                Debug.Log("geldi");
                generateScript.GeneratePowerUp();
                StartCoroutine(Delay());
           
            }
           
        }
   
        if (isFalling)
        {
            if (!GameManager.instance.playerEvents.speedActive)
            {
                transform.Translate(0, -forcePower / 2 * Time.deltaTime, 0); //Decent
                UiManager.instance.barImage.fillAmount -= Time.deltaTime / 20;
                if (UiManager.instance.ghostImage.rectTransform.anchoredPosition.y >= -234f)
                {
                    UiManager.instance.ghostImage.rectTransform.Translate(-24 * Time.deltaTime, 0, 0);
                }
          
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
                slideControl = false;
                slidetimer = 0f;
                tap = false;
                if (!GameManager.instance.playerEvents.speedActive)
                {
                    isFalling = true;

                }

                screenBorder.transform.parent = null;
                playerAnim.SetBool("Falling", true);
            }
            

        }
        if (Input.GetMouseButton(0))
        {
        
            if (GameManager.instance.gamestate==GameManager.GameState.start)
            {
                slidetimer += Time.deltaTime;
                if(GameManager.instance.playerEvents.speedActive)
                {
                    UiManager.instance.barImage.fillAmount += Time.deltaTime / 20*3;
                    UiManager.instance.ghostImage.rectTransform.Translate(72 * Time.deltaTime, 0, 0);
                }
                else
                {
                    UiManager.instance.barImage.fillAmount += Time.deltaTime / 20;
                    UiManager.instance.ghostImage.rectTransform.Translate(24 * Time.deltaTime, 0, 0);
                }
              
                if (!isFalling)
                {
                    if (!GameManager.instance.playerEvents.speedActive)
                    {
                        transform.Translate(0, forcePower * Time.deltaTime, 0); //Ascend
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



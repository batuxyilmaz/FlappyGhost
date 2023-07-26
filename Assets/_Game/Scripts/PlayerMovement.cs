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
    public GameObject heightObject;
    public Animator playerAnim;
    public TextMeshPro playerHeightText;

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

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        value = 50;
       
    }
    private void Update()
    {

        if(heightCount%50==0)
        {
            generateScript.GenerateBg();
     
        }
        if (isFalling)
        {
            transform.Translate(0, -forcePower / 2 * Time.deltaTime, 0); //Decent
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
       
        heightObject.transform.position = new Vector3(heightObject.transform.position.x, transform.position.y, heightObject.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            if(GameManager.instance.gamestate==GameManager.GameState.start)
            {
                Vector3 firstPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                xValOffset = (firstPos.x - .5f) * multiplier - transform.position.x;


                isFalling = false;
                tap = true;
                screenBorder.transform.parent = GameManager.instance.player.transform;
                screenBorder.transform.localPosition = new Vector3(screenBorder.transform.localPosition.x, -5.55f, screenBorder.transform.localPosition.z);

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

                isFalling = true;

                screenBorder.transform.parent = null;
                playerAnim.SetBool("Falling", true);
            }
            

        }
        if (Input.GetMouseButton(0))
        {
           
            if(GameManager.instance.gamestate==GameManager.GameState.start)
            {
                slidetimer += Time.deltaTime;

                if (!isFalling)
                {
                    transform.Translate(0, forcePower * Time.deltaTime, 0); //Ascend
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
      

        Vector3 currentPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, new Vector3((currentPos.x - 0.5f) * multiplier - xValOffset, transform.position.y, transform.position.z), value * Time.deltaTime);
        Vector3 pos = transform.position;
        offsetX = Mathf.Clamp(pos.x, ground.bounds.min.x + 0.5f, ground.bounds.max.x - 0.5f);
        pos.x = offsetX;
     
    }
   
}



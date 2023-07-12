using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using EKTemplate;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float xValOffset;
    public float multiplier;
    public float speedVal;
    private bool touched;
    public BoxCollider ground;
   
   
    private int value;
  
    public Rigidbody rb;
    public bool slideControl;
    public float delayTime;
    public int forcePower;
    public bool groundControl;
    private float slidetimer;
    public GenerateObjects generateScript;
    public ParticleSystem hitEffect;
    public GameObject screenBorder;
    public static PlayerMovement instance;
    public int touchCount;
    public TextMeshPro playerHeightText;
    private int heightCount;
    public GameObject heightObject;

    private float jumpTimer;

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
        if (!GameManager.instance.failed)
        {
            jumpTimer += Time.deltaTime;
        }
      
        if (jumpTimer >= 1.5f)
        {

            TapControl();
            jumpTimer = 0f;
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

        if (Input.GetMouseButtonDown(0)&&!GameManager.instance.failed)
        {
            Vector3 firstPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            xValOffset = (firstPos.x - .5f) * multiplier - transform.position.x;
            //TapControl();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touched = false;
            slideControl = false;
            slidetimer = 0f;

        }
        if (Input.GetMouseButton(0) && !GameManager.instance.failed)
        {
            slidetimer += Time.deltaTime;
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
    private float offsetX = 0f;
    private void SlideControl()
    {
        touched = true;

        Vector3 currentPos= Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, new Vector3((currentPos.x - .5f) * multiplier - xValOffset, transform.position.y, transform.position.z), value * Time.deltaTime);
        Vector3 pos = transform.position;
        offsetX = Mathf.Clamp(pos.x, ground.bounds.min.x + 0.5f, ground.bounds.max.x - 0.5f);
        pos.x = offsetX;
        

    }
    private void TapControl()
    {
        //if (!slideControl)
        //{
        //    transform.GetChild(0).DOScaleY(0.11f, 0.1f).SetEase(Ease.OutSine).OnComplete(() => transform.GetChild(0).DOScaleY(0.37f, 0.1f).SetEase(Ease.OutSine));
        //    touchCount++;
        //    StartCoroutine(ForceDelay());

        //}
        transform.GetChild(0).DOScaleY(0.11f, 0.1f).SetEase(Ease.OutSine).OnComplete(() => transform.GetChild(0).DOScaleY(0.37f, 0.1f).SetEase(Ease.OutSine));
        touchCount++;
        StartCoroutine(ForceDelay());

    }

    private IEnumerator ForceDelay()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector3(0, forcePower, 0);
       

    }
   

}



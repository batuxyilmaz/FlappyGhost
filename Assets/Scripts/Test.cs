using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Reflection;

public class Test : MonoBehaviour
{
    private Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;
    private BoxCollider2D _boxCollider2D;
    public LayerMask groundLayerMask;
    public float throwForce;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float t;
    [SerializeField] private float movementForce;
    [SerializeField] private float waitForJump;
    internal float maxYForce = 400;
    internal bool isInAir;
    internal bool isJumpMove = false;
    internal bool isSlide = false;
    internal bool isBurning = false;
    internal bool test = true;
    internal Transform platformTransform;

    internal JumpAnimScript _jumpAnimScript;

    private void Start()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        DOTween.Init();
    }
    
    void Update(){
        
        if (_rigidbody2D.velocity.y < 0)
        {
            _boxCollider2D.enabled = true;
            _animator.SetBool("Falling",true);
        }
        else if (_rigidbody2D.velocity.y == 0)
        {
            if (platformTransform != null && GroundCheck() != null)
            {
                if (platformTransform.name.Contains("Platform"))
                {
                    _animator.SetBool("Jump", false);
                    _animator.SetBool("Fall",true);
                }
            }
        }
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            if (platformTransform != null)
            {
                if (_touch.phase == TouchPhase.Began) {
                    touchTimeStart = Time.time;
                    startPos = _touch.position;
                    UIController _uiController = GameObject.FindWithTag("Controller").GetComponent<UIController>();
                    _uiController.CheckTouchIsOnButton();
                    if (GameObject.FindWithTag("TapToPlay") != null)
                        GameObject.FindWithTag("TapToPlay").GetComponent<TMP_Text>().text = "Swipe To Jump";
                }
                else if (_touch.phase == TouchPhase.Moved)
                {
                    isJumpMove = true;
                    isSlide = false;
                    if (!_animator.GetBool("Hang") || !_animator.GetBool("Climb"))
                    {
                        if (GroundCheck() == null)
                        {
                            _rigidbody2D.AddForce(new Vector2(_touch.deltaPosition.x * Time.deltaTime * movementForce, 0));
                        }
                        else
                        {
                            _rigidbody2D.AddForce(new Vector2(_touch.deltaPosition.x * Time.deltaTime * movementForce * 5, 0));
                        }
                    }
                }
                else if (_touch.phase == TouchPhase.Ended && isJumpMove && !isBurning && GroundCheck() != null) {
                    touchTimeFinish = Time.time;
                    timeInterval = touchTimeFinish - touchTimeStart;
                    endPos = _touch.position;
                    direction = startPos - endPos;
                    Debug.Log(direction.y);
                    if (direction.y > -50)
                    {
                    }
                    else
                    {
                        isInAir = true;
                        platformTransform = null;
                        GetComponent<NewPhysicStamina>().DecreaseStamina();
                        if (!GetComponent<NewPhysicStamina>().CheckIsOutOfStamina())
                        {
                            if (GameObject.FindWithTag("TapToPlay") != null)
                                GameObject.FindWithTag("TapToPlay").gameObject.SetActive(false);
                            _animator.SetBool("Jump", true);
                            if(!GameObject.Find("Canvas").transform.GetChild(1).gameObject.activeInHierarchy)
                                GameObject.FindGameObjectWithTag("Controller").GetComponent<AudioScript>().PlayJumpSound();
                            _animator.SetBool("Fall",false);
                            _animator.SetBool("Falling",false);
                            UIController _uiController = GameObject.FindWithTag("Controller").GetComponent<UIController>();
                            _uiController.MoveUpgradeButtons(-1000);
                            StartCoroutine(RBForce());
                        }
                        else
                        {
                            //Düşme Animasyonu + TryAgainPanel aç
                            GetComponent<NewPhysicStamina>().idleBaseSpeed = 1;
                            GetComponent<Animator>().SetTrigger("Lose");
                            GameObject.FindWithTag("Canvas").transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
            }
        }
        else
        {
            if (GroundCheck() != null)
            {
                if (_rigidbody2D.velocity.y == 0 && !_animator.GetBool("Jump") && !_animator.GetBool("Climb") && !_animator.GetBool("Hang"))
                    GetComponent<NewPhysicStamina>().Regeneration();
                else
                    GetComponent<NewPhysicStamina>().timer = 0;
            }
            else
            {
            }
        }
    }

    private IEnumerator RBForce()
    {
        yield return new WaitForSeconds(0.5f);
        isJumpMove = false;
        Vector2 forceVector = -direction / timeInterval * throwForce;
        forceVector = new Vector2(0, forceVector.y);
        if (forceVector.y > maxYForce)
            forceVector = new Vector2(0, maxYForce);
        else if (forceVector.y < 30)
            forceVector = new Vector2(0, 30);
        _rigidbody2D.AddForce(forceVector);
        _boxCollider2D.size = new Vector2(0.1f,_boxCollider2D.size.y);
        yield break;
    }
    
    internal IEnumerator RBForce(Vector2 force)
    {
        isJumpMove = false;
        Vector2 forceVector = force;
        _rigidbody2D.AddForce(forceVector);
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Havada İse Kontrolü
        if (GroundCheck() == null && isInAir)
        {
            if (other.gameObject.tag == "Platform")
            {
                if (_boxCollider2D.bounds.center.y - _boxCollider2D.size.y <= other.transform.GetComponent<BoxCollider2D>().bounds.center.y)
                {
                    platformTransform = other.transform;
                    //transform.position = new Vector3(transform.position.x, other.transform.position.y - 1.2f, -0.8f);
                    _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                    _animator.SetBool("Hang", true);
                    GetComponent<NewPhysicStamina>().DecreaseStamina();
                    if (GetComponent<NewPhysicStamina>().CheckIsOutOfStamina() || isBurning)
                        _animator.SetBool("HangDown", true);
                    else
                        _animator.SetBool("Climb", true);
                }
            }
        }
        else
        {
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("coin"))
        {
            MoneyController.IncreaseMoney();
            Destroy(other.gameObject);
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
            if (_boxCollider2D.bounds.center.y + _boxCollider2D.size.y / 2 >= other.transform.GetComponent<BoxCollider2D>().bounds.center.y)
                platformTransform = other.transform;
    }

    private void OnDrawGizmosSelected()
    {
        if (_boxCollider2D != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size);
        }
    }

    internal Transform GroundCheck(Transform platform = null)
    {
        if (platform != null)
            return platform;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center+new Vector3(0,-0.02f), _boxCollider2D.bounds.size + new Vector3(0,0.02f), 0f, Vector2.down, 0.015f, groundLayerMask);
        
        if(raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.name.Contains("Level End Platform"))
            {
                transform.GetChild(0).eulerAngles = new Vector3(transform.GetChild(0).rotation.x, 180);
                _animator.SetBool("Victory", true);
            }
            if (raycastHit2D.collider.gameObject.layer == Mathf.Log(groundLayerMask.value, 2))
            {
                _animator.SetBool("Grounded", true);
                return raycastHit2D.collider.transform;
            }
        }
        //_animator.SetBool("Grounded", false);
        return null;
    }

    internal void ResetPlayer()
    {
        SetFalseAllAnimBools();
        _animator.Play("Empty");
        transform.position = new Vector3(1, -1.7f, -0.8f);
    }

    internal void SetFalseAllAnimBools()
    {
        foreach(AnimatorControllerParameter parameter in _animator.parameters) {            
            _animator.SetBool(parameter.name, false);            
        }
    }
}

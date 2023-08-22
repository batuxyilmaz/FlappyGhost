using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed;
    public PowerUps powerUpState;
    private int id;
    public ParticleSystem effect;
    private int increaseValue;
    void Start()
    {
        speed = GameManager.instance.speedObject;
        increaseValue = 60;
        switch (powerUpState)
        {
            case PowerUps.magnet:
                id = 0;
              
                break;
            case PowerUps.speed:
                id = 1;
             
                break;

        }
    }
    void Update()
    {
        if (GameManager.instance.gamestate == GameManager.GameState.start)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.instance.playerEvents.speedActive)
        {
            switch (id)
            {
                case 0:
                    effect.Play();
                    effect.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<PlayerEvents>().magnetActive = true;
                    other.gameObject.GetComponent<PlayerEvents>().magnetTimer = 0f;
                    other.gameObject.GetComponent<PlayerEvents>().magnetCol.SetActive(true);
                    GameManager.instance.magnetEffect.Play();

                    break;
                case 1:
                    //effect.Play();
                    //effect.gameObject.transform.parent = null;
                    //other.gameObject.GetComponent<PlayerEvents>().speedActive = true;
                    //other.gameObject.GetComponent<PlayerEvents>().speedTimer = 0f;
                    //PlayerMovement.instance.oldSpeed = PlayerMovement.instance.currentSpeed;
                    //GameManager.instance.playerEvents.immunity = true;
                    //PlayerMovement.instance.currentSpeed = increaseValue;
                    //PlayerMovement.instance.newSpeed = PlayerMovement.instance.currentSpeed;
                    //if (PlayerMovement.instance.currentSpeed >= 20)
                    //{
                    //    increaseValue = 60;
                    //}   
                    break;
            }
           
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Platform"))
        {

            transform.position = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);

        }
    }

    public enum PowerUps
    {
        magnet,
        speed
    }
}

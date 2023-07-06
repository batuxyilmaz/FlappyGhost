using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerOutlineController : MonoBehaviour
{
    [SerializeField] public Material PlayerMaterial = null;
    private float duration;
    private float endValue;
    public float constantValue = 1; // bu değer / animation idleSpeedMultiplier = outline duration
    private Animator _animator;
    private NewPhysicStamina _newPhysicStamina;
    private AudioScript _audioScript;

    private void Start()
    {
        DOTween.Init();
        _animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        _newPhysicStamina = _animator.gameObject.GetComponent<NewPhysicStamina>();
        _audioScript = GameObject.FindWithTag("Controller").GetComponent<AudioScript>();
    }

    private void Update()
    {
        int clipIndex;
        if (_newPhysicStamina.currentStamina > 2 && _newPhysicStamina.currentStamina <= 5) //Bunlar değişkene verilecek
        {
            //0 => idle, 1 => slow, 2 => medium, 3 => fast
            clipIndex = 2;
            //Medium Hearth Beat
            if (Mathf.Abs(endValue - PlayerMaterial.GetFloat("_OtlWidth")) < 0.01f )
            {
                endValue = endValue == 0 ? 2 : 0;
            }
        }
        else if (_newPhysicStamina.currentStamina >= 0 && _newPhysicStamina.currentStamina <= 2)
        {
            clipIndex = 3;
            //Fast Hearth Beat
            if (Mathf.Abs(endValue - PlayerMaterial.GetFloat("_OtlWidth")) < 0.01f )
            {
                endValue = endValue == 2 ? 4 : 2;
            }
        }
        else if(_newPhysicStamina.currentStamina > 5 && _newPhysicStamina.currentStamina <= 9)
        {
            clipIndex = 1;
            //Slow Speed Hearth Beat
            endValue = 0;
        }
        else
        {
            clipIndex = 0;
            //Idle Hearth Beat
            endValue = 0;
        }
        
        if(!GameObject.Find("Canvas").transform.GetChild(1).gameObject.activeInHierarchy)
            _audioScript.PlayHeartBeatAudio(clipIndex);
        PlayerMaterial.DOFloat(endValue, "_OtlWidth", constantValue / _animator.GetFloat("idleSpeedMultiplier"));
    }
}

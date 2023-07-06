using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPhysicStamina : MonoBehaviour
{
    public int maxStamina;
    internal float currentStamina;
    internal float idleBaseSpeed;
    internal float idleBaseSpeedIncAmount;
    public float idleBaseSpeedMaxLimit;
    public float staminaRegenTime;
    public float staminaRegenTimeMultiplier;
    public int regenStaminaAmount;
    internal float firstStaminaRegenTime = 2;
    internal float timer;
    internal bool isIncTimer;
    public float decreaseAmount;
    public Animator _animator;

    private void Start()
    {
        ResetStamina();
        firstStaminaRegenTime = staminaRegenTime;
        idleBaseSpeedIncAmount = (idleBaseSpeedMaxLimit-1) / maxStamina;
    }

    private void LateUpdate()
    {
        if (currentStamina >= maxStamina)
            currentStamina = maxStamina;
        if (idleBaseSpeed >= idleBaseSpeedMaxLimit)
            idleBaseSpeed = idleBaseSpeedMaxLimit;
        if (idleBaseSpeed <= 1)
        {
            idleBaseSpeed = 1;
            _animator.SetFloat("idleSpeedMultiplier",idleBaseSpeed);
        }

        CheckIsOutOfStamina();
    }

    internal void ResetStamina()
    {
        currentStamina = maxStamina;
        staminaRegenTime = firstStaminaRegenTime;
        idleBaseSpeed = 1;
        _animator.SetFloat("idleSpeedMultiplier", 1);
    }

    internal void DecreaseStamina()
    {
        if (!CheckIsOutOfStamina())
        {
            isIncTimer = false;
            currentStamina -= decreaseAmount;
            idleBaseSpeed += idleBaseSpeedIncAmount;
            _animator.SetFloat("idleSpeedMultiplier", idleBaseSpeed);
        }
        else
        {
        }
    }

    internal bool CheckIsOutOfStamina()
    {
        if (currentStamina < 0)
            return true;
        return false;
    }

    internal void Regeneration()
    {
        if (currentStamina < maxStamina && !isIncTimer)
        {
            staminaRegenTime *= staminaRegenTimeMultiplier;
            isIncTimer = true;
        }
        timer += Time.deltaTime;
        float seconds = Mathf.FloorToInt(timer % 60);
        
        if (seconds >= staminaRegenTime && currentStamina < maxStamina && !GameObject.Find("Player").GetComponentInChildren<Animator>().GetBool("Victory"))
        {
            currentStamina += regenStaminaAmount;
            staminaRegenTime /= staminaRegenTimeMultiplier;
            idleBaseSpeed -= idleBaseSpeedIncAmount;
            _animator.SetFloat("idleSpeedMultiplier",idleBaseSpeed);
            timer = 0;
        }
    }
}

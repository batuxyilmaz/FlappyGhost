using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    #region Static Variables
        public static float staticMaxStamina;
        public static float staticCurrentStamina;
        public static int staticDecreaseStaminaAmount;
        private static int staticRegenStaminaAmount;
        public static float staticStaminaRegenTime;
        public static float staticFirstStaminaRegenTime;
        private static float staticStaminaRegenTimeMultiplier;
        private static float staticIdleSpeedMaxLimit;
        private static float staticIdleIncAmount;
        public static float baseSpeed;
        private static bool isIncTimer;
        private static bool isDecMultiplier;
        public static float timer;
        private static Animator _animator;
        public static UpgradeController _UpgradeController;
        static public PlayerStamina instance; //the instance of our class that will do the work
    #endregion

    #region Private Variables
        [SerializeField] private int maxStamina;
        [SerializeField] private int decreaseStaminaAmount;
        [SerializeField] private int regenStaminaAmount;
        [SerializeField] private float staminaRegenTime;
        [SerializeField] private float staminaRegenTimeMultiplier;
        [SerializeField] private float idleSpeedMaxLimit;
    #endregion

    private void Awake()
    {
        staticDecreaseStaminaAmount = decreaseStaminaAmount;
        staticMaxStamina = maxStamina;
        staticCurrentStamina = staticMaxStamina;
        staticRegenStaminaAmount = regenStaminaAmount;
        staticStaminaRegenTime = staminaRegenTime;
        staticFirstStaminaRegenTime = staminaRegenTime;
        staticStaminaRegenTimeMultiplier = staminaRegenTimeMultiplier;
        staticIdleSpeedMaxLimit = idleSpeedMaxLimit;
        staticIdleIncAmount = (staticIdleSpeedMaxLimit-1) / staticMaxStamina;
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        //GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().maxValue = staticMaxStamina;
        //GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().value = staticCurrentStamina;
        instance = this;
        _UpgradeController = GameObject.Find("Controller").GetComponent<UpgradeController>();
    }

    private void Update()
    {
        if (staticCurrentStamina >= staticMaxStamina)
            staticCurrentStamina = staticMaxStamina;
        if (staticFirstStaminaRegenTime >= staticStaminaRegenTime)
            staticStaminaRegenTime = staticFirstStaminaRegenTime;
        if (baseSpeed >= staticIdleSpeedMaxLimit)
            baseSpeed = staticIdleSpeedMaxLimit;
        if (baseSpeed <= 1)
        {
            baseSpeed = 1;
            _animator.SetFloat("idleSpeedMultiplier",baseSpeed);
        }
    }

    public static void ResetStamina()
    {
        staticCurrentStamina = staticMaxStamina;
        
        GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().value = staticCurrentStamina;
    }

    public static void Stamina()
    {
        
        staticCurrentStamina -= staticDecreaseStaminaAmount;
        isIncTimer = false;
        baseSpeed += staticIdleIncAmount;
        _animator.SetFloat("idleSpeedMultiplier", baseSpeed);
        Debug.Log(staticCurrentStamina);
        
        //@Todo: Smoothlaştırılabilir
        //GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().value = staticCurrentStamina;
    }

    public static void Regeneration()
    {
        if (staticCurrentStamina < staticMaxStamina && !isIncTimer)
        {
            staticStaminaRegenTime *= staticStaminaRegenTimeMultiplier;
            isIncTimer = true;
        }
        //Debug.Log(staticStaminaRegenTime);
        timer += Time.deltaTime;
        float seconds = Mathf.FloorToInt(timer % 60);
        if (seconds >= staticStaminaRegenTime && staticCurrentStamina < staticMaxStamina && !GameObject.Find("Player").GetComponentInChildren<Animator>().GetBool("Victory"))
        {
            staticCurrentStamina += staticRegenStaminaAmount;
            staticStaminaRegenTime /= staticStaminaRegenTimeMultiplier;
            baseSpeed -= staticIdleIncAmount;
            _animator.SetFloat("idleSpeedMultiplier",baseSpeed);
            
            timer = 0;
            GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().value = staticCurrentStamina;
        }
    }
}

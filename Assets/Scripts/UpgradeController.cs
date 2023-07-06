using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private float staminaPrice;
    [SerializeField] private float staminaPriceMultiplier;
    [SerializeField] internal float staminaMultiplier;
    internal float staminaMultiplierCounter = 1;
    [SerializeField] private float incomePrice;
    [SerializeField] private float incomePriceMultiplier;
    [SerializeField] internal float incomeMultiplier;
    internal float incomeMultiplierCounter = 1;
    [SerializeField] internal float jumpForcePrice;
    [SerializeField] private float jumpForcePriceMultiplier;
    internal float jumpForceMultiplierCounter = 1;
    private Button _staminaBtn, _incomeBtn, _jumpForceBtn;

    private void Awake()
    {
        UIController _uiController = GameObject.FindWithTag("Controller").GetComponent<UIController>();
        _staminaBtn = _uiController.StaminaBtn.GetComponent<Button>();
        _incomeBtn = _uiController.IncomeBtn.GetComponent<Button>();
        _jumpForceBtn = _uiController.JumpForceBtn.GetComponent<Button>();
    }

    private void LateUpdate()
    {
        if (MoneyController.staticTotalMoney >= staminaPrice)
            _staminaBtn.interactable = true;
        else
            _staminaBtn.interactable = false;
        
        if (MoneyController.staticTotalMoney >= incomePrice)
            _incomeBtn.interactable = true;
        else
            _incomeBtn.interactable = false;
        
        if (MoneyController.staticTotalMoney >= jumpForcePrice)
            _jumpForceBtn.interactable = true;
        else
            _jumpForceBtn.interactable = false;

        if (GameObject.Find("IncomePrice") != null || GameObject.Find("StaminaPrice") != null)
        {
            GameObject.Find("IncomePrice").GetComponent<TMP_Text>().text = incomePrice.ToString("0.0");
            GameObject.Find("IncomeLevel").GetComponent<TMP_Text>().text = "Level " + incomeMultiplierCounter;
            GameObject.Find("StaminaPrice").GetComponent<TMP_Text>().text = staminaPrice.ToString("0.0");
            GameObject.Find("StaminaLevel").GetComponent<TMP_Text>().text = "Level " + staminaMultiplierCounter;
            GameObject.Find("JumpForcePrice").GetComponent<TMP_Text>().text = jumpForcePrice.ToString("0.0");
            GameObject.Find("JumpForceLevel").GetComponent<TMP_Text>().text = "Level " + jumpForceMultiplierCounter;
        }
    }

    public void NewStaminaPrice()
    {
        float money = MoneyController.staticTotalMoney;
        if (money - staminaPrice > 0)
        {
            GameObject.Find("StaminaPrice").GetComponent<TMP_Text>().text = staminaPrice.ToString("0.0");
            MoneyController.DecreaseMoney(staminaPrice);
            staminaPrice *= staminaPriceMultiplier;
            staminaMultiplierCounter++;
            PlayerStamina.staticMaxStamina *= staminaMultiplier;
            PlayerStamina.staticCurrentStamina = Mathf.FloorToInt(PlayerStamina.staticMaxStamina);
            
            Debug.Log(PlayerStamina.staticMaxStamina);
            Debug.Log(PlayerStamina.staticCurrentStamina);
            PlayerStamina.baseSpeed = 1;
            GameObject.Find("Player").GetComponentInChildren<Animator>().SetFloat("idleSpeedMultiplier", 1);
            GameObject.FindWithTag("StaminaBar").GetComponent<Slider>().maxValue = PlayerStamina.staticMaxStamina;
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    public void NewMaxForce()
    {
        float money = MoneyController.staticTotalMoney;
        if (money - jumpForcePrice > 0)
        {
            GameObject.Find("JumpForcePrice").GetComponent<TMP_Text>().text = jumpForcePrice.ToString("0.0");
            MoneyController.DecreaseMoney(jumpForcePrice);
            jumpForcePrice *= jumpForcePriceMultiplier;
            jumpForceMultiplierCounter++;
            GameObject.FindObjectOfType<Test>().maxYForce += 25;
            if (GameObject.FindObjectOfType<Test>().maxYForce >= 700)
                GameObject.FindObjectOfType<Test>().maxYForce = 700;
            Debug.Log(GameObject.FindObjectOfType<Test>().maxYForce);
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    public void NewIncomePrice()
    {
        float money = MoneyController.staticTotalMoney;
        if (money - incomePrice > 0)
        {
            MoneyController.DecreaseMoney(incomePrice);
            incomePrice *= incomePriceMultiplier;
            incomeMultiplierCounter++;
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }
}

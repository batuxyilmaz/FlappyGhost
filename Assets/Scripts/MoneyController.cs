using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    
    #region Static Variables
        public static float staticTotalMoney = 100;
        private static float staticIncreaseMoneyAmount;
        private static float waitForSeconds;
        public static UpgradeController _UpgradeController;
        static float incomeMultiplier = 1;
    #endregion

    #region Private Variables
    [SerializeField] private float totalMoney;
    [SerializeField] private float increaseMoneyAmount;
    [SerializeField] private float waitForSec; //Text Delete Timer
    [SerializeField] private float textSize;
    #endregion
    
    static public MoneyController instance; //the instance of our class that will do the work
    void Awake(){
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        staticTotalMoney = totalMoney;
        staticIncreaseMoneyAmount = increaseMoneyAmount;
        waitForSeconds = waitForSec;
        GameObject.Find("MoneyArea").GetComponent<TMP_Text>().fontSize = textSize; //
        _UpgradeController = GameObject.Find("Controller").GetComponent<UpgradeController>();
    }

    private void DeleteMoneyText()
    {
        GameObject.Find("MoneyArea").GetComponent<TMP_Text>().text = "";
    }
    
    public static void IncreaseMoney()
    {
        if (_UpgradeController.incomeMultiplierCounter != 1)
        {
            incomeMultiplier = Mathf.Pow(_UpgradeController.incomeMultiplier, _UpgradeController.incomeMultiplierCounter-1);
            Debug.Log(incomeMultiplier);
        }
        staticTotalMoney += staticIncreaseMoneyAmount * incomeMultiplier;
        SetMoneyToText();
    }

    public static void DecreaseMoney(float Amount)
    {
        staticTotalMoney -= Amount;
        SetMoneyToText();
    }

    private static void SetMoneyToText()
    {
        GameObject.Find("MoneyArea").GetComponent<TMP_Text>().text = "+ " + (staticIncreaseMoneyAmount * incomeMultiplier).ToString("0.0") + " $";
        GameObject.Find("MoneyText").GetComponentInChildren<TMP_Text>().text = staticTotalMoney.ToString("0.0");
        instance.Invoke("DeleteMoneyText", waitForSeconds);
    }
}

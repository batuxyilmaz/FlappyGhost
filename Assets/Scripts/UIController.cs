using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] internal GameObject TryAgainPanel;
    [SerializeField] internal GameObject NextLevelPanel;
    [SerializeField] private float duration;
    internal Transform UpgradePanelTransform;
    
    private Button TryAgainBtn;
    private Button NextLevelBtn;
    public Button StaminaBtn;
    public Button IncomeBtn;
    public Button JumpForceBtn;
    
    private LevelGenerator _levelGenerator;
    private UpgradeController _upgradeController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        UpgradePanelTransform = GameObject.Find("Upgrades").transform;
        
        TryAgainPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate {ResetPlayer(false); });
        NextLevelPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate {ResetPlayer(true); });
        StaminaBtn.onClick.AddListener(delegate {UpgradeStamina(); });
        IncomeBtn.onClick.AddListener(delegate {UpgradeIncome(); });
        JumpForceBtn.onClick.AddListener(delegate {UpgradeJumpForce(); });
        
        GameObject _controller = GameObject.Find("Controller");
        _levelGenerator = _controller.GetComponent<LevelGenerator>();
        _upgradeController = _controller.GetComponent<UpgradeController>();
        MoveUpgradeButtons(0);
    }

    private void LateUpdate()
    {
        if (TryAgainPanel.activeInHierarchy)
        {
            _animator.SetBool("Jump", false);
            GameObject.FindWithTag("Controller").GetComponent<AudioScript>().StopAudio();
        }
        
        if(_animator.GetBool("Victory"))
            NextLevelPanel.SetActive(true);
    }

    private void UpgradeStamina()
    {
        _upgradeController.NewStaminaPrice();
    }
    
    private void UpgradeIncome()
    {
        _upgradeController.NewIncomePrice();
    }
    
    private void UpgradeJumpForce()
    {
        _upgradeController.NewMaxForce();
    }

    private void ResetPlayer(bool IncLevel = false)
    {
        UpgradePanelTransform.GetChild(0).gameObject.SetActive(true);
        _animator.GetComponentInParent<NewPhysicStamina>().ResetStamina();
        _animator.GetComponentInParent<Test>().ResetPlayer();
        Invoke("ClosePanels",0.1f);
        if (IncLevel)
        {
            //jumpForcePrice jumpForceMultiplierCounter maxYForce
            _upgradeController.jumpForceMultiplierCounter = 2;
            _upgradeController.jumpForcePrice = 1;
            GameObject.FindObjectOfType<Test>().maxYForce = 400;
            LevelGenerator.level++;
            _levelGenerator.GenerateLevel();
        }
    }

    private void ClosePanels()
    {
        MoveUpgradeButtons(0);
        _levelGenerator.GenerateLevel();
        TryAgainPanel.SetActive(false);
        NextLevelPanel.SetActive(false);
    }

    internal void MoveUpgradeButtons(float yAxis)
    {
        UpgradePanelTransform.GetChild(0).DOMoveY(yAxis, duration, true);
        GameObject.Find("MarketIcon").transform.GetComponent<RectTransform>().DOAnchorPosX(yAxis == 0 ? -370 : -600, duration, true);
        GameObject.Find("MoneyIcon").transform.GetComponent<RectTransform>().DOAnchorPosX(yAxis == 0 ? 355 : 590, duration, true);
    }
    
    internal bool CheckTouchIsOnButton()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            //Debug.Log("asdasd");
            return true;
        }
        else
        {
            if(GameObject.Find("Buttons") != null)
                MoveUpgradeButtons(-1000);
            return false;
        }
    }
}
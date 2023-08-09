using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class UiManager : MonoBehaviour
{   
    
    public static UiManager instance;

    public List<GameObject> panels;
    public List<Button> buttons;
    public List<GameObject> clickedObjects;
    public List<TextMeshProUGUI> leadTexts;
    private int leadCount;

    public Image barImage;
    public Image ghostImage;
    public Image speedbarImage;
    public GameObject speedImage;
    public GameObject clickedObject;
    public GameObject startPanel;
    public GameObject upgradePanel;
    public GameObject ingamePanel;
    public GameObject leadPanel;
    public GameObject namePanel;
    public int buyValue;
    public TextMeshProUGUI buyText;
    public GameObject fadeText;

    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject hapticOn;
    public GameObject hapticOff;
    private bool optionActive;
    public bool soundActive;
    public bool hapticActive;
    public Button upgradeButton;
    public GameObject tut;
    public List<GameObject> tuts;
    public int tutId;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        tutId = PlayerPrefs.GetInt("TutIdUp");
        if (tutId >= 1)
        {
            tut.SetActive(false);
        }
        else
        {
            if (GameManager.instance.firstPlayed)
            {
                tut.SetActive(true);
            }
        }
      
    }
    public void StartGame()
    {
        if (!tut.activeSelf)
        {
            DOTween.Restart("Click");
            GameManager.instance.startSound.Play();
            StartCoroutine(StartButton());
        }
     
      
    }
    public void NextEnd()
    {
        DOTween.Restart("ClickNext");
        GameManager.instance.startSound.Play();
        StartCoroutine(NextButton());
    }
    public void SelectCharacter()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(655f, 0.2f);

    }
    public void SelectPlatform()
    {
        panels[1].SetActive(true);
        panels[2].SetActive(false);
        panels[0].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
    }
    public void SelectBg()
    {
        panels[2].SetActive(true);
        panels[1].SetActive(false);
        panels[0].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
    }
    public void SelectModel()
    {
        
        var button = EventSystem.current.currentSelectedGameObject;
        clickedObject = button.GetComponent<SelectModel>().modelAssets.modelObject;
        buyValue= button.GetComponent<SelectModel>().modelAssets.value;
        buyText.text=buyValue.ToString();
        GameObject currentObject= Instantiate(clickedObject);
       
        clickedObjects.Add(currentObject);
        
        if(clickedObjects.Count >= 2)
        {
            clickedObjects[0].SetActive(false);
            clickedObjects.RemoveAt(0);
        }
    }
    public void Buy()
    {
        if (GameManager.instance.point >= buyValue)
        {
          
        }
    }
    IEnumerator TutorialOpen()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.tutorial.SetActive(true);
    }
    IEnumerator StartButton()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.gamestate = GameManager.GameState.start;
        startPanel.SetActive(false);
        ingamePanel.SetActive(true);
        if (GameManager.instance.tutId <= 0)
        {
            StartCoroutine(TutorialOpen());
        }
    }
    IEnumerator NextButton()
    {
        yield return new WaitForSeconds(0.2f);
        leadPanel.SetActive(false);
        GameManager.instance.endgamePanel.SetActive(true);
    }
    public void OpenOptions()
    {
       
        if (!optionActive)
        {
          
            DOTween.Restart("OpenOption");
            upgradeButton.gameObject.SetActive(false);
            optionActive = true;
            return;
        }
        else
        {
          
            DOTween.Restart("CloseOption");
            Invoke("UpgradeButton", 0.4f);
            optionActive = false;
        }
            
    }
    public void Sound()
    {
        if(!soundActive)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            soundActive = true;
            GameManager.instance.Mute();
            return;
        }
        else
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            GameManager.instance.Unmute();
            soundActive = false;
        }
    }
    public void Haptic()
    {
        if (!hapticActive)
        {
            hapticOff.SetActive(false);
            hapticOn.SetActive(true);
            hapticActive = true;
            return;
        }
        else
        {
            hapticOff.SetActive(true);
            hapticOn.SetActive(false);
            hapticActive = false;
        }
    }
    private void UpgradeButton()
    {
        upgradeButton.gameObject.SetActive(true);
    
    }
   

}

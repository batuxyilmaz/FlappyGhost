using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gamestate;
  
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI endcoinText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI mainHighScoreText;
    public TextMeshProUGUI speedText;

    public GameObject ingamePanel;
    public GameObject endgamePanel;
    public GameObject player;
    public GameObject blurPanel;
    public GameObject tutorial;
    public List<GameObject> closedThings;
    public GameObject openObject;

    public int tutId;
    public int highScore;
    public int point;
    public float speedObject;
    public int mainHighscore;

    private bool tutPassed;
    public PlayerEvents playerEvents;
    public ParticleSystem magnetEffect;
    public AudioSource collectSound;
    public AudioSource boostSound;
    public AudioSource startSound;
    public AudioSource hitSound;
    public AudioSource failSound;
    public AudioSource speedUiSound;
    public AudioSource flySound;

    public InputField input;
    [SerializeField] private int leadTextCount;
    private void Awake()
    {
        instance = this;
        playerEvents = GameObject.FindWithTag("Player").GetComponent<PlayerEvents>();
        point = PlayerPrefs.GetInt("Point");
        coinText.text = point.ToString();
    }
    private void Start()
    {
       
        tutId = PlayerPrefs.GetInt("TutId");
        if (tutId >= 1)
        {
            tutPassed = true;
        }
        if(tutPassed)
        {
            tutorial.SetActive(false);
        }
    }
  


    public enum GameState
    {
        start,
        pause
    }
    public void End()
    {
      
        mainHighscore=PlayerPrefs.GetInt("MainHighScore");
        blurPanel.SetActive(true);
        highScore = Mathf.RoundToInt(player.transform.position.y);
        if (highScore > mainHighscore)
        {
            mainHighscore = highScore;
            PlayerPrefs.SetInt("MainHighScore", mainHighscore);
            mainHighScoreText.color = Color.green;
        }
        mainHighScoreText.text = mainHighscore.ToString();
        if (highScore <= 0)
        {
            highScore = 0;
            highScoreText.text = highScore.ToString();
        }
        else
        {
            highScoreText.text = highScore.ToString();
        }
       
        endcoinText.text=point.ToString();
      
    }
    public void OpenEndGame()
    {
        ingamePanel.SetActive(false);
        if (leadTextCount >= 10)
        {
            input.gameObject.SetActive(false);
        }
       
        for (int i = 0; i < UiManager.instance.leadTexts.Count; i++)
        {
            if (!UiManager.instance.leadTexts[i].gameObject.activeSelf)
            {
                UiManager.instance.leadTexts[i].gameObject.SetActive(true);
                UiManager.instance.leadTexts[i].text = input.text + " " + highScore;
                
                UiManager.instance.leadTexts[i].GetComponent<LeadControl>().filled = true;
                UiManager.instance.leadTexts[i].GetComponent<LeadControl>().scoreCount = highScore;
                leadTextCount++;
                break;
            }
        }
       


        UiManager.instance.leadPanel.SetActive(true);
       
        TouchScreenKeyboard.Open(UiManager.instance.leadTexts[0].text);
        failSound.Play();
    }
    public void Mute()
    {
        flySound.mute = true;
        collectSound.mute = true;
        hitSound.mute = true;
        boostSound.mute = true;
        startSound.mute = true;
        failSound.mute = true;
        speedUiSound.mute = true;
    }
    public void Unmute()
    {
        flySound.mute = false;
        collectSound.mute = false;
        hitSound.mute = false;
        boostSound.mute = false;
        startSound.mute = false;
        failSound.mute = false;
        speedUiSound.mute = false;
    }
   public void AddData()
    {
        
        OpenEndGame();
        UiManager.instance.namePanel.SetActive(false);
       

    }
    private void Sort()
    {
        for (int i = 0; i < UiManager.instance.leadTexts.Count; i++)
        {
            if (UiManager.instance.leadTexts[i].gameObject.activeSelf)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    if (UiManager.instance.leadTexts[j].gameObject.GetComponent<LeadControl>().scoreCount > UiManager.instance.leadTexts[i].gameObject.GetComponent<LeadControl>().scoreCount)
                    {
                        string temptex = UiManager.instance.leadTexts[i].text;

                        UiManager.instance.leadTexts[i].text = UiManager.instance.leadTexts[j].text;
                        UiManager.instance.leadTexts[j].text = temptex;
                    }

                }


            }


        }
    }
}

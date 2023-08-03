using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gamestate;
  
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI endcoinText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI mainHighScoreText;
    

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
        endgamePanel.SetActive(true);
        failSound.Play();
    }
 
   
}

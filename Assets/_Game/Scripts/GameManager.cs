using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public int highScore;
    public int point;
    public float speedObject;
    public int mainHighscore;
    public List<GameObject> closedThings;
    public GameObject openObject;
    public PlayerEvents playerEvents;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerEvents=GameObject.FindWithTag("Player").GetComponent<PlayerEvents>();
    }

    public enum GameState
    {
        start,
        pause
    }
    public void End()
    {
        //highscore

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
    }
   
}

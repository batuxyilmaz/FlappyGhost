using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gamestate;
    public int point;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI endcoinText;
    public GameObject ingamePanel;
    public GameObject endgamePanel;
    public CinemachineVirtualCamera playerCam;
    public int highScore;
    public TextMeshProUGUI highScoreText;
    public GameObject player;
    public bool failed;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
   public enum GameState
    {
        start,
        pause
    }
    public void End()
    {
        //highscore

        highScore = Mathf.RoundToInt(player.transform.position.y);
        if (highScore <= 0)
        {
            highScore = 0;
            highScoreText.text = highScore.ToString();
        }
        else
        {
            highScoreText.text = highScore.ToString();
        }
       
        endcoinText.text =point.ToString();
        ingamePanel.SetActive(false);
        endgamePanel.SetActive(true);
    }
}

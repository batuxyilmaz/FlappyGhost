using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
     public void RestartGame()
     {
        DOTween.Restart("Click");
        SceneManager.LoadScene(1);
     }
}

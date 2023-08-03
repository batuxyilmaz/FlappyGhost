using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
     public void RestartGame()
     {
       
        GameManager.instance.startSound.Play();
        DOTween.Restart("ClickRestart");
        StartCoroutine(RestartButton());

    }
    IEnumerator RestartButton()
    {
        yield return new WaitForSeconds(0.2f);
       
        DOTween.Clear();
        SceneManager.LoadScene(1);
    }
}

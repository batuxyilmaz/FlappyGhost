using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.LoadSceneAsync(1);
    }

  
}

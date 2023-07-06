using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float count;

    private void Start()
    {
        Application.targetFrameRate = 300;
    }

    private void Update()
    {
        count = 1f / Time.deltaTime;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 150, 200, 100), "FPS: " + Mathf.Round(count));
    }
}

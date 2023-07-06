using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float tmr;
    private LevelGenerator _levelGenerator;
    public Slider _progressBar;

    private bool isTemp = false;
    private float _sliderValue;
    
    private void Start()
    {
        _levelGenerator = GameObject.FindWithTag("Controller").GetComponent<LevelGenerator>();
    }

    private void LateUpdate()
    {
        if(_levelGenerator.emptyGO != null)
        {
            _progressBar.maxValue = _levelGenerator.emptyGO.transform.position.y;
            if (GameObject.FindWithTag("Player").GetComponentInParent<Animator>().GetBool("Climb"))
            {
                tmr += Time.deltaTime / 4;
                if (!isTemp)
                {
                    isTemp = true;
                    _sliderValue = _progressBar.value;
                }
                _progressBar.value = Mathf.Lerp(_sliderValue, _sliderValue + 1.6f, tmr);
            }
            else
            {
                _progressBar.value = GameObject.Find("Player").transform.position.y;
                tmr = 0;
                isTemp = false;
            }
        }
    }
}

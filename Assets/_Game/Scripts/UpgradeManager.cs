using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI magnetCointText;
    public TextMeshProUGUI speedCointText;
    public TextMeshProUGUI magnetLvlText;
    public TextMeshProUGUI speedLvlText;

    public int magnetCandyValue;
    public int magnetLvl;
    public int speedCandyValue;
    public int speedLvl;


    private void Start()
    {
        if (GameManager.instance.playerEvents.magnetUpgraded)
        {
            magnetLvl = PlayerPrefs.GetInt("MagnetLevel");
            magnetLvlText.text=magnetLvl.ToString();
            magnetCandyValue = PlayerPrefs.GetInt("MagnetCandy");
            magnetCointText.text=magnetCandyValue.ToString();
        }
        else
        {
            magnetLvl = 1;
            magnetLvlText.text = magnetLvl.ToString();
            magnetCandyValue = 100;
            magnetCointText.text = magnetCandyValue.ToString();
        }
        if (GameManager.instance.playerEvents.speedUpgraded)
        {
            speedLvl = PlayerPrefs.GetInt("MagnetLevel");
            speedLvlText.text = speedLvl.ToString();
            speedCandyValue = PlayerPrefs.GetInt("MagnetCandy");
            speedCointText.text = speedCandyValue.ToString();
        }
        else
        {
            speedCandyValue = 100;
            speedLvl = 1;
            speedLvlText.text = speedLvl.ToString();
            speedCointText.text = speedCandyValue.ToString();
        }

      
    }
    public void UpgradeMagnet()
    {
        if (GameManager.instance.point >=magnetCandyValue )
        {
            GameManager.instance.startSound.Play();
            GameManager.instance.point -= magnetCandyValue;
            GameManager.instance.coinText.text = GameManager.instance.point.ToString();
            PlayerPrefs.SetInt("CandyPoint", GameManager.instance.point);
            magnetCandyValue += 100;
            magnetCointText.text = magnetCandyValue.ToString();
            PlayerPrefs.SetInt("MagnetCandy", magnetCandyValue);
            magnetLvl++;
            magnetLvlText.text = magnetLvl.ToString();
            PlayerPrefs.SetInt("MagnetLevel", magnetLvl);
            GameManager.instance.playerEvents.magnetTime += 0.2f;
            PlayerPrefs.SetFloat("MagnetTime", GameManager.instance.playerEvents.magnetTime);
            GameManager.instance.playerEvents.magnetId = 1;
            PlayerPrefs.SetInt("MagnetId", GameManager.instance.playerEvents.magnetId);
           
        }
    }
    public void UpgradeSpeed()
    {
        if (GameManager.instance.point >=speedCandyValue )
        {
            GameManager.instance.startSound.Play();
            GameManager.instance.point -= speedCandyValue;
            GameManager.instance.coinText.text = GameManager.instance.point.ToString();
            speedCandyValue += 100;
            speedCointText.text = speedCandyValue.ToString();
            PlayerPrefs.SetInt("SpeedCandy", speedCandyValue);
            PlayerPrefs.SetInt("CandyPoint", GameManager.instance.point);
            speedLvl++;
            speedLvlText.text = speedLvl.ToString();
            PlayerPrefs.SetInt("SpeedLevel", speedLvl);
            GameManager.instance.playerEvents.speedTime += 0.2f;
            PlayerPrefs.SetFloat("SpeedTime", GameManager.instance.playerEvents.speedTime);
            GameManager.instance.playerEvents.speedId = 1;
            PlayerPrefs.SetInt("SpeedId", GameManager.instance.playerEvents.speedId);

        }
    }
    public void CloseUpgrade()
    {
        Invoke("OpenPanel",0.8f);
        DOTween.Restart("CloseUpgrade");
    }
    private void OpenPanel()
    {
        UiManager.instance.startPanel.SetActive(true);
        UiManager.instance.upgradePanel.SetActive(false);
    }

}

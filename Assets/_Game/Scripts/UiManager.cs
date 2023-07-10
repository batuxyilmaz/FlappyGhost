using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public List<GameObject> panels;
    public List<Button> buttons;
    
    public void SelectCharacter()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(655f, 0.2f);

    }
    public void SelectPlatform()
    {
        panels[1].SetActive(true);
        panels[2].SetActive(false);
        panels[0].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
    }
    public void SelectBg()
    {
        panels[2].SetActive(true);
        panels[1].SetActive(false);
        panels[0].SetActive(false);
        buttons[0].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[1].transform.parent.transform.DOLocalMoveY(655f, 0.2f);
        buttons[2].transform.parent.transform.DOLocalMoveY(692f, 0.2f);
    }
}

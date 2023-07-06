using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.failed = true;

            GameManager.instance.playerCam.Follow = null;
            GameManager.instance.playerCam.LookAt = null;
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            StartCoroutine(EndDelay());

        }
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.End();
    }
}

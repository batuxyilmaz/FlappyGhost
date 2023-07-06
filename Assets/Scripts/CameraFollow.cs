using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    private float tmr;
    private float lerpVal;
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(0, _player.transform.position.y + 2f, transform.position.z);
    }
}

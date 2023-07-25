using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamFollowplayer : MonoBehaviour
{
    public GameObject followObject;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!PlayerMovement.instance.isFalling)
        {
            if (!PlayerMovement.instance.stopped)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(startPos.x, followObject.transform.position.y + 2.7f, startPos.z), 5f * Time.deltaTime);
            }
           
        }
      
    }
}

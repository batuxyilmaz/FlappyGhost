using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollow : MonoBehaviour
{
  
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position;
    }
}

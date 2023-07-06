using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        speed = 0.2f;
    }

    
    void Update()
    {
         transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}

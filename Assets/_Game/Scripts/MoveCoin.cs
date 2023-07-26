using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        speed = GameManager.instance.speedObject;
    }

    
    void Update()
    {
        if(GameManager.instance.gamestate==GameManager.GameState.start)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
      
    }
}

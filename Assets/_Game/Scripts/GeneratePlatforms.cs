using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatforms : MonoBehaviour
{
    public Difficulty difState;

    public float ePosMin;
    public float ePosMax;
  
    public float mPosMin;
    public float mPosMax;
 
    public float hPosMin;
    public float hPosMax;


    void Start()
    {
        switch (difState)
        {
            case Difficulty.easy:
                float randomPos = Random.Range(ePosMin, ePosMax);
                transform.position = new Vector3(randomPos, transform.position.y, transform.position.z);
                break;
            case Difficulty.medium:
                float randomPosMid = Random.Range(mPosMin, mPosMax);
                transform.position = new Vector3(randomPosMid, transform.position.y, transform.position.z);
                break;
            case Difficulty.hard:
                float randomPosHard = Random.Range(hPosMin, hPosMax);
                transform.position = new Vector3(randomPosHard, transform.position.y, transform.position.z);
                break;
        }
    
    }

    public enum Difficulty
    {
        easy,
        medium,
        hard
    }
}

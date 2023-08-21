using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        ePosMin = transform.parent.GetComponent<PosHolder>().ePosMin;
        ePosMax = transform.parent.GetComponent<PosHolder>().ePosMax;
        mPosMin = transform.parent.GetComponent<PosHolder>().mPosMin;
        mPosMax = transform.parent.GetComponent<PosHolder>().mPosMax;
        hPosMin = transform.parent.GetComponent<PosHolder>().hPosMin;
        hPosMax = transform.parent.GetComponent<PosHolder>().hPosMax;
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

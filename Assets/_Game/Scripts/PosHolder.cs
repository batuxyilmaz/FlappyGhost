using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosHolder : MonoBehaviour
{
    public float ePosMin;
    public float ePosMax;

    public float mPosMin;
    public float mPosMax;

    public float hPosMin;
    public float hPosMax;

    public Difficulty difState;
    public enum Difficulty
    {
        easy,
        medium,
        hard
    }
}

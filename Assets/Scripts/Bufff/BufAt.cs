using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufAt : BuDebu
{
    public float dDuration;
    public float dValue;
    public BufAt(float duration, float value): base( duration,  value) 
    {
        Duration = duration;
        Value = value;
    }
}

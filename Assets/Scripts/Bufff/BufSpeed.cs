using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufSpeed : BuDebu
{
    public float dDuration;
    public float dValue;
    public BufSpeed(float duration, float value) : base(duration, value)
    {
        Duration = duration;
        Value = value;
    }
}

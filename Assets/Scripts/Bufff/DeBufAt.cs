using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBufAt : BuDebu
{
    public float dDuration;
    public float dValue;
    public DeBufAt(float duration, float value) : base(duration, value)
    {
        Duration = duration;
        Value = value;
    }
}

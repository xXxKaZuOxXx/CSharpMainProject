using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBufSp : BuDebu
{
    public float dDuration;
    public float dValue;
    public DeBufSp(float duration, float value) : base(duration, value)
    {
        Duration = duration;
        Value = value;
    }
}

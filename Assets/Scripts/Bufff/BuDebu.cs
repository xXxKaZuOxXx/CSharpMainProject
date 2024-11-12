using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuDebu
{
    public float Duration { get;  set; }
    public float Value { get; set; }
    public BuDebu(float duration, float value)
    {
        Duration = duration;
        Value = value;
    }
}

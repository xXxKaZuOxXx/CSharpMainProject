using Model;
using Model.Config;
using Model.Runtime;
using Model.Runtime.ReadOnly;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitBrains;
using UnityEngine;
using Utilities;


public class SystemForBuffs
{
  
   Dictionary<Unit, BuDebu> buf = new Dictionary<Unit, BuDebu> ();
   
    public float Push(Unit unit, BuDebu buDebu)
    {
        if (buf.ContainsKey(unit))
        {
            return 0;
        }
        else
        {
            buf.Add(unit, buDebu);
            
            float ZNACH = buDebu.Value;
            Pop(unit, buDebu);
            return ZNACH;
        }
        
    }
    public IEnumerator Pop(Unit unit, BuDebu buDebu)
    {
        yield return new WaitForSeconds(buDebu.Duration);
        buf.Remove(unit);

    }
    public float IfTrueUnit(Unit unit)
    {
        if(buf.ContainsKey(unit))
        {
            return buf[unit].Value;
        }
        else {return 0f; }
    }
    


}

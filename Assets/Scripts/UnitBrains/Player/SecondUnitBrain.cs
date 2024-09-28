using System.Collections.Generic;
using Model;
using Model.Runtime.Projectiles;
using UnityEngine;
using Utilities;

namespace UnitBrains.Player
{
    public class SecondUnitBrain : DefaultPlayerUnitBrain
    {
        public override string TargetUnitName => "Cobra Commando";
        private const float OverheatTemperature = 3f;
        private const float OverheatCooldown = 2f;
        private float _temperature = 0f;
        private float _cooldownTime = 0f;
        private bool _overheated;
       
        private static int Counter = 0;
        public int UnitNumber = Counter;
        private const  int MaxTargets = 3;
        private List<Vector2Int> FarTargets = new List<Vector2Int>();
        

        
        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
        {
            float overheatTemperature = OverheatTemperature;
            ///////////////////////////////////////
            // Homework 1.3 (1st block, 3rd module)
            if(GetTemperature() < overheatTemperature)
            {
                Debug.Log(_temperature);

                for (int i = 0; i < _temperature+1; i++ )
                {
                    var projectile = CreateProjectile(forTarget);
                    AddProjectileToList(projectile, intoList);
                }

                IncreaseTemperature();
            }   
        }

        public override Vector2Int GetNextStep()
        {



            //Vector2Int pos = unit.Pos;
            //Vector2Int nextpos = new Vector2Int();
            //Vector2Int target;

            //if (FarTargets.Count > 0)
            //{
            //    target = FarTargets[0];
            //}
            //else
            //{
            //    return unit.Pos;
            //}

            //if (IsTargetInRange(target))
            //{
            //    return unit.Pos;
            //}
            //else
            //{
            //    nextpos = target;
            //    return pos.CalcNextStepTowards(nextpos);
            //}
            return base.GetNextStep();

        }

        protected override List<Vector2Int> SelectTargets()
        {

            ///////////////////////////////////////
            // Homework 1.4 (1st block, 4rd module)
            ///////////////////////////////////////
            List<Vector2Int> result = new List<Vector2Int>();

            float minimum = float.MaxValue;
            Vector2Int pos = Vector2Int.zero;

            FarTargets.Clear();

            foreach (var target in GetAllTargets())
            {
                FarTargets.Add(target);
            }

            if (minimum < float.MaxValue)
            {
                if (IsTargetInRange(pos))
                {
                    //result.Add(pos);
                }
                else
                {
                    int palyerID = IsPlayerUnitBrain ? RuntimeModel.PlayerId : RuntimeModel.BotPlayerId;
                    Vector2Int enemyBase = runtimeModel.RoMap.Bases[palyerID];
                    FarTargets.Add(enemyBase);
                }
            }
            SortByDistanceToOwnBase(FarTargets);

            int EnID = UnitNumber % MaxTargets;
            int i = 0;
            if (EnID == i % MaxTargets)
            {
                pos = FarTargets[i];
            }
            if (IsTargetInRange(pos))
            {
                result.Add(pos);
            }
            i++;


            Counter++;




            //IEnumerable< Vector2Int > allTargets = GetAllTargets();
            //if(allTargets != null)
            //{
            //    foreach(Vector2Int target in GetAllTargets())
            //    {
            //        if (DistanceToOwnBase(target) < minimum)
            //        {   
            //            minimum = DistanceToOwnBase(target);
            //            pos = target;
            //        }

            //    }
            //    FarTargets.Clear();
            //    FarTargets.Add(pos);
            //    if (IsTargetInRange(pos))
            //    {
            //        result.Add(pos);
            //    }



            //}
            //if (minimum < float.MaxValue)
            //{
            //    if (IsTargetInRange(pos))
            //    {
            //        result.Add(pos);
            //    }
            //    else
            //    {
            //        int palyerID = IsPlayerUnitBrain ? RuntimeModel.PlayerId : RuntimeModel.BotPlayerId;
            //        Vector2Int enemyBase = runtimeModel.RoMap.Bases[palyerID];
            //        FarTargets.Add(enemyBase);
            //    }
            //}


            //if (result.Count > 0)
            //{
            //    result.Clear();
            //   result.Add(pos);
            //}

            //while (result.Count > 1)
            //{
            //    result.RemoveAt(result.Count - 1);
            //}
            return result;
            ///////////////////////////////////////
        }



        public override void Update(float deltaTime, float time)
        {
            if (_overheated)
            {              
                _cooldownTime += Time.deltaTime;
                float t = _cooldownTime / (OverheatCooldown/10);
                _temperature = Mathf.Lerp(OverheatTemperature, 0, t);
                if (t >= 1)
                {
                    _cooldownTime = 0;
                    _overheated = false;
                }
            }
        }

        private int GetTemperature()
        {
            if(_overheated) return (int) OverheatTemperature;
            else return (int)_temperature;
        }

        private void IncreaseTemperature()
        {
            _temperature += 1f;
            if (_temperature >= OverheatTemperature) _overheated = true;
        }
    }
}
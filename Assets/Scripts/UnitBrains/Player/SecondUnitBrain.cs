using System.Collections.Generic;
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
            
            Vector2Int position = unit.Pos;
            Vector2Int nextposition = unit.Pos;
            foreach ( var targets in FarTargets )
            {
                if(IsTargetInRange(targets))
                {
                    FarTargets.Clear();
                    
                    return position;
                }
                else
                {
                    
                    nextposition = targets;
                    
                }

            }
            
            return position.CalcNextStepTowards(nextposition);

        }

        protected override List<Vector2Int> SelectTargets()
        {
            ///////////////////////////////////////
            // Homework 1.4 (1st block, 4rd module)
            ///////////////////////////////////////
            List<Vector2Int> result = new List<Vector2Int>();

            float minimum = float.MaxValue;
            Vector2Int pos = Vector2Int.zero;

            IEnumerable< Vector2Int > allTargets = GetAllTargets();
            if(allTargets != null)
            {
                foreach(Vector2Int target in allTargets)
                {
                    if (DistanceToOwnBase(target) < minimum)
                {   
                    minimum = DistanceToOwnBase(target);
                    pos = target;
                }

                }
                if(IsTargetInRange(pos))
                {
                    result.Add(pos);
                   
                }
                else
                {
                    FarTargets.Add(pos);
                    
                }
            }
            else
            {
                if (IsTargetInRange(runtimeModel.RoMap.Bases[0]))
                {
                    result.Add(runtimeModel.RoMap.Bases[0]);
                    
                }
                else
                {
                    FarTargets.Add(runtimeModel.RoMap.Bases[0]);
                    
                }
            }
            
            
            
            //List<Vector2Int> result = GetReachableTargets();
            //float minimum = float.MaxValue;
            //Vector2Int pos = Vector2Int.zero;
            //foreach (var target in result)
            //{
            //    if(DistanceToOwnBase(target) < minimum)
            //    {   
            //        minimum = DistanceToOwnBase(target);
            //        pos = target;
            //    }
            //}

            //if(result.Count > 0)
            //{
            //    result.Clear();
            //    result.Add(pos);
            //}

            while (result.Count > 1)
            {
                result.RemoveAt(result.Count - 1);
            }
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
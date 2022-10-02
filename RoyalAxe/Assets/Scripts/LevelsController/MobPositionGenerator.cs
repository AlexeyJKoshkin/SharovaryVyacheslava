using System;
using System.Linq;
using GameKit;
using RoyalAxe.Map;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoyalAxe.CoreLevel
{
    public interface IMobPositionGenerator
    {
        (Vector2 startPoint, Vector2 endPoint) GetPosForNewMob(string modDataMobId);
        void Reset();
    }

    class MobPositionGenerator : IMobPositionGenerator
    {
        private readonly LineRoyalAxeMap[] _lineRoyalAxeMaps;
        private readonly EndPointsRoyalAxeMap[] _endPoints;
        private readonly float _yZero;

        private const float OFFSET_X = 0.1f;


        public MobPositionGenerator(ILineRoyalAxeMapBuilder currenLevelLineBuilder, ILevelAdapter levelAdapter)
        {
            _lineRoyalAxeMaps = currenLevelLineBuilder.Build(levelAdapter.BiomeDef.Lines, levelAdapter.Bounds);
            _endPoints = currenLevelLineBuilder.Build(levelAdapter.EndPointsModels);
            _yZero            = levelAdapter.Bounds.max.y + 0.3f;
        }

        public (Vector2 startPoint, Vector2 endPoint) GetPosForNewMob(string modDataMobId)
        {
            var startPoint = GetStartPoint(modDataMobId);
            var endpointModel = GetEndPoint(modDataMobId, startPoint);
            if (endpointModel == null) return (startPoint, startPoint);
            return (startPoint, endpointModel.Position);
        }

        private EndPointsRoyalAxeMap GetEndPoint(string modDataMobId, Vector2 startPos)
        {
            float min = float.MaxValue;
            EndPointsRoyalAxeMap result = null;
            for (int i = 0; i < _endPoints.Length; i++)
            {
                var endPoint = _endPoints[i];
                if (endPoint.Contains(modDataMobId))
                {
                    var mincandidat = Vector2.SqrMagnitude(startPos - endPoint.Position); 
                    if (mincandidat < min)
                    {
                        min = mincandidat;
                        result = endPoint;
                    }
                }
            }
            return result;
        }

        Vector2 GetStartPoint(string modDataMobId)
        {
            var lines = _lineRoyalAxeMaps.Where(o => o.CanSpawn(modDataMobId)).ToList();
            lines.Sort((o1, o2) => o1.MobAmount.CompareTo(o2.MobAmount));
            lines.RemoveAt(lines.Count - 1); // удаляем самый большой

            var line = Mathf.Abs(lines[0].MobAmount - lines[lines.Count - 1].MobAmount) > 1
                ? lines[0]
                : lines.GetRandom();

            return GetNextMobPosition(line);
        }

        public void Reset()
        {
            for (int i = 0; i < _lineRoyalAxeMaps.Length; i++)
            {
                _lineRoyalAxeMaps[i].Reset();
            }
        }

        private Vector2 GetNextMobPosition(LineRoyalAxeMap line)
        {
            var y = line.MobAmount++;
            var x =  Random.Range(line.MinX + OFFSET_X, line.MaxX - OFFSET_X);


            return new Vector2(x, _yZero + y * 2);
        }
    }
}
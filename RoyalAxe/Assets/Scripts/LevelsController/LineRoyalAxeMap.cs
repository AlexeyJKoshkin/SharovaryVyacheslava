using System.Collections.Generic;
using System.Linq;
using RoyalAxe.Map;
using UnityEngine;

namespace RoyalAxe.CoreLevel 
{
    public interface ILineRoyalAxeMapBuilder
    {
        LineRoyalAxeMap[] Build(LineModel[] viewModelLines, Bounds arenaBounds);
        EndPointsRoyalAxeMap[] Build(IReadOnlyList<EndPointMeleeMobPoint> levelAdapterEndPointsModels);
    }

    public class LineRoyalAxeMapBuilder : ILineRoyalAxeMapBuilder
    {
        private TileCoreMapSettings _tileCoreMapSettings;
        public LineRoyalAxeMapBuilder(TileCoreMapSettings tileCoreMapSettings)
        {
            _tileCoreMapSettings = tileCoreMapSettings;
        }

        public LineRoyalAxeMap[] Build(LineModel[] viewModelLines, Bounds arenaBounds)
        {
            float minX = arenaBounds.min.x;
            float maxX = minX;
            var result = new LineRoyalAxeMap[viewModelLines.Length];
            for (int i = 0; i < viewModelLines.Length; i++)
            {
                var lineModel = viewModelLines[i];
                maxX += lineModel.Size * _tileCoreMapSettings.CellSize;
                result[i] = new LineRoyalAxeMap(lineModel, i)
                {
                    MinX =minX,
                    MaxX = maxX
                };
                minX = maxX;
            }

            return result;
        }

        public EndPointsRoyalAxeMap[] Build(IReadOnlyList<EndPointMeleeMobPoint> levelAdapterEndPointsModels)
        {
            return levelAdapterEndPointsModels.Select(o => new EndPointsRoyalAxeMap(o)).ToArray();
        }
    }


    public class LineRoyalAxeMap
    {
        private readonly HashSet<string> _mobs;
        public float MinX;
        public float MaxX;
        public int MobAmount;
        public int LineIndex { get; private set; }

        public LineRoyalAxeMap(LineModel line, int index)
        {
            _mobs     = new HashSet<string>(line.MobId);
            LineIndex = index;
        }

        public bool CanSpawn(string mobId)
        {
            return _mobs.Count == 0 || mobId.Contains(mobId);
        }

        public void Reset()
        {
            MobAmount = 0;
        }

      
    }

    
    public class EndPointsRoyalAxeMap
    {
        public Vector2 Position { get; private set; }

        private HashSet<string> _mobIds;
        public EndPointsRoyalAxeMap(EndPointMeleeMobPoint model)
        {
        _mobIds = new HashSet<string>(model.MobIds);
        Position = model.PointPosition;
        }

        public bool Contains(string modDataMobId)
        {
            return _mobIds.Count == 0 || _mobIds.Contains(modDataMobId);
        }
    }
}
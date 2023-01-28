using System.Collections.Generic;
using System.Linq;
using Core;
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
            float minX   = arenaBounds.min.x;
            float maxX   = minX;
            var   result = new LineRoyalAxeMap[viewModelLines.Length];
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
}
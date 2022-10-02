/*using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace RoyalAxe.Map
{
    [CustomEditor(typeof(TileCoreMapSettings))]
    public class TileCoreMapSettingsEditor : OdinEditor
    {
        private TileCoreMapSettings Settings => target as TileCoreMapSettings;
        private TileSettings _settings => Settings.TileSettings;
        

        protected override void OnEnable()
        {
            base.OnEnable();
            SceneView.duringSceneGui += DrawSceneGUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SceneView.duringSceneGui -= DrawSceneGUI;
        }


        private void DrawSceneGUI(SceneView view)
        {
            TileMathUtility.TILE_SETTINGS = _settings;
            /*DrawHorizontalLines();
            DrawVerticalLines();#1#
            //   DrawLabels();
            CheckMouseClick(view);
        }

     

        private void CheckMouseClick(SceneView sceneView)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                var mousePos = sceneView.camera.ScreenToWorldPoint(Event.current.mousePosition);
                //This makes the mouse position adjust so it is relative to the world position
                mousePos.y = -(mousePos.y - 2 * sceneView.camera.transform.position.y);
                mousePos.z = 0;
              //  Debug.LogError(TileMathUtility.UnityToCell(mousePos));
            }
        }

        private void DrawVerticalLines()
        {
            Vector2 startPoint = _settings.StartPoint;
            for (int i = 0; i <= Settings.WidthMap; i++)
            {
                DrawVerticalLine(startPoint);
                startPoint.x += _settings.CellSizeInPixel;
            }
        }

        private void DrawHorizontalLines()
        {
            Vector2 startPoint = _settings.StartPoint;
            for (int i = 0; i <= Settings.HeightMap; i++)
            {
                DrawHorizontalLine(startPoint);
                startPoint.y += _settings.CellSizeInPixel;
            }
        }

        private void DrawHorizontalLine(Vector2 startPoint)
        {
            var endPoint = startPoint;
            endPoint.x += Settings.WidthMap * _settings.CellSizeInPixel;
            Handles.DrawLine(startPoint, endPoint);
        }

        private void DrawVerticalLine(Vector2 startPoint)
        {
            var endPoint = startPoint;
            endPoint.y += Settings.HeightMap * _settings.CellSizeInPixel;
            Handles.DrawLine(startPoint, endPoint);
        }
    }
}*/
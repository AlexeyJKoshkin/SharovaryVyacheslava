using UnityEngine;

namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Храним и вычисляем значения областей окна генератора Enum
    /// </summary>
    internal struct EnumWindowArea
    {
        public const int TOP = 5;
        public const int BORDER = 10;

        public Rect AddValueSettingsArea;
        public Rect EditingArea;
        public Rect DownArea;

        public EnumWindowArea(Rect windowPosition)
        {
            AddValueSettingsArea = new Rect(BORDER, TOP, windowPosition.width - BORDER * 2,
                                            windowPosition.height * 0.18f);
            EditingArea = new Rect(BORDER, AddValueSettingsArea.yMax + TOP, AddValueSettingsArea.width,
                                   windowPosition.height * 0.75f - TOP);
            DownArea = new Rect(BORDER, EditingArea.yMax, AddValueSettingsArea.width,
                                windowPosition.height * 0.12f - BORDER);
        }
    }
}
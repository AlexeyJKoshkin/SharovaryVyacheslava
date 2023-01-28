using System.Collections.Generic;

namespace RoyalAxe.CoreLevel 
{
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

        public override string ToString()
        {
            return $"[{MinX} - {MaxX}] Mobs : {MobAmount}";
        }
    }
}
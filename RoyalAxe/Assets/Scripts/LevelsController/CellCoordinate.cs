using System;

namespace RoyalAxe.Map
{
    /// <summary>
    ///     Координата в мире
    /// </summary>
    [Serializable]
    public struct CellCoordinate : IComparable<CellCoordinate>
    {
        public static readonly CellCoordinate One = new CellCoordinate(1, 1);
        public static readonly CellCoordinate Zero = new CellCoordinate(0, 0);

        public int X;

        public int Y;
        //   public int Y;

        public CellCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }


        public bool Equals(CellCoordinate other)
        {
            return X == other.X && Y == other.Y; // && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is CellCoordinate coorinate && Equals(coorinate);
        }

        public override int GetHashCode()
        {
            return X ^ Y; // ^ Y ;
        }

        public override string ToString()
        {
            return $"[{X} {Y}]";
        }


        public static bool operator ==(CellCoordinate c1, CellCoordinate c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(CellCoordinate c1, CellCoordinate c2)
        {
            return c1.X != c2.X || c1.Y != c2.Y;
        }

        public static CellCoordinate operator +(CellCoordinate c1, CellCoordinate c2)
        {
            return new CellCoordinate(c1.X + c2.X, c1.Y + c2.Y);
        }

        public static CellCoordinate operator -(CellCoordinate c1, CellCoordinate c2)
        {
            return new CellCoordinate(c1.X - c2.X, c1.Y - c2.Y);
        }

        public int CompareTo(CellCoordinate other)
        {
            var xComparison = X.CompareTo(other.X);
            return xComparison == 0 ? Y.CompareTo(other.Y) : xComparison;
        }
    }

    /// <summary>
    ///     Точки тайла
    /// </summary>
    public enum TilePointType
    {
        //центральная
        Center,
        Left,
        Right,
        Top,
        Bottom
    }
}
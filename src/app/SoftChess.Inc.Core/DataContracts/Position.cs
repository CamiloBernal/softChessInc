using System;

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace SoftChess.Inc.Core.DataContracts
{
    public struct Position : IEquatable<Position>
    {
        public int X, Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator !=(Position leftOperand, Position rightOperand)
        {
            return !(leftOperand == rightOperand);
        }

        public static bool operator ==(Position leftOperand, Position rightOperand)
        {
            return leftOperand.Equals(rightOperand);
        }

        public bool Equals(Position other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object obj) => Equals((Position) obj);

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
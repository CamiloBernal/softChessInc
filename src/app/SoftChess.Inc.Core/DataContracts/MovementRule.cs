using System;
using SoftChess.Inc.Core.Extensions;

namespace SoftChess.Inc.Core.DataContracts
{
    public class MovementRule
    {
        public MovementDirection AllowedDirection { get; set; }
        public int AllowedBlocksToMove { get; set; }

        public Tuple<bool, string> MovementIsValidBasedOnRule(Position startPosition, Position endPosition)
        {
            var errorMessage = string.Empty;
            var isValid = false;
            var direction = DetectDirection(startPosition, endPosition);
            if (!startPosition.IsInLimits())
            {
                errorMessage = "Start position is out of range";
            }
            else if (!endPosition.IsInLimits())
            {
                errorMessage = "End position is out of range";
            }
            else if (direction != AllowedDirection)
            {
                errorMessage = $"The direction {direction} is not allowed for this piece";
            }
            else if (!AllowedBlocksToMove.Equals(Constants.BlockMovementWildcard) &&
                     CountBlocksToMove(startPosition, endPosition) > AllowedBlocksToMove)
            {
                errorMessage = "The number of positions to move piece is not allowed";
            }
            else
            {
                isValid = true;
            }
            return Tuple.Create(isValid, errorMessage);
        }

        public int CountBlocksToMove(Position startPosition, Position endPosition)
        {
            var direction = DetectDirection(startPosition, endPosition);
            int blockCount;
            switch (direction)
            {
                case MovementDirection.Unknow:
                    blockCount = Math.Abs(endPosition.X - startPosition.X) + Math.Abs(endPosition.Y - startPosition.Y);
                    break;

                case MovementDirection.Up:
                case MovementDirection.Down:
                    blockCount = Math.Abs(endPosition.Y - startPosition.Y);
                    break;

                case MovementDirection.Left:
                case MovementDirection.Right:
                    blockCount = Math.Abs(endPosition.X - startPosition.X);
                    break;

                case MovementDirection.Diagonal:
                    blockCount = Math.Abs(endPosition.Y - startPosition.Y);
                    break;

                case MovementDirection.LMovement:
                    blockCount = Constants.BlockMovementWildcard;
                    break;

                case MovementDirection.None:
                    blockCount = 0;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return blockCount;
        }

        public MovementDirection DetectDirection(Position startPosition, Position endPosition)
        {
            var direction = MovementDirection.Unknow;
            if (startPosition.Equals(endPosition))
            {
                direction = MovementDirection.None;
            }
            else
            {
                if (startPosition.X == endPosition.X)
                {
                    //Movement only on Y axis
                    direction = startPosition.Y < endPosition.Y ? MovementDirection.Up : MovementDirection.Down;
                }

                if (startPosition.Y == endPosition.Y)
                {
                    //Movement only on X axis
                    direction = startPosition.X < endPosition.X ? MovementDirection.Right : MovementDirection.Left;
                }

                if (Math.Abs(endPosition.X - startPosition.X).Equals(Math.Abs(endPosition.Y - startPosition.Y)))
                    direction = MovementDirection.Diagonal;

                if (((Math.Abs(startPosition.X - endPosition.X) == 2) &&
                     (Math.Abs(startPosition.Y - endPosition.Y) == 1)) ||
                    ((Math.Abs(startPosition.X - endPosition.X) == 1) &&
                     (Math.Abs(startPosition.Y - endPosition.Y) == 2)))
                {
                    direction = MovementDirection.LMovement;
                }
            }
            return direction;
        }
    }
}
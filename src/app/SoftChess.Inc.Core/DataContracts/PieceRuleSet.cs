using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftChess.Inc.Core.DataContracts
{
    public class PieceRuleSet
    {
        /// <summary>
        /// Piece for which the rules apply
        /// </summary>
        public Piece ForPiece { get; set; }

        /// <summary>
        /// List of rules for piece movement.
        /// </summary>
        public IList<MovementRule> Rules { get; } = new List<MovementRule>();

        /// <summary>
        /// This method evaluates all rules for piece an return if all rules returned is valid.
        /// </summary>
        /// <param name="startPosition">Current piece position</param>
        /// <param name="endPosition">Future piece position</param>
        /// <returns>Tuple indicating whether if movement is valid. And error message if apply.</returns>
        public Tuple<bool, string> MovementIsValid(Position startPosition, Position endPosition)
        {
            var isValid = false;
            var lastMessage = string.Empty;
            foreach (var validationResult in Rules.Select(rule => rule.MovementIsValidBasedOnRule(startPosition, endPosition)))
            {
                isValid = isValid || validationResult.Item1;
                lastMessage = validationResult.Item2;
            }
            return Tuple.Create(isValid, lastMessage);
        }



        /// <summary>
        /// Get all available piece movements based on all rules.
        /// </summary>
        /// <param name="startPosition">Current piece position</param>
        /// <returns>List of all available movements for piece</returns>
        public IEnumerable<Position> GetAvailableMovements(Position startPosition)
        {

            //var rookRuleUp = new MovementRule
            //{
            //    AllowedDirection = MovementDirection.Up,
            //    AllowedBlocksToMove = Constants.BlockMovementWildcard
            //};
            //var rookRuleDown = new MovementRule
            //{
            //    AllowedDirection = MovementDirection.Down,
            //    AllowedBlocksToMove = Constants.BlockMovementWildcard
            //};

            //var rookRuleLeft = new MovementRule
            //{
            //    AllowedDirection = MovementDirection.Left,
            //    AllowedBlocksToMove = Constants.BlockMovementWildcard
            //};

            //var rookRuleRight = new MovementRule
            //{
            //    AllowedDirection = MovementDirection.Right,
            //    AllowedBlocksToMove = Constants.BlockMovementWildcard
            //};


            //Rules.Add(rookRuleUp);
            //Rules.Add(rookRuleDown);
            //Rules.Add(rookRuleLeft);
            //Rules.Add(rookRuleRight);

            //var start = new Position(3,4);
            //var end = new Position(3,5);



            var posibleMovements = GetPosibleMovements();
            //return (from movement in posibleMovements where !movement.Equals(startPosition) let isValid = MovementIsValid(startPosition, movement).Item1 where isValid select movement).ToList();

            var p = posibleMovements.Where(m => !m.Equals(startPosition) && MovementIsValid(startPosition, m).Item1);
            return p;


        }

        /// <summary>
        /// Get the posible movements in chess board. All unvalidated.
        /// </summary>
        /// <returns>Complete Chess board based on limits</returns>
        private static IEnumerable<Position> GetPosibleMovements()
        {
            var posibleMovements = new List<Position>();
            for (var x = Constants.XAxisMinLimit; x <= Constants.XAxisMaxLimit; x++)
            {
                for (var y = Constants.YAxisMinLimit; y <= Constants.YAxisMaxLimit; y++)
                {
                    posibleMovements.Add(new Position(x, y));
                }
            }
            return posibleMovements;
        }
    }
}
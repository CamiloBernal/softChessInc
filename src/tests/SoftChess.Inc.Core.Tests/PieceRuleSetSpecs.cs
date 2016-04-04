using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Core.Tests
{
    [TestClass]
    public sealed class CuandoSeConfiguranReglasParaElMovimientoDelAlfil : PieceRuleSetSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementFactory")]
        public override void debe_retornar_todos_los_movimientos_posibles_para_la_pieza()
        {
            ConfigTest();
            GetAvailableMovements();
            Assert.AreEqual(13, AvailablePositions.Count());

            var movements = AvailablePositions.ToList();

            CollectionAssert.Contains(movements, new Position(4, 5));
            CollectionAssert.Contains(movements, new Position(5, 6));
            CollectionAssert.Contains(movements, new Position(6, 7));
            CollectionAssert.Contains(movements, new Position(2, 5));
            CollectionAssert.Contains(movements, new Position(1, 6));
            CollectionAssert.Contains(movements, new Position(0, 7));
            CollectionAssert.Contains(movements, new Position(4, 3));
            CollectionAssert.Contains(movements, new Position(5, 2));
            CollectionAssert.Contains(movements, new Position(6, 1));
            CollectionAssert.Contains(movements, new Position(7, 0));
            CollectionAssert.Contains(movements, new Position(2, 3));
            CollectionAssert.Contains(movements, new Position(1, 2));
            CollectionAssert.Contains(movements, new Position(0, 1));
        }

        protected override void ConfigTest()
        {
            var bishopRule = new MovementRule
            {
                AllowedDirection = MovementDirection.Diagonal,
                AllowedBlocksToMove = Constants.BlockMovementWildcard
            };

            RuleSet.Rules.Add(bishopRule);
            StartPosition = new Position(3, 4);
        }
    }

    [TestClass]
    public sealed class CuandoSeConfiguranReglasParaElMovimientoDeLaTorre : PieceRuleSetSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementFactory")]
        public override void debe_retornar_todos_los_movimientos_posibles_para_la_pieza()
        {
            ConfigTest();
            GetAvailableMovements();
            Assert.AreEqual(14, AvailablePositions.Count());

            var movements = AvailablePositions.ToList();

            CollectionAssert.Contains(movements, new Position(0, 4));
            CollectionAssert.Contains(movements, new Position(1, 4));
            CollectionAssert.Contains(movements, new Position(2, 4));
            CollectionAssert.Contains(movements, new Position(4, 4));
            CollectionAssert.Contains(movements, new Position(5, 4));
            CollectionAssert.Contains(movements, new Position(6, 4));
            CollectionAssert.Contains(movements, new Position(7, 4));
            CollectionAssert.Contains(movements, new Position(3, 0));
            CollectionAssert.Contains(movements, new Position(3, 1));
            CollectionAssert.Contains(movements, new Position(3, 2));
            CollectionAssert.Contains(movements, new Position(3, 3));
            CollectionAssert.Contains(movements, new Position(3, 5));
            CollectionAssert.Contains(movements, new Position(3, 6));
            CollectionAssert.Contains(movements, new Position(3, 7));
        }

        protected override void ConfigTest()
        {
            var rookRuleUp = new MovementRule
            {
                AllowedDirection = MovementDirection.Up,
                AllowedBlocksToMove = Constants.BlockMovementWildcard
            };
            var rookRuleDown = new MovementRule
            {
                AllowedDirection = MovementDirection.Down,
                AllowedBlocksToMove = Constants.BlockMovementWildcard
            };

            var rookRuleLeft = new MovementRule
            {
                AllowedDirection = MovementDirection.Left,
                AllowedBlocksToMove = Constants.BlockMovementWildcard
            };

            var rookRuleRight = new MovementRule
            {
                AllowedDirection = MovementDirection.Right,
                AllowedBlocksToMove = Constants.BlockMovementWildcard
            };

            RuleSet.Rules.Add(rookRuleUp);
            RuleSet.Rules.Add(rookRuleDown);
            RuleSet.Rules.Add(rookRuleLeft);
            RuleSet.Rules.Add(rookRuleRight);
            StartPosition = new Position(3, 4);
        }
    }

    [TestClass]
    public sealed class CuandoSeConfiguranReglasParaElMovimientoDelPeon : PieceRuleSetSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementFactory")]
        public override void debe_retornar_todos_los_movimientos_posibles_para_la_pieza()
        {
            ConfigTest();
            GetAvailableMovements();
            Assert.AreEqual(1, AvailablePositions.Count());
            CollectionAssert.Contains(AvailablePositions.ToList(), new Position(3, 3));
        }

        protected override void ConfigTest()
        {
            var pawnRule = new MovementRule
            {
                AllowedDirection = MovementDirection.Up,
                AllowedBlocksToMove = 1
            };

            RuleSet.Rules.Add(pawnRule);
            StartPosition = new Position(3, 2);
        }
    }

    public abstract class PieceRuleSetSpecsBase
    {
        protected IEnumerable<Position> AvailablePositions { get; set; }
        protected PieceRuleSet RuleSet { get; } = new PieceRuleSet();
        protected Position StartPosition { get; set; }

        public abstract void debe_retornar_todos_los_movimientos_posibles_para_la_pieza();

        protected abstract void ConfigTest();

        protected void GetAvailableMovements()
        {
            AvailablePositions = RuleSet.GetAvailableMovements(StartPosition);
        }
    }
}
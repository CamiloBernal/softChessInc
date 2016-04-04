using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Core.Tests
{
    [TestClass]
    public sealed class CuandoHayMovimientoEnL : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(Constants.BlockMovementWildcard, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(3, 2);
            EndPosition = new Position(5, 3);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoNoHayMovimiento : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(0, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(6, 7);
            EndPosition = new Position(6, 7);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoSeMueve1BloqueHaciaArriba : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(1, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(0, 1);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoSeMueve1BloqueHaciaLaDerecha : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(1, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(4, 5);
            EndPosition = new Position(5, 5);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoSeMueve4PosicionesEnDiagonal : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(4, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(1, 0);
            EndPosition = new Position(5, 4);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoSeMueve6BloqueHaciaAbajo : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(6, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 6);
            EndPosition = new Position(0, 0);
            CountBlocksToMove();
        }
    }

    [TestClass]
    public sealed class CuandoSeMueve6BloqueHaciaLaIzquierda : MovementRuleBlockCountSpecsBase
    {
        [TestMethod]
        [TestCategory("BlockCount")]
        public override void debe_contar_apropiadamente_los_bloques_movidos()
        {
            ConfigTest();
            Assert.AreEqual(6, BlockCount);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(6, 5);
            EndPosition = new Position(0, 5);
            CountBlocksToMove();
        }
    }

    public abstract class MovementRuleBlockCountSpecsBase
    {
        protected int BlockCount { get; private set; }
        protected Position EndPosition { get; set; }
        protected Position StartPosition { get; set; }

        public abstract void debe_contar_apropiadamente_los_bloques_movidos();

        protected abstract void ConfigTest();

        protected void CountBlocksToMove()
        {
            var rule = new MovementRule();
            BlockCount = rule.CountBlocksToMove(StartPosition, EndPosition);
        }
    }
}
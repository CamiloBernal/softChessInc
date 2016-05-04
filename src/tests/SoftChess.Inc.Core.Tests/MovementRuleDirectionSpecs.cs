using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Core.Tests
{
    [TestClass]
    public sealed class CuandoNoSeRealizaMovimientoYSeLlamaAlMetodo : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.None;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(0, 0);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoAmbiguo : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Unknow;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(6, 2);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoEnL1 : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.LMovement;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(2, 3);
            EndPosition = new Position(4, 4);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoEnL2 : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.LMovement;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(3, 2);
            EndPosition = new Position(2, 4);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoEnL3 : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.LMovement;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(3, 2);
            EndPosition = new Position(5, 1);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoALaDerecha : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Right;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(10, 0);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoALaIzquierda : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Left;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(3, 0);
            EndPosition = new Position(0, 0);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoEnDiagonalAbajo : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Diagonal;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(4, 5);
            EndPosition = new Position(1, 2);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoEnDiagonalArriba : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Diagonal;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(1, 0);
            EndPosition = new Position(4, 3);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoHaciaAbajo : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Down;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 5);
            EndPosition = new Position(0, 3);
            DetectMovement();
        }
    }

    [TestClass]
    public sealed class CuandoSeRealizaUnMovimientoHaciaArriba : MovementRuleDirectionSpecsBase
    {
        [TestMethod]
        [TestCategory("Direction")]
        public override void debe_identificar_la_direccion_del_movimiento()
        {
            const MovementDirection expected = MovementDirection.Up;
            ConfigTest();
            Assert.AreEqual(expected, MovementDirection);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(0, 5);
            DetectMovement();
        }
    }

    public abstract class MovementRuleDirectionSpecsBase
    {
        protected Position EndPosition { get; set; }
        protected MovementDirection MovementDirection { get; set; }
        protected Position StartPosition { get; set; }

        public abstract void debe_identificar_la_direccion_del_movimiento();

        protected abstract void ConfigTest();

        protected void DetectMovement()
        {
            var rule = new MovementRule();
            MovementDirection = rule.DetectDirection(StartPosition, EndPosition);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftChess.Inc.Core.DataContracts;

namespace SoftChess.Inc.Core.Tests
{
    [TestClass]
    public sealed class CuandoElMovimientoPermitidoEsHaciaArribaUnaPosicionYElNumeroDePosicionesSeExcede :
        MovementRuleIsValidSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementValidation")]
        public override void debe_identificar_apropiadamente_si_el_movimiento_es_valido()
        {
            ConfigTest();
            ValidateMovement();
            Assert.AreEqual(false, IsValid.Item1);
            Assert.AreEqual("The number of positions to move piece is not allowed", IsValid.Item2);
        }

        protected override void ConfigTest()
        {
            AllowedDirection = MovementDirection.Up;
            AllowedBlocksToMove = 1;
            StartPosition = new Position(0, 0);
            EndPosition = new Position(0, 3);
        }
    }

    [TestClass]
    public sealed class CuandoElMovimientoPermitidoEsHaciaArribaUnaPosicionYHayHappyPath : MovementRuleIsValidSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementValidation")]
        public override void debe_identificar_apropiadamente_si_el_movimiento_es_valido()
        {
            ConfigTest();
            ValidateMovement();
            Assert.AreEqual(true, IsValid.Item1);
            Assert.AreEqual(string.Empty, IsValid.Item2);
        }

        protected override void ConfigTest()
        {
            AllowedDirection = MovementDirection.Up;
            AllowedBlocksToMove = 1;
            StartPosition = new Position(0, 0);
            EndPosition = new Position(0, 1);
        }
    }

    [TestClass]
    public sealed class CuandoElMovimientoPermitidoEsHaciaArribaYLaDireccionNoCoincide : MovementRuleIsValidSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementValidation")]
        public override void debe_identificar_apropiadamente_si_el_movimiento_es_valido()
        {
            ConfigTest();
            ValidateMovement();
            Assert.AreEqual(false, IsValid.Item1);
            Assert.AreEqual("The direction Unknow is not allowed for this piece", IsValid.Item2);
        }

        protected override void ConfigTest()
        {
            AllowedDirection = MovementDirection.Up;
            StartPosition = new Position(0, 0);
            EndPosition = new Position(5, 3);
        }
    }

    [TestClass]
    public sealed class CuandoLaPosicionFinalNoEstaDentroDeLosLimites : MovementRuleIsValidSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementValidation")]
        public override void debe_identificar_apropiadamente_si_el_movimiento_es_valido()
        {
            ConfigTest();
            ValidateMovement();
            Assert.AreEqual(false, IsValid.Item1);
            Assert.AreEqual("End position is out of range", IsValid.Item2);
        }

        protected override void ConfigTest()
        {
            EndPosition = new Position(0, 8);
        }
    }

    [TestClass]
    public sealed class CuandoLaPosicionInicialNoEstaDentroDeLosLimites : MovementRuleIsValidSpecsBase
    {
        [TestMethod]
        [TestCategory("MovementValidation")]
        public override void debe_identificar_apropiadamente_si_el_movimiento_es_valido()
        {
            ConfigTest();
            ValidateMovement();
            Assert.AreEqual(false, IsValid.Item1);
            Assert.AreEqual("Start position is out of range", IsValid.Item2);
        }

        protected override void ConfigTest()
        {
            StartPosition = new Position(8, 0);
        }
    }

    public abstract class MovementRuleIsValidSpecsBase
    {
        public int AllowedBlocksToMove { get; set; }
        public MovementDirection AllowedDirection { get; set; }
        protected Position EndPosition { get; set; }
        protected Tuple<bool, string> IsValid { get; private set; }
        protected Position StartPosition { get; set; }

        public abstract void debe_identificar_apropiadamente_si_el_movimiento_es_valido();

        protected abstract void ConfigTest();

        protected void ValidateMovement()
        {
            var rule = new MovementRule
            {
                AllowedDirection = AllowedDirection,
                AllowedBlocksToMove = AllowedBlocksToMove
            };
            IsValid = rule.MovementIsValidBasedOnRule(StartPosition, EndPosition);
        }
    }
}
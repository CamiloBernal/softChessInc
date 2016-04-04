using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftChess.Inc.Core.DataContracts;
using SoftChess.Inc.Core.Extensions;

namespace SoftChess.Inc.Core.Tests
{
    [TestClass]
    public class MovementRuleLimitsSpecs
    {
        protected Position EndPosition { get; set; }
        protected Position StartPosition { get; set; }

        [TestMethod]
        [TestCategory("Limits")]
        public void debe_identificar_si_la_posicion_esta_dentro_de_los_limites()
        {
            ConfigTest();
            var startPositionIsInlimits = StartPosition.IsInLimits();
            var endPositionIsInlimits = EndPosition.IsInLimits();
            Assert.AreEqual(true, startPositionIsInlimits);
            Assert.AreEqual(false, endPositionIsInlimits);
        }

        protected void ConfigTest()
        {
            StartPosition = new Position(0, 0);
            EndPosition = new Position(-1, 8);
        }
    }
}
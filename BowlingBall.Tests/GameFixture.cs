using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BowlingBall.Game;

namespace BowlingBall.Tests
{
    [TestClass]
    public class GameFixture
    {
        readonly Game _game = new Game();

        [TestMethod]
        public void Gutter_game_score_should_be_zero_test()
        {
            var game = new Game();
            Roll(game, 0, 20);
            Assert.AreEqual(0, game.GetScore());
        }

        private void Roll(Game game, int pins, int times)
        {
            for (int i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }

        [TestMethod]
        public void Test_GivenScores()
        {
            _game.RollStrike();
            _game.Roll(9, 1);
            _game.Roll(5, 5);
            _game.Roll(7, 2);
            _game.RollStrike();
            _game.RollStrike();
            _game.RollStrike();
            _game.Roll(9, 0);
            _game.Roll(8, 2);
            _game.RollFinalFrame(9, 1, 10);
            Assert.AreEqual(187, _game.GetScore());
        }

        [TestMethod]
        public void Test_ZeroScore()
        {
            for (int i = 0; i < 10; i++) { _game.Roll(0, 0); }
            Assert.AreEqual(0, _game.GetScore());
        }
        [TestMethod]
        public void Test_RollingSameScore()
        {
            for (int i = 0; i < 10; i++) { _game.Roll(2, 2); }
            Assert.AreEqual(40, _game.GetScore());
        }

        [TestMethod]
        public void Test_CommonScore()
        {
            _game.Roll(3, 3);
            _game.Roll(3, 3);
            _game.Roll(4, 4);
            _game.Roll(4, 4);
            _game.Roll(5, 5);
            _game.Roll(5, 5);
            _game.Roll(6, 6);
            _game.Roll(6, 6);
            _game.Roll(7, 7);
            _game.Roll(7, 7);
            Assert.AreEqual(111, _game.GetScore());
        }

        [TestMethod]
        public void Test_AnotherScore()
        {
            _game.Roll(8, 0);
            _game.Roll(7, 0);
            _game.Roll(5, 3);
            _game.Roll(9, 1);
            _game.Roll(9, 1);
            _game.RollStrike();
            _game.Roll(8, 0);
            _game.Roll(5, 1);
            _game.Roll(3, 7);
            _game.RollFinalFrame(9, 0, 0);
            Assert.AreEqual(122, _game.GetScore());
        }
    }
}

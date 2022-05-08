using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingBall
{
    public class Game
    {
        private readonly List<Frame> _frames = new List<Frame>();

        private int[] rolls = new int[21];
        int currentRoll = 0;

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;
        }

        public int GetScore()
        {
            if(currentRoll == 20)
                return 0;

            Add(new Open(0, 0));
            Add(new Open(0, 0));

            for (int i = 0; i < 10; i++)
                _frames[i].AddBonus(_frames[i + 1], _frames[i + 2]);

            int _totalScore = 0;
            _frames.ForEach(frame => _totalScore += frame.Score());
            return _totalScore;
        }

        public void Roll(int firstRoll, int secondRoll)
        {
            if (GameFinished())
                throw new GameOverException();

            Add(Frame.Create(firstRoll, secondRoll));
        }

        public void RollStrike()
        {
            Roll(10, 0);
        }

        private bool GameFinished()
        {
            return _frames.Count.Equals(10);
        }

        private void Add(Frame frame)
        {
            _frames.Add(frame);
        }

        public void RollFinalFrame(int firstRoll, int secondRoll, int thirdRoll)
        {
            Add(Frame.Create(firstRoll, secondRoll, thirdRoll));
        }
    }

    public abstract class Frame
    {
        public int _firstRoll;
        public int _secondRoll;
        public int _frameScore;
        public Frame(int firstRoll, int secondRoll)
        {
            _firstRoll = firstRoll;
            _secondRoll = secondRoll;
        }

        public int FirstRoll() { return _firstRoll; }

        public int SecondRoll() { return _secondRoll; }

        public int Score() { return _firstRoll + _secondRoll + _frameScore; }

        public static Frame Create(int firstRoll, int secondRoll)
        {
            if (firstRoll == 10)
                return new Strike();

            if ((firstRoll + secondRoll) == 10)
                return new Spare(firstRoll, secondRoll);

            return new Open(firstRoll, secondRoll);
        }

        public static Frame Create(int firstRoll, int secondRoll, int thirdRoll)
        {
            return new FinalScore(firstRoll, secondRoll, thirdRoll);
        }

        public virtual void AddBonus(Frame framePlusOne, Frame framePlusTwo) { }

        public void AddBonus(int points)
        {
            _frameScore += points;
        }

    }

    public class Open : Frame
    {
        public Open(int firstRoll, int secondRoll) : base(firstRoll, secondRoll) { }
    }

    public class Strike : Frame
    {
        public Strike() : base(10, 0) { }
        public override void AddBonus(Frame framePlusOne, Frame framePlusTwo)
        {
            if (framePlusOne is Strike)
                _frameScore += 10 + framePlusTwo.FirstRoll();
            else
                _frameScore += framePlusOne.FirstRoll() + framePlusOne.SecondRoll();
        }
    }

    public class Spare : Frame
    {
        public Spare(int firstRoll, int secondRoll) : base(firstRoll, secondRoll) { }

        public override void AddBonus(Frame framePlusOne, Frame framePlusTwo)
        {
            _frameScore += framePlusOne.FirstRoll();
        }
    }

    public class FinalScore : Frame
    {
        private readonly int _thirdRoll;

        public FinalScore(int firstRoll, int secondRoll, int thirdRoll) : base(firstRoll, secondRoll)
        {
            _thirdRoll = thirdRoll;
        }

        public override void AddBonus(Frame framePlusOne, Frame framePlusTwo)
        {
            _frameScore += _thirdRoll;
        }

    }

    public class GameOverException : Exception
    {
    }
}

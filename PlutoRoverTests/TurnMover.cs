using System;

namespace PlutoRoverTests
{
    public class TurnMover : MoverBase
    {
        private readonly string[] _rightTurnSequence = new[] {"N", "E", "S", "W"};
        private readonly string[] _leftTurnSequence = new[] {"N", "W", "S", "E"};
        public TurnMover(string[] currentRoverLocation, string move) 
            : base(currentRoverLocation, move)
        {
       
        }

        public override string[] ExecuteAndReturnStatus()
        {
            var sequenceToUse = _move == "R"?_rightTurnSequence : _leftTurnSequence;

            var currentIndex = Array.IndexOf(sequenceToUse, _currentRoverLocation[2]);

            var nextDirection = currentIndex + 1;
            if (nextDirection > _rightTurnSequence.Length - 1)
                nextDirection = 0;

            _currentRoverLocation[2] = sequenceToUse[nextDirection];
            return _currentRoverLocation;
        }
    }
}
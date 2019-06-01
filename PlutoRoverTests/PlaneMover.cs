using System;

namespace PlutoRoverTests
{
    public class PlaneMover : MoverBase
    {

        private string _roverFacing;

        public PlaneMover(string[] currentRoverLocation, string move)
            : base(currentRoverLocation, move)
        {
            _roverFacing = _currentRoverLocation[2];
        }

        public override string[] ExecuteAndReturnStatus()
        {
            Func<int, int> op = x => x - 1;

            if ((_move == "F" && _roverFacing == "N")
                || (_move == "B" && _roverFacing == "S")
                || (_move == "F" && _roverFacing == "E")
                || (_move == "B" && _roverFacing == "W"))
                op = x => x + 1;

            var axisToWorkOn = _roverFacing == "N"
                               || _roverFacing == "S"
                ? 1
                : 0;

            Move(Convert.ToInt32(_currentRoverLocation[axisToWorkOn]), op, axisToWorkOn);

            return _currentRoverLocation;
        }

        private void Move(int start, Func<int, int> op, int axisToMoveOn)
        {
            _currentRoverLocation[axisToMoveOn] = (op.Invoke(start)).ToString();
        }
    }
}
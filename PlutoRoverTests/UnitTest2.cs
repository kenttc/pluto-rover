using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlutoRoverTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void given_rover_default_when_get_position_called_will_report_default_position()
        {
            var expectedRoverPosition = new string[] {"0", "0", "N"};
            var rover = new RoverNumber2();

            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }

        [TestMethod]
        public void given_rover_location_when_get_position_called_will_report_current_position()
        {
            var currentRoverPosition = new string[] {"1", "1", "N"};
            var expectedRoverPosition = new string[] {"1", "1", "N"};
            var rover = new RoverNumber2();
            rover.SetCurrentPosition(currentRoverPosition);
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }

        [TestMethod]
        public void given_rover_is_sent_move_f_or_b_command_will_be_able_to_move()
        {
            SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
                , "F", new string[] {"0", "1", "N"});
            SendMoveAndAssertLocation(new string[] {"0", "2", "N"}
                , "B", new string[] {"0", "1", "N"});

            SendMoveAndAssertLocation(new string[] { "0", "2", "S" }
                , "F", new string[] { "0", "1", "S" });
        }


        public void SendMoveAndAssertLocation(string[] currentRoverLocation, string move,
            string[] expectedRoverPosition)
        {
            var rover = new RoverNumber2();
            rover.SetCurrentPosition(currentRoverLocation);
            rover.SendCommand(move);
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }
    }

    public class RoverNumber2
    {
        private string[] _currentRoverLocation = new string[] {"0", "0", "N"};


        public string[] GetPosition()
        {
            return _currentRoverLocation;
        }

        public void SetCurrentPosition(string[] currentRoverPosition)
        {
            _currentRoverLocation = currentRoverPosition;
        }

        public void SendCommand(string move)
        {
            if ((_currentRoverLocation[2] == "N" || _currentRoverLocation[2] == "S") && (move == "F" || move == "B"))
            {
                _currentRoverLocation = new YPlaneMover(_currentRoverLocation, move).ExecuteAndReturnStatus();
            }
        }
    }

    public class YPlaneMover
    {
        private readonly string[] _currentRoverLocation;
        private readonly string _move;

        public YPlaneMover(string[] currentRoverLocation, string move)
        {
            _currentRoverLocation = currentRoverLocation;
            _move = move;
        }


        public string[] ExecuteAndReturnStatus()
        {

            Func<int, int> op = x => x - 1; 

            if (_move == "F" && _currentRoverLocation[2] == "N")
                op = x => x + 1;
            
            Move(Convert.ToInt32(_currentRoverLocation[1]), op);

            return _currentRoverLocation;
        }

        private void Move(int start, Func<int, int> op)
        {
            _currentRoverLocation[1] = (op.Invoke(start)).ToString();

        }
    }
}
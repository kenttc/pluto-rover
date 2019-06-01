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
        public void given_rover_is_sent_move_f_or_b_command_will_be_able_to_move_y_plane()
        {
            SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
                , "F", new string[] {"0", "1", "N"});
            SendMoveAndAssertLocation(new string[] {"0", "2", "N"}
                , "B", new string[] {"0", "1", "N"});

            SendMoveAndAssertLocation(new string[] {"0", "2", "S"}
                , "F", new string[] {"0", "1", "S"});
            SendMoveAndAssertLocation(new string[] {"0", "2", "S"}
                , "B", new string[] {"0", "3", "S"});
        }

        [TestMethod]
        public void given_rover_is_sent_move_f_or_b_command_will_be_able_to_move_x_plane()
        {
            SendMoveAndAssertLocation(new string[] {"0", "0", "E"}
                , "F", new string[] {"1", "0", "E"});
            SendMoveAndAssertLocation(new string[] {"2", "0", "E"}
                , "B", new string[] {"1", "0", "E"});

            SendMoveAndAssertLocation(new string[] {"2", "0", "W"}
                , "F", new string[] {"1", "0", "W"});
            SendMoveAndAssertLocation(new string[] {"2", "0", "W"}
                , "B", new string[] {"3", "0", "W"});
        }

        [TestMethod]
        public void given_rover_is_sent_move_l_or_r_command_will_be_able_to_turn()
        {
            SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
                , "R", new string[] {"0", "0", "E"});
            SendMoveAndAssertLocation(new string[] {"0", "0", "E"}
                , "R", new string[] {"0", "0", "S"});

            SendMoveAndAssertLocation(new string[] {"0", "0", "S"}
                , "R", new string[] {"0", "0", "W"});
            SendMoveAndAssertLocation(new string[] {"0", "0", "W"}
                , "R", new string[] {"0", "0", "N"});


            SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
                , "L", new string[] {"0", "0", "W"});
            SendMoveAndAssertLocation(new string[] { "0", "0", "W" }
                , "L", new string[] { "0", "0", "S" });

            SendMoveAndAssertLocation(new string[] { "0", "0", "S" }
                , "L", new string[] { "0", "0", "E" });
            SendMoveAndAssertLocation(new string[] { "0", "0", "E" }
                , "L", new string[] { "0", "0", "N" });
        }

        [TestMethod]
        public void given_rover_is_sent_multiple_moves_command_will_be_able_to_move()
        {
            SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
                , "FF", new string[] {"0", "2", "N"});
            SendMoveAndAssertLocation(new string[] { "0", "2", "N" }
                , "BB", new string[] { "0", "0", "N" });

            //SendMoveAndAssertLocation(new string[] {"0", "0", "S"}
            //    , "R", new string[] {"0", "0", "W"});
            //SendMoveAndAssertLocation(new string[] {"0", "0", "W"}
            //    , "R", new string[] {"0", "0", "N"});


            //SendMoveAndAssertLocation(new string[] {"0", "0", "N"}
            //    , "L", new string[] {"0", "0", "W"});
            //SendMoveAndAssertLocation(new string[] {"0", "0", "W"}
            //    , "L", new string[] {"0", "0", "S"});

            //SendMoveAndAssertLocation(new string[] {"0", "0", "S"}
            //    , "L", new string[] {"0", "0", "E"});
            //SendMoveAndAssertLocation(new string[] {"0", "0", "E"}
            //    , "L", new string[] {"0", "0", "N"});
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
            var currentMoveIndex = 0;
            while (move.Length > currentMoveIndex)
            {
                var currentMove = move[currentMoveIndex].ToString();
                var mover = new MoverSelector(currentMove).Get(_currentRoverLocation);
                _currentRoverLocation = mover.ExecuteAndReturnStatus();
                currentMoveIndex++;
            }
                

        }
    }

    public class MoverSelector
    {
        private readonly string _currentMove;

        public MoverSelector(string currentMove)
        {
            _currentMove = currentMove;
        }

        public MoverBase Get(string[] currentRoverLocation)
        {
            if(_currentMove == "R" || _currentMove == "L")
                return new TurnMover(currentRoverLocation, _currentMove);
                 
            return new PlaneMover(currentRoverLocation, _currentMove);
        }
    }

    public abstract class MoverBase
    {
        protected string[] _currentRoverLocation;
        protected string _move;

        protected MoverBase(string[] currentRoverLocation, string move)
        {
            _currentRoverLocation = currentRoverLocation;
            _move = move;
        }

        public abstract string[] ExecuteAndReturnStatus();
    }

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
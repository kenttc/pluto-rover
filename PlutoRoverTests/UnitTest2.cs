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

            SendMoveAndAssertLocation(new string[] { "0", "0", "N" }
                , "FFRFF", new string[] { "2", "2", "E" });

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
}
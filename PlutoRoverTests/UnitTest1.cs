using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlutoRoverTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void given_rover_location_when_get_position_called_will_report_current_position()
        {
            var currentRoverLocation = new string[]{"0", "0", "N"};
            var expectedRoverPosition= new string[] { "0", "0", "N" };
            var rover = new Rover(currentRoverLocation);

            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }

        [TestMethod]
        public void given_rover_location_and_send_command_forward_when_get_position_called_will_report_current_position()
        {
            var currentRoverLocation = new string[] { "0", "0", "N" };
            var expectedRoverPosition = new string[] { "0", "1", "N" };
            var rover = new Rover(currentRoverLocation);
            rover.SendCommand("F");
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }

        [TestMethod]
        public void given_rover_location_and_send_command_back_when_get_position_called_will_report_current_position()
        {
            var currentRoverLocation = new string[] { "0", "1", "N" };
            var expectedRoverPosition = new string[] { "0", "0", "N" };
            var rover = new Rover(currentRoverLocation);
            rover.SendCommand("B");
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }
        [TestMethod]
        public void given_rover_location_and_send_command_right_when_get_position_called_will_report_current_position()
        {
            var currentRoverLocation = new string[] { "0", "0", "N" };
            var expectedRoverPosition = new string[] { "0", "0", "E" };
            var rover = new Rover(currentRoverLocation);
            rover.SendCommand("R");
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }
    }
}

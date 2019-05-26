using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlutoRoverTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]//and_move_command_
        public void given_rover_location_when_get_position_called_will_report_current_position()
        {
            var currentRoverLocation = new string[]{"0", "0", "N"};
            var expectedRoverPosition= new string[] { "0", "0", "N" };
            var rover = new Rover(currentRoverLocation);

            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition));

        }
    }

    public class Rover
    {
        public Rover(string[] currentRoverLocation)
        {
            throw new NotImplementedException();
        }

        public string[] GetPosition()
        {
            throw new NotImplementedException();
        }
    }
}

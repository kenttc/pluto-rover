﻿using System;
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
        public void given_rover_location_and_send_single_command_when_get_position_called_will_report_current_position()
        {
            MoveRoverForwardsInDifferentDirectionTests();

            MoveRoverBackwardsInDifferentDirectionTests();

            TurnRoverLeftInWhenInDifferentDirectionsTests();

            TurnRoverRightInWhenInDifferentDirectionsTests();

            

        }
        [TestMethod]
        public void given_rover_location_and_send_multiple_command_when_get_position_called_will_report_current_position()
        {
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "N" },
                "FF", new string[] { "0", "2", "N" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "E" },
                "FF", new string[] { "2", "0", "E" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "2", "0", "W" },
                "FF", new string[] { "0", "0", "W" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "2", "S" },
                "FF", new string[] { "0", "0", "S" });


            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "2", "N" },
                "BB", new string[] { "0", "0", "N" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "2", "0", "E" },
                "BB", new string[] { "0", "0", "E" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "W" },
                "BB", new string[] { "2", "0", "W" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "S" },
                "BB", new string[] { "0", "2", "S" });

        }

        private static void MoveRoverForwardsInDifferentDirectionTests()
        {
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "0", "N"},
                "F", new string[] {"0", "1", "N"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "0", "E"},
                "F", new string[] {"1", "0", "E"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"1", "0", "W"},
                "F", new string[] {"0", "0", "W"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "1", "S"},
                "F", new string[] {"0", "0", "S"});
        }

        private static void
            MoveRoverBackwardsInDifferentDirectionTests()
        {
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "1", "N"},
                "B", new string[] {"0", "0", "N"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"1", "0", "E"},
                "B", new string[] {"0", "0", "E"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "0", "W"},
                "B", new string[] {"1", "0", "W"});
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] {"0", "0", "S"},
                "B", new string[] {"0", "1", "S"});
        }
        private static void TurnRoverLeftInWhenInDifferentDirectionsTests()
        {
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "N" },
                "L", new string[] { "0", "0", "W" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "W" },
                "L", new string[] { "0", "0", "S" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "S" },
                "L", new string[] { "0", "0", "E" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "E" },
                "L", new string[] { "0", "0", "N" });
        }
        private static void TurnRoverRightInWhenInDifferentDirectionsTests()
        {
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "N" },
                "R", new string[] { "0", "0", "E" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "E" },
                "R", new string[] { "0", "0", "S" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "S" },
                "R", new string[] { "0", "0", "W" });
            SendCommandToRoverAtPositionAndVerifyAfterMovement(new string[] { "0", "0", "W" },
                "R", new string[] { "0", "0", "N" });
        }
        private static void SendCommandToRoverAtPositionAndVerifyAfterMovement(string[] currentRoverLocation, string move,
            string[] expectedRoverPosition)
        {
            var rover = new Rover(currentRoverLocation);
            rover.SendCommand(move);
            var afterMovingPosition = rover.GetPosition();
            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }
        private static void SetPlanetSizeAndCheckRoverCanGoAround(string[] currentRoverLocation, int[] planetSize, string move,
            string[] expectedRoverPosition)
        {
            var rover = new Rover(currentRoverLocation);
            rover.SetPlanetSize(planetSize);
            rover.SendCommand(move);
            var afterMovingPosition = rover.GetPosition();

            Assert.IsTrue(expectedRoverPosition.SequenceEqual(afterMovingPosition)
                , $"expected {string.Join(",", expectedRoverPosition)} but got {string.Join(",", afterMovingPosition)}");
        }

        [TestMethod]
        public void given_rover_location_has_grid_size_and_command_made_rover_go_around_the_planet_will_show_correct_coordinates()
        {
            SetPlanetSizeAndCheckRoverCanGoAround(new string[] { "0", "0", "S" }, 
                new int[] { 1, 1 }, "F", new string[] { "0", "1", "S" });
            SetPlanetSizeAndCheckRoverCanGoAround(new string[] { "0", "1", "N" },
                new int[] { 1, 1 }, "F", new string[] { "0", "0", "N" });

        }


    }
}


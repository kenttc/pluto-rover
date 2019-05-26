using System;

namespace PlutoRoverTests
{
    public class Rover
    {
        private int _xCoordinate;
        private int _yCoordinate;
        private string _heading;


        public Rover(string[] currentRoverLocation)
        {
            _xCoordinate = Convert.ToInt32(currentRoverLocation[0]);
            _yCoordinate = Convert.ToInt32(currentRoverLocation[1]);
            _heading = currentRoverLocation[2];
            
        }

        public string[] GetPosition()
        {
            return new string[]{_xCoordinate.ToString(), _yCoordinate.ToString(), _heading};
        }

        public void SendCommand(string move)
        {

            
            if (IsMoveForwardCommand(move))
                if (IsRoverFacingEast())
                    _xCoordinate++;
                else if (IsRoverFacingWest())
                    _xCoordinate--;
                else
                    _yCoordinate++;
                
            if (IsMoveBackwardsCommand(move))
                _yCoordinate--;

            if (IsTurnRightCommand(move))
                _heading = "E";

            if(isTurnLeftCommand(move))
                _heading = "W";

        }

        private bool IsRoverFacingWest()
        {
            return _heading == "W";
        }

        private bool IsRoverFacingEast()
        {
            return _heading == "E";
        }

        private static bool isTurnLeftCommand(string move)
        {
            return move == "L";
        }

        private static bool IsTurnRightCommand(string move)
        {
            return move == "R";
        }

        private static bool IsMoveBackwardsCommand(string move)
        {
            return move == "B";
        }

        private static bool IsMoveForwardCommand(string move)
        {
            return move == "F";
        }
    }
}
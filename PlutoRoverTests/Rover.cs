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
            return new string[] {_xCoordinate.ToString(), _yCoordinate.ToString(), _heading};
        }

        public void SendCommand(string move)
        {
            if (IsMoveForwardCommand(move))
                if (IsRoverFacingEast())
                    MoveEast();
                else if (IsRoverFacingWest())
                    MoveWest();
                else if (IsRoverFacingNorth())
                    MoveNorth();
                else
                    MoveSouth();

            if (IsMoveBackwardsCommand(move))
                if (IsRoverFacingEast())
                    MoveWest();
                else if (IsRoverFacingWest())
                    MoveEast();
                else if (IsRoverFacingNorth())
                    MoveSouth();
                else
                    MoveNorth();


            if (IsTurnRightCommand(move))
                _heading = "E";

            if (isTurnLeftCommand(move))
                if (IsRoverFacingNorth())
                    _heading = "W";
                else if (IsRoverFacingWest())
                    _heading = "S";
        }

        private bool IsRoverFacingNorth()
        {
            return _heading == "N";
        }

        private int MoveSouth()
        {
            return _yCoordinate--;
        }

        private int MoveNorth()
        {
            return _yCoordinate++;
        }

        private int MoveEast()
        {
            return _xCoordinate++;
        }

        private int MoveWest()
        {
            return _xCoordinate--;
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
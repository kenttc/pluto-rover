﻿using System;

namespace PlutoRoverTests
{
    public class Rover
    {
        private int _xCoordinate;
        private int _yCoordinate;
        private string _heading;
        private int _planetXBoundary;
        private int _planetYBoundary;


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
            var commandIndex = 0;
            while (move.Length > commandIndex)
            {
                var currentMoveCommand = move[commandIndex].ToString();
                if (IsMoveForwardCommand(currentMoveCommand))
                    if (IsRoverFacingEast())
                        MoveEast();
                    else if (IsRoverFacingWest())
                        MoveWest();
                    else if (IsRoverFacingNorth())
                        MoveNorth();
                    else if(IsRoverFacingSouth())
                        MoveSouth();

                if (IsMoveBackwardsCommand(currentMoveCommand))
                    if (IsRoverFacingEast())
                        MoveWest();
                    else if (IsRoverFacingWest())
                        MoveEast();
                    else if (IsRoverFacingNorth())
                        MoveSouth();
                    else
                        MoveNorth();


                if (IsTurnRightCommand(currentMoveCommand))
                    if (IsRoverFacingNorth())
                        SetRoverFacingEast();
                    else if (IsRoverFacingEast())
                        SetRoverFacingSouth();
                    else if (IsRoverFacingSouth())
                        SetRoverFacingWest();
                    else if (IsRoverFacingWest())
                        SetRoverFacingNorth();

                if (isTurnLeftCommand(currentMoveCommand))
                    if (IsRoverFacingNorth())
                        SetRoverFacingWest();
                    else if (IsRoverFacingWest())
                        SetRoverFacingSouth();
                    else if (IsRoverFacingSouth())
                        SetRoverFacingEast();
                    else
                        SetRoverFacingNorth();

                commandIndex++;
            }
        }

        private void SetRoverFacingWest()
        {
            _heading = "W";
        }

        private void SetRoverFacingSouth()
        {
            _heading = "S";
        }

        private void SetRoverFacingEast()
        {
            _heading = "E";
        }

        private string SetRoverFacingNorth()
        {
            return _heading = "N";
        }

        private bool IsRoverFacingSouth()
        {
            return _heading == "S";
        }

        private bool IsRoverFacingNorth()
        {
            return _heading == "N";
        }

        private void MoveSouth()
        {
            if (_yCoordinate - 1 < 0 && _planetYBoundary > 0)
            {
                _yCoordinate = _planetYBoundary;
            }
            else
            {
                _yCoordinate--;
            }
            
        }

        private void MoveNorth()
        {
            if (_yCoordinate + 1 > _planetYBoundary && _planetYBoundary > 0)
            {
                _yCoordinate = 0;
            }
            else
            {
                _yCoordinate++;
            }
            
        }

        private void MoveEast()
        {
            _xCoordinate++;
        }

        private void MoveWest()
        {
            _xCoordinate--;
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

        public void SetPlanetSize(int[] planetSize)
        {
            _planetXBoundary = planetSize[0];
            _planetYBoundary = planetSize[1];
        }
    }
}
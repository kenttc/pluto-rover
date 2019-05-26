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
            _heading = currentRoverLocation[1];
            
        }

        public string[] GetPosition()
        {
            return new string[]{_xCoordinate.ToString(), _yCoordinate.ToString(), _heading};
        }

        public void SendCommand(string move)
        {
            throw new System.NotImplementedException();
        }
    }
}
namespace PlutoRoverTests
{
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
}
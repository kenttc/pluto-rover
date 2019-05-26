namespace PlutoRoverTests
{
    public class Rover
    {
        private readonly string[] _currentRoverLocation;

        public Rover(string[] currentRoverLocation)
        {
            _currentRoverLocation = currentRoverLocation;
        }

        public string[] GetPosition()
        {
            return _currentRoverLocation;
        }

        public void SendCommand(string move)
        {
            throw new System.NotImplementedException();
        }
    }
}
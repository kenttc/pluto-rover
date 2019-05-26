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
    }
}
namespace PlutoRoverTests
{
    public abstract class MoverBase
    {
        protected string[] _currentRoverLocation;
        protected string _move;

        protected MoverBase(string[] currentRoverLocation, string move)
        {
            _currentRoverLocation = currentRoverLocation;
            _move = move;
        }

        public abstract string[] ExecuteAndReturnStatus();
    }
}
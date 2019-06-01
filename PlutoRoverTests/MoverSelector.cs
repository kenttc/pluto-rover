namespace PlutoRoverTests
{
    public class MoverSelector
    {
        private readonly string _currentMove;

        public MoverSelector(string currentMove)
        {
            _currentMove = currentMove;
        }

        public MoverBase Get(string[] currentRoverLocation)
        {
            if(_currentMove == "R" || _currentMove == "L")
                return new TurnMover(currentRoverLocation, _currentMove);
                 
            return new PlaneMover(currentRoverLocation, _currentMove);
        }
    }
}
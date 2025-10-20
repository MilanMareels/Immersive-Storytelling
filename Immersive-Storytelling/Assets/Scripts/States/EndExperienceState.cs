public class EndExperienceState : State
{
    public EndExperienceState(DirectorScript director, State nextState) : base(director, nextState)
    {
    }

    public void SetNextState(State state)
    {
        _nextState = state;
    }
}

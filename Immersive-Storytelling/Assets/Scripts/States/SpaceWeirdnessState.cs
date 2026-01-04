public class SpaceWeirdnessState : State
{
    public SpaceWeirdnessState(DirectorScript director, State nextState) : base(director, nextState)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // Immediately transition to the next state
        Transition();
    }
}

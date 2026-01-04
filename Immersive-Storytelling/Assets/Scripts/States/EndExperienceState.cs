public class EndExperienceState : State
{
    private float _delta = 0f;
    public EndExperienceState(DirectorScript director, State nextState) : base(director, nextState)
    {
    }

    public void SetNextState(State state)
    {
        _nextState = state;
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _delta += UnityEngine.Time.deltaTime;
        if (_delta > 10f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

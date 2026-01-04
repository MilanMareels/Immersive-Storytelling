using UnityEngine;

public class SpaceWeirdnessState : State
{
    private float _delta = 0f;

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
        _delta += Time.deltaTime;
        if (_delta > 42f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

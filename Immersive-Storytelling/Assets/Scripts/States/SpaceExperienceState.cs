using UnityEngine;

public class SpaceExperienceState : State
{
    private float _delta = 0f;

    public SpaceExperienceState(DirectorScript director, State nextState) : base(director, nextState)
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
        if (_delta > 13f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

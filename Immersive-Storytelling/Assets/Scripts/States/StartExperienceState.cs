using System.Collections;
using UnityEngine;

public class StartExperienceState : State
{
    private float _delta = 0f;

    public StartExperienceState(DirectorScript director, State nextState) : base(director, nextState)
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

        if (_delta > 5f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

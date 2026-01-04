using UnityEngine;

public class TimeLapseState : State
{
    private float _delta = 0f;

    public TimeLapseState(DirectorScript director, State nextState) : base(director, nextState)
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

        if (_delta > 45f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

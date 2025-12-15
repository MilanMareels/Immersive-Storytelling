using UnityEngine;

public class TimeLapseState : State
{
    private float _delta;

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
    }
}

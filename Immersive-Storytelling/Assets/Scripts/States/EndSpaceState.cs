using UnityEngine;

public class EndSpaceState : State
{
    private float _delta = 0f;

    public EndSpaceState(DirectorScript director, State nextState) : base(director, nextState)
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
        if (_delta > 25f)
        {
            Transition();
            _delta = 0f;
        }
    }
}

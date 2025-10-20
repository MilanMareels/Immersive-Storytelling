public class InitialState : State
{
    private bool _buttonPressed = false;
    public InitialState(DirectorScript director) : base(director)
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_buttonPressed)
        {
            director.SetState(director.DayNightState);
            _buttonPressed = false;
        }
    }

    public void OnButtonPress()
    {
        _buttonPressed = true;
    }
}

public abstract class State
{
    public delegate void EntryDelegate();
    public EntryDelegate Entry;
    public delegate void ExitDelegate();
    public ExitDelegate Exit;
    public delegate void UpdateDelegate();
    public UpdateDelegate Update;

    protected DirectorScript _director;
    protected bool _transition = false;
    protected State _nextState;

    protected State(DirectorScript director, State nextState)
    {
        _director = director;
        _nextState = nextState;
    }

    public virtual void OnEntry()
    {
        Entry?.Invoke();
    }

    public virtual void OnExit()
    {
        Exit?.Invoke();
    }

    public virtual void OnUpdate()
    {
        Update?.Invoke();

        if (_transition)
        {
            _director.SetState(_nextState);
            _transition = false;
        }
    }

    public void Transition()
    {
        _transition = true;
    }
}

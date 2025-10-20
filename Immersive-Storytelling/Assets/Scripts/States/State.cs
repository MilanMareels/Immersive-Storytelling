using UnityEngine;

public abstract class State
{
    public delegate void EntryDelegate();
    public EntryDelegate Entry;
    public delegate void ExitDelegate();
    public ExitDelegate Exit;
    public delegate void UpdateDelegate();
    public UpdateDelegate Update;

    protected DirectorScript director;

    protected State(DirectorScript director)
    {
        this.director = director;
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
    }
}

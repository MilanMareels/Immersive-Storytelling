using System.Collections;
using UnityEditor;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{
    public delegate void CycleDelegate();
    public CycleDelegate cycleDelegate;
    public delegate void BreakFloorDelegate();
    public BreakFloorDelegate breakFloorDelegate;
    public State InitialState;
    public State DayNightState;
    public State StartExperienceState;
    public State TimeLapseState;
    public State SpaceExperienceState;
    public State SpaceWeirdnessState;
    public State EndSpaceState;
    public State EndExperienceState;

    private State _currentState;
    private State _prevState;

    public DirectorScript()
    {
        EndExperienceState = new EndExperienceState(this, InitialState);
        EndSpaceState = new EndSpaceState(this, EndExperienceState);
        SpaceWeirdnessState = new SpaceWeirdnessState(this, EndSpaceState);
        SpaceExperienceState = new SpaceExperienceState(this, SpaceWeirdnessState);
        TimeLapseState = new TimeLapseState(this, SpaceExperienceState);
        StartExperienceState = new StartExperienceState(this, TimeLapseState);
        InitialState = new InitialState(this, StartExperienceState);
        (EndExperienceState as EndExperienceState).SetNextState(InitialState);
    }
         
    private void Start()
    {
        _currentState = InitialState;
        InitialState.Entry += () => Debug.Log("State 1");
    }

    private void Update()
    {
        if (_prevState is null)
        {
            _currentState.OnEntry();
            _prevState = _currentState;
        }
        else if (_prevState != _currentState)
        {
            _prevState.OnExit();
            _currentState.OnEntry();
            _prevState = _currentState;
        }
        else
        {
            _currentState.OnUpdate();
        }
        Debug.Log(_currentState.GetType());
    }

    public void SetState(State state)
    {
        _currentState = state;
    }

    public void NextState()
    {
        _currentState.Transition();
    }

    public void ResetDirector()
    {
        _currentState = InitialState;
        _prevState = null;
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(5);
        breakFloorDelegate();
    }
}

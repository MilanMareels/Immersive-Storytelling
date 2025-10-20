using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class DirectorScript : MonoBehaviour
{
    public delegate void CycleDelegate();
    public CycleDelegate cycleDelegate;
    public delegate void BreakFloorDelegate();
    public BreakFloorDelegate breakFloorDelegate;
    public State InitialState;
    public State DayNightState;

    private State _currentState;
    private State _prevState;

    public DirectorScript()
    {
        InitialState = new InitialState(this);
        DayNightState = new DayNightState(this);
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

using System.Collections;
using UnityEditor;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{
    public delegate void CycleDelegate();
    public CycleDelegate cycleDelegate;
    public delegate void BreakFloorDelegate();
    public BreakFloorDelegate breakFloorDelegate;
    public InitialState InitialState;
    public State DayNightState;
    public StartExperienceState StartExperienceState;
    public TimeLapseState TimeLapseState;
    public SpaceExperienceState SpaceExperienceState;
    public SpaceWeirdnessState SpaceWeirdnessState;
    public EndSpaceState EndSpaceState;
    public EndExperienceState EndExperienceState;

    private State _currentState;
    private State _prevState;

    private SoundEffectManager SoundEffectManager;
    private DayNightCycle DayNightCycle;
    private AsteroidSpawner AsteroidSpawner;
    private FloorScript FloorScript;
    private ObjectFallScript ObjectFallScript;
    public void Awake()
    {
        SoundEffectManager = FindFirstObjectByType<SoundEffectManager>();
        DayNightCycle = FindFirstObjectByType<DayNightCycle>();
        AsteroidSpawner = FindFirstObjectByType<AsteroidSpawner>();
        FloorScript = FindFirstObjectByType<FloorScript>();
        ObjectFallScript = FindFirstObjectByType<ObjectFallScript>();

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

        _currentState.Entry += () => Debug.Log($"{_currentState}");

        InitialState.Entry += () => SoundEffectManager.PlaySong("Nature", 1f, true);
        
        TimeLapseState.Entry += () => DayNightCycle.StartSpeedUp();
        TimeLapseState.Entry += () => SoundEffectManager.PlaySong("DayNight", 0.15f, true);
        TimeLapseState.Entry += () => SoundEffectManager.PlayVoice("DayNightVoice", 0.35f);

        SpaceExperienceState.Entry += () => breakFloorDelegate?.Invoke();
        SpaceExperienceState.Entry += () => FloorScript.StartBreak();
        SpaceExperienceState.Entry += () => ObjectFallScript.StartFall();
        SpaceExperienceState.Entry += () => SoundEffectManager.PlaySong("Space", 0.1f, true);
        SpaceExperienceState.Entry += () => SoundEffectManager.PlayVoice("SpaceVoice", 0.35f);
        SpaceExperienceState.Entry += () => AsteroidSpawner.SpawnAsteroid();

        EndSpaceState.Entry += () => FloorScript.ReverseBreak();
        EndSpaceState.Entry += () => ObjectFallScript.ReverseFall();
        EndSpaceState.Entry += () => SoundEffectManager.PlayVoice("LoopVoice", 0.2f, true);
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
        //Debug.Log(_currentState.GetType());
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

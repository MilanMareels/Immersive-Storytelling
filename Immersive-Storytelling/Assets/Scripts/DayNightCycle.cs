using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float DurationDayInitial = 10;
    public float DurationDayFFWD = 2;
    public float TransitionTime = 10;
    private float rotationSpeed;
    public Gradient AmbientColors;
    public Material DaySkyboxMaterial;
    public Material NightSkyboxMaterial;
    private Light sun;
    private bool isDay;

    private CycleState state = CycleState.Slow;
    private float cycleTransitionDelta = 0f;

    private enum CycleState
    {
        Slow,
        Fast,
        SpeedUp,
        SlowDown
    }


    void Start()
    {
        rotationSpeed = 360f / DurationDayInitial;
        sun = GetComponent<Light>();
        isDay = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CycleState.Slow:
                rotationSpeed = 360f / DurationDayInitial; break;
            case CycleState.Fast:
                rotationSpeed = 360f / DurationDayFFWD; break;
            case CycleState.SpeedUp:
                cycleTransitionDelta += Time.deltaTime;
                if (cycleTransitionDelta > TransitionTime)
                {
                    state = CycleState.Fast;
                    cycleTransitionDelta = 0f;
                    rotationSpeed = 360f / DurationDayFFWD;
                    break;
                }
                rotationSpeed = Mathf.Lerp(360f / DurationDayInitial, 360f / DurationDayFFWD, cycleTransitionDelta / TransitionTime);
                break;
            case CycleState.SlowDown:
                cycleTransitionDelta += Time.deltaTime;
                if (cycleTransitionDelta > TransitionTime)
                {
                    state = CycleState.Slow;
                    cycleTransitionDelta = 0f;
                    rotationSpeed = 360f / DurationDayInitial;
                    break;
                }
                rotationSpeed = Mathf.Lerp(360f / DurationDayFFWD, 360f / DurationDayInitial, cycleTransitionDelta / TransitionTime);
                break;
        }


        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        isDay = transform.eulerAngles.x < 180f && transform.eulerAngles.x > 0f;

        RenderSettings.skybox = isDay ? DaySkyboxMaterial : NightSkyboxMaterial;


        float timeFactor = isDay ? Mathf.InverseLerp(0, 180, transform.eulerAngles.x) * 1.5f : 0;
        sun.intensity = timeFactor;
        RenderSettings.reflectionIntensity = timeFactor;
    }

    public void StartSpeedUp()
    {
        if (state != CycleState.Slow)
        {
            Debug.LogError("SpeedUp was called but cycle was not Slow at this time");
            return;
        }
        state = CycleState.SpeedUp;
    }

    public void StartSlowDown()
    {
        if (state != CycleState.Fast)
        {
            Debug.LogError("SlowDown was called but cycle was not Fast at this time");
            return;
        }
        state = CycleState.SlowDown;
    }
}


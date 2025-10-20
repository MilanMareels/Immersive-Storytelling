using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float DurationDayInitial = 10;
    private float rotationSpeed;
    public Gradient AmbientColors;
    public Material DaySkyboxMaterial;
    public Material NightSkyboxMaterial;
    private Light sun;
    private bool isDay;
    private bool updateSkybox;

    void Start()
    {
        rotationSpeed = 360f / DurationDayInitial;
        sun = GetComponent<Light>();
        isDay = true;
        updateSkybox = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        isDay = transform.eulerAngles.x < 180f && transform.eulerAngles.x > 0f;

        RenderSettings.skybox = isDay ? DaySkyboxMaterial : NightSkyboxMaterial;


        float timeFactor = isDay ? Mathf.InverseLerp(0, 180, transform.eulerAngles.x) * 1.5f : 0;
        sun.intensity = timeFactor;
        Debug.Log($"{sun.intensity}: {transform.eulerAngles.x}: {isDay}");
        RenderSettings.reflectionIntensity = timeFactor;
    }
}


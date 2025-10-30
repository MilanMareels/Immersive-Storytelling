using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Beweging")]
    public float speed = 5f;
    public Vector2 direction = new Vector2(-1, -1);
    public float smoothness = 0.5f;

    [Header("Rotatie & Slingering")]
    public float rotationSpeed = 50f;
    public float swayAmount = 0.5f;
    public float swaySpeed = 2f;

    [Header("Geluid")]
    public AudioClip fallSound;
    private AudioSource audioSource;

    private Vector2 velocity;
    private float swayOffset;
    private Camera mainCam;

    void Start()
    {
        direction = direction.normalized;
        swayOffset = Random.Range(0f, 2f * Mathf.PI);
        mainCam = Camera.main;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (fallSound != null)
        {
            audioSource.clip = fallSound;
            audioSource.Play();
        }
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed + swayOffset) * swayAmount;
        Vector2 swayDir = new Vector2(direction.x + sway, direction.y).normalized;

        Vector2 targetVelocity = swayDir * speed;
        velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref velocity, smoothness);

        transform.Translate(velocity * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}

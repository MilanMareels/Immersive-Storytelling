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

    private Vector2 velocity;
    private float swayOffset;

    void Start()
    {
        direction = direction.normalized;
        swayOffset = Random.Range(0f, 2f * Mathf.PI);
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

using System.Collections;
using UnityEngine;

public class FloorMotion : MonoBehaviour
{
    public float minSpeed = 0.2f;  // minimale scrollsnelheid
    public float maxSpeed = 1.0f;  // maximale scrollsnelheid
    public float changeIntervalMin = 1.0f; // minimale tijd voordat richting verandert
    public float changeIntervalMax = 3.0f; // maximale tijd

    private Renderer rend;
    private Vector2 scrollSpeed;

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        // Update texture offset elke frame
        Vector2 offset = new Vector2(Time.time * scrollSpeed.x, Time.time * scrollSpeed.y);
        rend.material.mainTextureOffset = offset;
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            // Kies nieuwe random snelheid en richting
            float speedX = Random.Range(minSpeed, maxSpeed) * (Random.value > 0.5f ? 1 : -1);
            float speedY = Random.Range(minSpeed, maxSpeed) * (Random.value > 0.5f ? 1 : -1);

            scrollSpeed = new Vector2(speedX, speedY);

            // Wacht random tijd voordat volgende verandering
            float waitTime = Random.Range(changeIntervalMin, changeIntervalMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

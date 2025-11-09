using UnityEngine;

public class HoverStand : MonoBehaviour
{
    public float height = 0.5f;      // maximale hoogte omhoog/omlaag
    public float speed = 1f;         // snelheid van het op- en neergaan

    private Vector3 startPos;

    void Start()
    {
        // beginpositie onthouden
        startPos = transform.position;
    }

    void Update()
    {
        // sinus voor smooth op- en neerbeweging
        float yOffset = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}

using UnityEngine;

public class Wall_Motion : MonoBehaviour
{
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.0f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * scrollSpeedX, Time.time * scrollSpeedY);
        rend.material.mainTextureOffset = offset;
    }
}

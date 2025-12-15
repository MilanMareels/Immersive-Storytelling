using UnityEngine;

public class AnimatieController : MonoBehaviour
{
    public Animator mijnAnimator; // Sleep je animator hierin
    private bool status = false;

    void Update()
    {
        // Voorbeeld: Als je op Spatie drukt, wisselt hij van status
        if (Input.GetKeyDown(KeyCode.Space))
        {
            status = !status; // Wissel true naar false of andersom
            mijnAnimator.SetBool("IsOpen", status);
        }
    }
}

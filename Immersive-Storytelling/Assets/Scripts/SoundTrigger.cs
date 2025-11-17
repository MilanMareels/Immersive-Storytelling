using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
    }
}
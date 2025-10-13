using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour
{
    [Header("Your sound effect")]
    public AudioClip soundClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void PlaySoundEffect(int amountOfTimes, float speed)
    {
        if (soundClip == null) return;
        StartCoroutine(PlayRepeated(speed, amountOfTimes)); // Speed still need to fix.
    }

    private IEnumerator PlayRepeated(float speed,int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            audioSource.PlayOneShot(soundClip);
            yield return new WaitForSeconds(soundClip.length);
        }
    }
}

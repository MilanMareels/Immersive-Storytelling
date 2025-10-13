using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

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
        StartCoroutine(PlayRepeated(amountOfTimes,speed));
    }

    private IEnumerator PlayRepeated(int amount = 1, float speed = 1)
    {
        audioSource.PlayOneShot(soundClip);
        audioSource.pitch = speed;
        for (int i = 0; i < amount; i++)
        {
            audioSource.PlayOneShot(soundClip);
            yield return new WaitForSeconds(soundClip.length);
        }
    }
}

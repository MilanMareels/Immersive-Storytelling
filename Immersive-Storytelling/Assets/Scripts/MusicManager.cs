using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private string[] musicKeys = new string[0];
    [SerializeField] private AudioClip[] musicClips = new AudioClip[0];

    public Dictionary<string, AudioClip> MusicSources;
    private AudioSource musicPlayer;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        BuildDictionaryFromArrays();
    }

    private void Update()
    {
        
    }

    public void PlaySong(string name)
    {
        if (MusicSources == null || !MusicSources.ContainsKey(name))
        {
            Debug.LogError("Music clip not found");
            return;
        }
        musicPlayer.clip = MusicSources[name];
        musicPlayer.Play();
    }

    public void Stop()
    {
        musicPlayer.Stop();
    }

    private void BuildDictionaryFromArrays()
    {
        MusicSources = new Dictionary<string, AudioClip>(musicKeys.Length);
        int count = Mathf.Min(musicKeys.Length, musicClips.Length);

        if (musicKeys.Length != musicClips.Length)
            Debug.LogWarning("musicKeys and musicClips length mismatch. Extra items will be ignored.");

        for (int i = 0; i < count; i++)
        {
            var key = musicKeys[i];
            var clip = musicClips[i];
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"Empty key at index {i} ignored.");
                continue;
            }
            if (clip == null)
            {
                Debug.LogWarning($"Null clip for key '{key}' at index {i} ignored.");
                continue;
            }
            if (MusicSources.ContainsKey(key))
            {
                Debug.LogWarning($"Duplicate key '{key}' at index {i} ignored.");
                continue;
            }

            MusicSources.Add(key, clip);
        }
    }
}

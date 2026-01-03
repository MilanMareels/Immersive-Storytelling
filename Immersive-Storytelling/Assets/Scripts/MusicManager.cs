using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private string[] musicKeys = new string[0];
    [SerializeField] private AudioClip[] musicClips = new AudioClip[0];

    public Dictionary<string, AudioClip> MusicSources;
    private AudioSource musicPlayer;
    private AudioSource musicPlayer2;

    private void Awake()
    {
        var sources = GetComponents<AudioSource>();
        if (sources.Length == 0)
            musicPlayer = gameObject.AddComponent<AudioSource>();
        else
            musicPlayer = sources[0];

        if (sources.Length >= 2)
            musicPlayer2 = sources[1];
        else
            musicPlayer2 = gameObject.AddComponent<AudioSource>();

        musicPlayer.playOnAwake = false;
        musicPlayer.loop = false;

        musicPlayer2.playOnAwake = false;
        musicPlayer2.loop = true;

        BuildDictionaryFromArrays();
    }

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public void PlaySong(string music, float volume1 = 1f, string music2 = "", float volume2 = 1f)
    {
        if (MusicSources == null || !MusicSources.ContainsKey(music))
        {
            Debug.LogError("First music clip not found");
            return;
        }
        musicPlayer.clip = MusicSources[music];
        musicPlayer.volume = volume1;
        musicPlayer.Play();
        if (music2 == "")
        {
            return;
        }
        else if (!MusicSources.ContainsKey(music2))
        {
            Debug.LogError("Second music clip not found");
            return;
        }
        musicPlayer2.clip = MusicSources[music2];
        musicPlayer2.volume = (float)volume2;
        musicPlayer2.Play();
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

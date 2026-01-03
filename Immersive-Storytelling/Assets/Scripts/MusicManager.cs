using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private string[] musicKeys = new string[0];
    [SerializeField] private AudioClip[] musicClips = new AudioClip[0];

    public Dictionary<string, AudioClip> MusicSources;
    private AudioSource musicPlayer;
    private AudioSource sfxPlayer;

    private void Awake()
    {
        var sources = GetComponents<AudioSource>();
        if (sources.Length == 0)
            musicPlayer = gameObject.AddComponent<AudioSource>();
        else
            musicPlayer = sources[0];

        if (sources.Length >= 2)
            sfxPlayer = sources[1];
        else
            sfxPlayer = gameObject.AddComponent<AudioSource>();

        musicPlayer.playOnAwake = false;
        musicPlayer.loop = false;

        sfxPlayer.playOnAwake = false;
        sfxPlayer.loop = true;

        BuildDictionaryFromArrays();
    }

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public void PlaySong(string name, float volume, string? name2, float? sfxVolume)
    {
        if (MusicSources == null || !MusicSources.ContainsKey(name))
        {
            Debug.LogError("Music clip not found");
            return;
        }
        musicPlayer.clip = MusicSources[name];
        musicPlayer.volume = volume;
        musicPlayer.Play();
        if (name2 != null && MusicSources.ContainsKey(name2) && sfxVolume != null)
        {
            sfxPlayer.clip = MusicSources[name2];
            sfxPlayer.volume = (float)sfxVolume;
            sfxPlayer.Play();
        }
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

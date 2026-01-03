using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private string[] soundKeys = new string[0];
    [SerializeField] private AudioClip[] soundClips = new AudioClip[0];

    public Dictionary<string, AudioClip> SoundSources;
    private AudioSource musicPlayer;
    private AudioSource voicePlayer;

    private void Awake()
    {
        var sources = GetComponents<AudioSource>();
        if (sources.Length == 0)
            musicPlayer = gameObject.AddComponent<AudioSource>();
        else
            musicPlayer = sources[0];

        if (sources.Length >= 2)
            voicePlayer = sources[1];
        else
            voicePlayer = gameObject.AddComponent<AudioSource>();

        musicPlayer.playOnAwake = false;

        voicePlayer.playOnAwake = false;

        BuildDictionaryFromArrays();
    }

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public void PlaySong(
        string name,
        float volume,
        bool loop = false)
    {
        if (SoundSources == null || !SoundSources.ContainsKey(name))
        {
            Debug.LogError("Music clip not found");
            return;
        }
        musicPlayer.clip = SoundSources[name];
        musicPlayer.volume = volume;
        musicPlayer.loop = loop;
        musicPlayer.Play();
        
    }

    public void PlayVoice(
        string name,
        float volume,
        bool loop = false)
    {
        if (SoundSources == null || !SoundSources.ContainsKey(name))
        {
            Debug.LogError("Voice clip not found");
            return;
        }
        voicePlayer.clip = SoundSources[name];
        voicePlayer.volume = volume;
        voicePlayer.loop = loop;
        voicePlayer.Play();
    }

    public void Stop()
    {
        musicPlayer.Stop();
    }

    private void BuildDictionaryFromArrays()
    {
        SoundSources = new Dictionary<string, AudioClip>(soundKeys.Length);
        int count = Mathf.Min(soundKeys.Length, soundClips.Length);

        if (soundKeys.Length != soundClips.Length)
            Debug.LogWarning("soundKeys and soundClips length mismatch. Extra items will be ignored.");

        for (int i = 0; i < count; i++)
        {
            var key = soundKeys[i];
            var clip = soundClips[i];
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
            if (SoundSources.ContainsKey(key))
            {
                Debug.LogWarning($"Duplicate key '{key}' at index {i} ignored.");
                continue;
            }

            SoundSources.Add(key, clip);
        }
    }
}

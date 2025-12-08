using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> MusicSources;
    private AudioSource musicPlayer;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void PlaySong(string name)
    {
        if (!MusicSources.ContainsKey(name))
            Debug.LogError("Music clip not found");
        musicPlayer.clip = MusicSources[name];
        musicPlayer.Play();
    }

    public void Stop()
    {
        musicPlayer.Stop();
    }
}

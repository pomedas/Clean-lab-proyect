using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public enum SoundName
    {
        MainMusic,
        LoungeMusic,
        StartLevel,
        EnterHarbor,
        FishingAmbient,
        HarborAmbient,
        StartAmbient,
        RedBoatNoise,
        BlueBoatNoise,
        CollectFish,
        CollectTrash,
        NetRip,
        Select
    }

    [SerializeField] private Sound[] GlobalSounds;

    private List<Sound> PausedSounds = new List<Sound>();

    void Awake()
    {
        SetGlobalAudioSources();
    }

    public void Play(SoundName name)
    {
        Sound s = FindSound(name);
        s?.source.Play();
        Debug.Log("[ AUDIO ] Playing " + name);
    }

    public void PlayOnce(SoundName name)
    {
        Sound s = FindSound(name);

        if (s != null && !s.source.isPlaying)
        {
            s.source.Play();
            Debug.Log("[ AUDIO ] Playing once " + name);
        }
    }

    public void Stop(SoundName name)
    {
        Sound s = FindSound(name);
        s?.source.Stop();
    }

    public void PauseIfPlaying(SoundName name)
    {
        Sound s = FindSound(name);

        if (s != null && s.source.isPlaying)
        {
            s.source.Pause();
            PausedSounds.Add(s);
        }
    }

    public void ResumeIfPaused(SoundName name)
    {
        Sound s = FindSound(name);

        if (s != null && PausedSounds.Contains(s))
        {
            s.source.Play();
            PausedSounds.Remove(s);
        }
    }

    private void SetGlobalAudioSources()
    {
        foreach (Sound s in GlobalSounds)
        {
            if (s.source == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }          
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private Sound FindSound(SoundName name)
    {
        Sound s = System.Array.Find(GlobalSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound '" + name + "' does not exist in GlobalSounds Array!");
            return null;
        }
        return s;
    }
}

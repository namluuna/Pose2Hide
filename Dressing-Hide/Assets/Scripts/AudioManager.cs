using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] Musics, Soundfxs;
    public AudioSource musicSource, SoundfxSource   ;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void playSFX(string name)
    {
        Sound s = Array.Find(Soundfxs, x => x.name == name);

        if (s != null)
        {
            SoundfxSource.PlayOneShot(s.clip);
        }
        else
        {
        }
    }

    public void playSFX(AudioClip clip)
    {
        SoundfxSource.PlayOneShot(clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        SoundfxSource.mute = !SoundfxSource.mute;
    }

    public void MusicVolum(float volumn)
    {
        musicSource.volume = volumn;
    }


    public void SoundFxVolum(float volumn)
    {
        SoundfxSource.volume = volumn;
    }   

    public void playMusic()
    {
        if (Musics.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(1, Musics.Length);
            AudioClip musicClip = Musics[randomIndex].clip;
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }

    public void playMainMusic()
    {
        AudioClip musicClip = Musics[0].clip;
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}

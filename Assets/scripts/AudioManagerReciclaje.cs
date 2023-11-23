using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generic script for music

public class AudioManagerReciclaje : MonoBehaviour
{
    public static AudioManagerReciclaje instance;
    public SoundReciclaje[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    //singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //initial music
    private void Start()
    {
        PlayMusicPanelInicio();
    }

    //gameplay music
    public void PlayMusicGame()
    {
        PlayMusic("game");
    }

    //music panelInicio
    public void PlayMusicPanelInicio()
    {
        PlayMusic("panelInicio");
    }

    
    public void PlayMusic(string name)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundReciclaje s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundReciclaje s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            //bajamos volumen de musica normal
            musicSource.volume = 0.2f;
            sfxSource.PlayOneShot(s.clip);
            Invoke("PonerVolumenNormal", 1.5f);
        }
    }

    public void PlaySFXDuracion(string name, float duracion)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundReciclaje s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            //bajamos volumen de musica normal
            musicSource.volume = 0.2f;
            sfxSource.PlayOneShot(s.clip);
            Invoke("PonerVolumenNormal", duracion);
        }
    }

   
    //stop vfx music
    public void StopSFX()
    {
        sfxSource.Stop();
    }


    //sonidos de canvas como botones se llaman desde aqui
    public void PulsarBotonSound()
    {
        //sonido pala golpe al acabar animacion
        PlaySFX("clickButton");
    }

   
}

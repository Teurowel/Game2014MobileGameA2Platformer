//SourceFileName : SoundManager.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles sound manager
//Revision History : Nov.14, 2020 Created, Added list of sounds
//                                         Added play function that plays sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] listOfSounds;

    private void Awake()
    {
        //Make sure there is only one instance of SoundManager;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Make it persist through levels
        DontDestroyOnLoad(gameObject);

        //Create AudioSource for each sound
        foreach (Sound s in listOfSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound foundSound = Array.Find(listOfSounds, sound => sound.name == name);
        if (foundSound != null)
        {
            foundSound.source.PlayOneShot(foundSound.clip);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaestroAudio : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip[] clips;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    public void PlayTalk()
    {
        aud.clip = clips[new System.Random().Next(0, clips.Length - 1)];
        aud.Play();
    }
}

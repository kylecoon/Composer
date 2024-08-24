using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private char currMode = 'c';
    private AudioSource aud;
    private Dictionary<char, AudioClip> noteDictionary = new Dictionary<char, AudioClip>();
    public AudioClip A;
    public AudioClip B;
    public AudioClip C;
    public AudioClip D;
    public AudioClip E;
    public AudioClip F;
    public AudioClip G;
    public AudioClip H;
    public AudioClip I;
    public AudioClip J;
    public AudioClip K;
    private float beatInterval = 0.3f;
    public AudioClip[] songs;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ButtonEvent>(HandleButtonPress);
        aud = GetComponent<AudioSource>();
        noteDictionary.Add('A', A);
        noteDictionary.Add('B', B);
        noteDictionary.Add('C', C);
        noteDictionary.Add('D', D);
        noteDictionary.Add('E', E);
        noteDictionary.Add('F', F);
        noteDictionary.Add('G', G);
        noteDictionary.Add('H', H);
        noteDictionary.Add('I', I);
        noteDictionary.Add('J', J);
        noteDictionary.Add('K', K);
        aud.clip = songs[new System.Random().Next(0, songs.Length - 1)];
        aud.Play();
    }
    private void HandleButtonPress(ButtonEvent e)
    {
        if (e.mode == 'c')
        {
            aud.Stop();
            aud.clip = songs[new System.Random().Next(0, songs.Length - 1)];
            aud.Play();
            currMode = e.mode;
        }

        if (e.mode == 'p')
        {
            currMode = e.mode;
            StartCoroutine(PlayCustomSong());
        }
    }

    private IEnumerator PlayCustomSong()
    {
        aud.Stop();
        string music = GetComponent<MusicMaker>().MakeMusic();

        if (music == "")
        {
            yield break;
        }
        yield return new WaitForSeconds(0.6f);
        int counter = 0;
        while (currMode == 'p')
        {
            if (music[counter] >= 'A' && music[counter] <= 'Z')
            {
                aud.Stop();
                aud.clip = noteDictionary[music[counter]];
                aud.Play();
            }
            else if (music[counter] == '.')
            {
                aud.Stop();
            }
            counter++;
            if (counter >= music.Length)
            {
                counter = 0;
            }
            yield return new WaitForSecondsRealtime(beatInterval);
        }
    }
}

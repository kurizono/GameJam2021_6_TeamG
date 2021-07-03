using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    AudioSource TitleMusic;


    // Update is called once per frame
    void Update()
    {
        TitleMusic.volume = 0.01f * OptionButton.MusicSound;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    
    TimeContoller Timecs;

    [SerializeField]
    AudioClip[] musics;
    [SerializeField]
    AudioClip SE01, SE02;
    [SerializeField]
    AudioSource musicaudio, se;
    
    //シナリオに合わせて音楽を選択
    //音楽を流す時間に合わせて音楽をストップ

    private void Awake()
    {
        Timecs = GetComponent<TimeContoller>();

        musicaudio.loop = true;
        musicaudio.volume = 0.01f * OptionButton.MusicSound;
        se.volume = 0.01f * OptionButton.MusicSound;
        GetMusic();
    }
    private void Start()
    {
        StartMusic();
    }

    //音楽をAssetから変換
    void GetMusic()
    {
        Object[] allmusic_obj;
        allmusic_obj = Resources.LoadAll("music", typeof(AudioClip));
        int music_num = allmusic_obj.Length;
        musics = new AudioClip[music_num];
        for(int i = 0; i < music_num; i++)
        {
            musics[i] = (allmusic_obj[i] as AudioClip);
        }        
    }

    //音楽を取得(RandomとNomusicがある)
    string GetMusicName()
    {
        int Num = StorySelect.scenario_now;
        string musicName = ReadTalk.TalkController.scenarios[Num].music;
        return musicName;
    }
    //音楽の時間を取得
    public float GetMusicTime()
    {
        float musicstoptime = Timecs.Musictime();
        return musicstoptime;
    }

    //音楽を止める
    public void StopMusic()
    {
        musicaudio.Stop();
    }
    //音楽を流す
    public void StartMusic()
    {
        string musicname = GetMusicName();
        if (musicname == "Random")
        {
            int musicnum = Random.Range(0, musics.Length);
            musicaudio.clip = musics[musicnum];
            musicaudio.Play();
        }
        else
        {
            for (int i = 0; i < musics.Length; i++)
            {
                if (musicname == musics[i].name)
                {
                    musicaudio.clip = musics[i];
                    musicaudio.Play();
                }
            }
        }
    }

    public void StartWinSE()
    {
        se.PlayOneShot(SE01);
    }
    public void StartDefeatSE()
    {
        se.PlayOneShot(SE02);
    }
}

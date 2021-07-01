using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    TimeContoller Timecs;

    string music;
    float musictime;

    //シナリオに合わせて音楽を選択
    //音楽を流す時間に合わせて音楽をストップ


    // Start is called before the first frame update
    void Start()
    {
        Timecs = GetComponent<TimeContoller>();
        music = GetMusic();
        musictime = GetMusicTime();
    }

    // Update is called once per frame
    void Update()
    {
        float time = GetTime();

    }

    //音楽を取得
    string GetMusic()
    {
        int Num = StorySelect.scenario_now;
        string musicName = ReadTalk.TalkController.scenarios[Num].music;
        return musicName;
    }
    //音楽の時間を取得
    float GetMusicTime()
    {
        float[] musicstoptime = Timecs.Musictime();
        float musictime = Random.Range(musicstoptime[0], musicstoptime[1]);
        Debug.Log(musicstoptime[0] + "," + musicstoptime[1]);
        return musictime;
    }
    //時間を取得
    float GetTime()
    {
        return Timecs.time;
    }
    //音楽を止める
    void StopMusic()
    {

    }
    //音楽を流す
    void StartMusic()
    {

    }
}

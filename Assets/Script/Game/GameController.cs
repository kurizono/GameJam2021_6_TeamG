using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadText;

//全体のコントローラー
public class GameController : MonoBehaviour
{
    TimeContoller timecs;
    CharaTalk talkcs;
    CharaController characs;
    GameMusic musiccs;

    float musicstoptime;
    float bossstoptime;
    
    // Start is called before the first frame update
    private void Awake()
    {
        timecs = GetComponent<TimeContoller>();
        talkcs = GetComponent<CharaTalk>();
        characs = GetComponent<CharaController>();
        musiccs = GetComponent<GameMusic>();
    }
    void Start()
    {
        //曲が止まる時間を取得
        musicstoptime = musiccs.GetMusicTime();
        //魔王が止まる時刻を取得
        bossstoptime = timecs.Kingtime(musicstoptime);
        //曲を流す
        musiccs.StartMusic();

    }

    // Update is called once per frame
    void Update()
    {
        //セリフを喋らせる
        talkcs.NovelContinue(timecs.time);
        //魔王とプレイヤーを椅子の周りで回らせる
        characs.HeroTurnMove();
        characs.BossTurnMove();
        //曲を止める
        if (musicstoptime < timecs.time)
        {
            musiccs.StopMusic();
        }
        //魔王を止める
        if (bossstoptime < timecs.time)
        {
            characs.BossStop();
        }
        //プレイヤーが止まったら止める
        //勝敗判定
    }
}

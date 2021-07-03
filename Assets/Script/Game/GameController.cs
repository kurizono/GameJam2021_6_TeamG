using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ReadText;


//全体のコントローラー
public class GameController : MonoBehaviour
{
    TimeContoller timecs;
    CharaTalk talkcs;
    CharaController characs;
    CharaStop charastopcs;
    GameMusic musiccs;
    WinEffect resultcs;

    float musicstoptime;
    float bossstoptime;

    int result = 0;
    
    // Start is called before the first frame update
    private void Awake()
    {
        timecs = GetComponent<TimeContoller>();
        talkcs = GetComponent<CharaTalk>();
        characs = GetComponent<CharaController>();
        charastopcs = GetComponent<CharaStop>();
        musiccs = GetComponent<GameMusic>();
        resultcs = GetComponent<WinEffect>();
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
        ////勝敗が決まってない場合
        if(result == 0) {
            StartCoroutine(ResultJudge());
        }
    }

    private IEnumerator ResultJudge()
    {
        //魔王を止める(魔王の勝利)
        if (bossstoptime < timecs.time)
        {
            result = 1;
            characs.BossStop();
            musiccs.StartDefeatSE();
            yield return StartCoroutine(resultcs.PlayerDefeat());
            SceneManager.LoadScene("Defeat");
        }
        //プレイヤーが止まったら止める(プレイヤーの勝利)
        else if (charastopcs.Stop())
        {
            result = 1;
            if (musicstoptime <= timecs.time)
            {
                Debug.Log("aaa");
                characs.HeroStop();
                musiccs.StartDefeatSE();
                yield return StartCoroutine(resultcs.PlayerWin());
                SceneManager.LoadScene("Win");
            }
            else
            {
                Debug.Log("aaa");
                characs.HeroStop();
                musiccs.StartDefeatSE();
                yield return StartCoroutine(resultcs.PlayerWin());
                SceneManager.LoadScene("Defeat");
            }

        }
    }
}

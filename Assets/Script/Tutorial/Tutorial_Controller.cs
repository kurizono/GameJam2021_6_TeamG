using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Controller : MonoBehaviour
{
    TimeContoller timecs;
    CharaController characs;
    CharaStop charastopcs;
    WinEffect resultcs;
    Tutorial_Speak speakcs;
    Tutorial_Talk talkcs;


    [SerializeField]
    public　AudioSource Tutorial_audio, se;
    [SerializeField]
    AudioClip SE01, SE02, Tell;

    //シナリオがどの段階まで行ったか
    int level;

    float musicstoptime;
    float bossstoptime;

    int result = 0;

    private void Awake()
    {
        timecs = GetComponent<TimeContoller>();
        characs = GetComponent<CharaController>();
        charastopcs = GetComponent<CharaStop>();
        resultcs = GetComponent<WinEffect>();
        speakcs = GetComponent<Tutorial_Speak>();
        talkcs = GetComponent<Tutorial_Talk>();

        Tutorial_audio.loop = true;
        Tutorial_audio.volume = 0.01f * OptionButton.MusicSound;
    }
    void Start()
    {
        level = 0;
        //曲が止まる時間を取得
        musicstoptime = 100;
        //魔王が止まる時刻を取得
        bossstoptime = 100;
    }

    // Update is called once per frame
    void Update()
    {
        switch (level) {
            case 0:
                //最初のセリフを喋らせる
                speakcs.PreNovel(timecs.time);
                break;
            case 1:
                //メインのセリフを喋らせる
                speakcs.MainNovel(timecs.time);
                //魔王とプレイヤーを椅子の周りで回らせる
                characs.HeroTurnMove();
                characs.BossTurnMove();
                //曲を止める
                if (musicstoptime < timecs.time)
                {
                    Tutorial_audio.Stop();
                }
                ////勝敗が決まってない場合
                if (result == 0)
                {
                    StartCoroutine(ResultJudge());
                }
                break;
        }
    }

    public void LevelChange(int i)
    {
        Tutorial_audio.Play();
        musicstoptime = Musictime() + timecs.time;
        timecs.Kingtime(musicstoptime);
        level = i;
        se.Stop();
    }

    private IEnumerator ResultJudge()
    {
        //魔王を止める(魔王の勝利)
        if (bossstoptime < timecs.time)
        {
            result = 1;
            characs.BossStop();
            Tutorial_audio.PlayOneShot(SE02);
            yield return StartCoroutine(resultcs.PlayerDefeat());
            SceneManager.LoadScene("Defeat");
        }
        //プレイヤーが止まったら止める(プレイヤーの勝利)
        else if (charastopcs.Stop())
        {
            result = 1;
            if (musicstoptime <= timecs.time)
            {
                characs.HeroStop();
                Tutorial_audio.PlayOneShot(SE01);
                yield return StartCoroutine(resultcs.PlayerWin());
                SceneManager.LoadScene("Win");
            }
            else
            {
                characs.HeroStop();
                yield return StartCoroutine(resultcs.PlayerWin());
                SceneManager.LoadScene("Defeat");
            }

        }
    }

    //音楽が止まる時間を取得
    public float Musictime()
    {
        int[,] stopnum = new int[2, 2];
        stopnum[0, 0] = talkcs.maintext.stopstart.taiknum;
        stopnum[0, 1] = talkcs.maintext.stopstart.charnum;
        stopnum[1, 0] = talkcs.maintext.stopend.taiknum;
        stopnum[1, 1] = talkcs.maintext.stopend.charnum;

        float[] musicstoprange = new float[2];

        for (int i = 0; i < musicstoprange.Length; i++)
        {
            musicstoprange[i] = 0;
            for (int j = 0; j < stopnum[i, 0]; j++)
            {
                musicstoprange[i] += talkcs.maintext.scenariomain[j].talk.Length * timecs.GetnovelSpeed();
                musicstoprange[i] += timecs.GetnovelSpace();
            }
            musicstoprange[i] += stopnum[i, 1] * timecs.GetnovelSpeed();
        }

        float musicstoptime = Random.Range(musicstoprange[0], musicstoprange[1]);
        return musicstoptime;
    }

    public void TellPhone()
    {
        se.PlayOneShot(Tell);
    }
}

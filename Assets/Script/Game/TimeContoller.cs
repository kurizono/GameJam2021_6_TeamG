using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeContoller : MonoBehaviour
{
    CharaController characs;

    //時間
    public float time, deltatime;

    //音楽を流す時間を計算

    //喋る時間
    //一文字一文字の表示する速さ
    float novelSpeed;
    //テキスト間の間
    float novelSpace;

    //魔王が止まるまでの間隔
    float[] bossstop = new float[3] { 1f, 0.6f, 0.3f };
    float bossstoptime;

    // Start is called before the first frame update
    void Awake()
    {
        characs = GetComponent<CharaController>();

        novelSpeed = OptionButton.textcharspeed;
        novelSpace = OptionButton.textspacespeed;
        bossstoptime = bossstop[OptionButton.Mode - 1];   //魔王が止まるまでの間隔
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        deltatime = Time.deltaTime;
    }


    //音楽が止まる時間を取得
    public float Musictime()
    {
        int Num = StorySelect.scenario_now;
        int[,] stopnum = new int[2, 2];
        stopnum[0, 0] = ReadTalk.TalkController.scenarios[Num].stopstart.taiknum;
        stopnum[0, 1] = ReadTalk.TalkController.scenarios[Num].stopstart.charnum;
        stopnum[1, 0] = ReadTalk.TalkController.scenarios[Num].stopend.taiknum;
        stopnum[1, 1] = ReadTalk.TalkController.scenarios[Num].stopend.charnum;

        float[] musicstoprange = new float[2];

        for (int i = 0; i < musicstoprange.Length; i++)
        {
            musicstoprange[i] = 0;
            for (int j = 0; j < stopnum[i, 0]; j++)
            {
                musicstoprange[i] += ReadTalk.TalkController.scenarios[Num].scenariomain[j].talk.Length * novelSpeed;
                musicstoprange[i] += novelSpace;
            }
            musicstoprange[i] += stopnum[i, 1] * novelSpeed;
        }

        float musicstoptime = Random.Range(musicstoprange[0], musicstoprange[1]);
        return musicstoptime;
    }

    //魔王が止まる時間を取得
    public float Kingtime(float musicstoptime)
    {
        float kingstoptime = musicstoptime + bossstoptime;
        return kingstoptime;
    }

    //速さを取得する
    public float GetnovelSpeed()
    {
        return novelSpeed;
    }
    public float GetnovelSpace()
    {
        return novelSpace;
    }

}

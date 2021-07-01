using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeContoller : MonoBehaviour
{
    //時間
    public float time;

    //音楽を流す時間を計算

    //喋る時間
    //一文字一文字の表示する速さ
    float novelSpeed = 0.01f;
    //テキスト間の間
    float novelSpace = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    //音楽が止まる時間を取得(止まる最初の時間,止まる最後の時間)
    public float[] Musictime()
    {
        int Num = StorySelect.scenario_now;
        int[,] stopnum = new int[2, 2];
        stopnum[0, 0] = ReadTalk.TalkController.scenarios[Num].stopstart.taiknum;
        stopnum[0, 1] = ReadTalk.TalkController.scenarios[Num].stopstart.charnum;
        stopnum[1, 0] = ReadTalk.TalkController.scenarios[Num].stopend.taiknum;
        stopnum[1, 1] = ReadTalk.TalkController.scenarios[Num].stopend.charnum;

        float[] musicstoptime = new float[2];

        for (int i = 0; i < musicstoptime.Length; i++)
        {
            musicstoptime[i] = 0;
            for (int j = 0; j < stopnum[i, 0]; j++)
            {
                musicstoptime[i] += ReadTalk.TalkController.scenarios[Num].scenariomain[j].talk.Length * novelSpeed;
                musicstoptime[i] += novelSpace;
            }
            musicstoptime[i] += stopnum[i, 1] * novelSpeed;
        }
        return musicstoptime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadTalk;
using DataControll;

//タイトルでStoryを選択する
public class StorySelect : MonoBehaviour
{
    int scenario_num = 0;
    public static int scenario_now = 0;

    private void Start()
    {
        scenario_num = TalkController.scenario_num;
    }

    //ストーリーをランダムで決める場合
    public void StoryRandom()
    {
        scenario_now = Random.Range(0,scenario_num);
    }
    //ストーリーを選ぶ場合
    public void StoryCheck(int scenarionum)
    {
        scenario_now = scenarionum;
    }
}

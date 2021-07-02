using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadTalk;

public class ShowAllStory : MonoBehaviour
{
    //一度の表示するシナリオタイトルの数
    int looktitlenum = 4;
    //総ページ数
    int pagenum;
    //最後のページのタイトル数
    int lastpagetitle;

    //ページの総数
    public int AllPageNum()
    {
        pagenum = ((TalkController.scenario_num - 1) / looktitlenum) + 1;
        return pagenum;
    }

    //番号を入れたらシナリオの名前を返す
    public string Scenario_title(int num)
    {
        string titlename = TalkController.scenarios[num].title;
        string titleshowname = " No." + num + ":" + titlename;
        return titleshowname;
    }

    //次に表示するシナリオタイトルの数
    public int Look_titlenum(int gopage)
    {
        int titlenum = looktitlenum;
        lastpagetitle = (TalkController.scenario_num - 1) % looktitlenum + 1;
        if (gopage == pagenum) 
        {
            titlenum = lastpagetitle;
        }
        return titlenum;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ReadTalk;

public class StoryButton : MonoBehaviour
{
    TitleController Controllercs;
    ShowAllStory Pagecs;
    StorySelect Selectcs;

    [SerializeField]
    Button Button_Pre, Button_Post, Button_Game1, Button_Game2, Button_Game3, Button_Game4, Button_back;
    Button[] Button_Game;

    [SerializeField]
    Text Text_Pre, Text_Post, Text_Game1, Text_Game2, Text_Game3, Text_Game4;
    Text[] Text_Game;


    //今のページ数
    int nowpage;
    //総ページ数
    int allpage;


    void Awake()
    {
        Controllercs = GetComponent<TitleController>();
        Pagecs = GetComponent<ShowAllStory>();
        Selectcs = GetComponent<StorySelect>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Button_Game = new Button[4] { Button_Game1, Button_Game2, Button_Game3, Button_Game4 };
        Button_Pre.onClick.AddListener(PreClick);
        Button_Post.onClick.AddListener(PostClick);
        for(int i = 0; i < Button_Game.Length; i++)
        {
            int buttonNum = i;
            Button_Game[buttonNum].onClick.AddListener(() => GameClick(buttonNum));
        }
        Button_back.onClick.AddListener(BackClick);

        Text_Game = new Text[4] { Text_Game1, Text_Game2, Text_Game3, Text_Game4 };

        allpage = Pagecs.AllPageNum();
        FirstSetting();
    }

    //Storyボタン押した時に最初にやるやつ
    void FirstSetting()
    {
        nowpage = 1;
        Button_Pre.gameObject.SetActive(false);
        Button_Post.gameObject.SetActive(true);
        for (int i = 0; i < Button_Game.Length; i++)
        {
            Button_Game[i].gameObject.SetActive(true);
        }
        Button_back.gameObject.SetActive(true);
        ChangeTitle();
    }

    //前へボタンの挙動
    void PreClick()
    {
        nowpage--;
        Button_Post.gameObject.SetActive(true);
        //前が最初のページの場合
        if (nowpage == 1)
        {
            Button_Pre.gameObject.SetActive(false);
            Text_Pre.color = ButtonEfect.offcolor;
        }
        for(int i = 0; i < Text_Game.Length; i++)
        {
            Button_Game[i].gameObject.SetActive(true);
        }
        ChangeTitle();

    }
    //次へボタンの挙動
    void PostClick()
    {
        nowpage++;
        //前へボタンを表示
        Button_Pre.gameObject.SetActive(true);
        //次が最終ページの場合
        if (nowpage >= allpage)
        {
            Button_Post.gameObject.SetActive(false);
            Text_Post.color = ButtonEfect.offcolor;

            //いらないボタンを消す
            int nexttitlenum = Pagecs.Look_titlenum(nowpage);
            for (int i = nexttitlenum; i < Text_Game.Length; i++)
            {
                Button_Game[i].gameObject.SetActive(false);
            }
        }
        ChangeTitle();

        Debug.Log(allpage);
        Debug.Log(nowpage);
    }

    //ゲームボタンの挙動
    void GameClick(int gamenum)
    {
        Debug.Log("Gamenum" + gamenum);
        int selectnum = ButtonTitleNum(gamenum);
        Selectcs.StoryCheck(selectnum);
        Debug.Log(selectnum);
        SceneManager.LoadScene("Game01");
    }

    //戻るボタンの挙動
    void BackClick()
    {
        Controllercs.HomeSetting(); 
    }

    //タイトルの変更
    void ChangeTitle()
    {
        for (int i = 0; i < Text_Game.Length; i++)
        {
            //タイトルの初期化
            Text_Game[i].text = "";

            int titlenum = ButtonTitleNum(i);
            if (titlenum < TalkController.scenario_num)
            {
                string titlename = Pagecs.Scenario_title(titlenum);
                Text_Game[i].text = titlename;
            }
        }
    }

    //ボタンのシナリオ番号
    int ButtonTitleNum(int buttonnum)
    {
        int titlenum = (nowpage - 1) * 4 + buttonnum;
        return titlenum;
    }
}

using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

namespace ReadTalk
{
    public class TalkController : MonoBehaviour
    {
        enum splitpoint
        {
            over,
            setting,
            setting_info,
            main,
            face_text
        };
        string[][] splitword =  {
            new string[]{"---"},
            new string[]{"\n"},
            new string[]{":" },
            new string[]{":"},
            new string[]{"\n"},
        };

        //フォルダ内のシナリオを全て読み込む
        //シナリオ番号ごとに順番を付ける
        //全てのタイトルとシナリオを保存する

        //シナリオ(シナリオ番号(float),シナリオタイトル(string),シナリオ使用音楽(string),シナリオ本文(list[]))
        public class Scenario
        {
            public float number;
            public string title;
            public string music;
            public ScenarioTalk[] scenariomain;
            public ScenarioStop stopstart;
            public ScenarioStop stopend;
        }
        //シナリオ本文(表情差分,セリフ)
        public class ScenarioTalk
        {
            public string face;
            public string talk;
        }
        //シナリオの#の位置(セリフ数,セリフの文字数)
        public class ScenarioStop
        {
            public int taiknum = 0;
            public int charnum = 0;
        }

        private Object[] allscenario_obj;
        private string[] allscenario_txt;

        //シナリオ
        public static Scenario[] scenarios;
        //シナリオの数
        public static int scenario_num;


        private void Awake()
        {
            //シナリオをデータにする
            GetScenario();
            //シナリオ番号で入れかえ
            TextNumberSort();
        }

        //最初に一回のみする奴
        public void First()
        {
            //シナリオをデータにする
            GetScenario();
            //シナリオ番号で入れかえ
            TextNumberSort();
        }


        //全てのシナリオをstringとして取得
        private void GetScenario()
        {
            allscenario_obj = Resources.LoadAll("Scenario", typeof(TextAsset));
            scenario_num = allscenario_obj.Length;
            allscenario_txt = new string[scenario_num];
            for (int i = 0; i < scenario_num; i++)
            {
                allscenario_txt[i] = (allscenario_obj[i] as TextAsset).text;
            }

            //シナリオの構造体を作成
            scenarios = new Scenario[scenario_num];
            for (int i = 0; i < scenario_num; i++)
            {
                scenarios[i] = new Scenario();
                scenarios[i].stopstart = new ScenarioStop();
                scenarios[i].stopend = new ScenarioStop();
            }

            StructiveScenario(allscenario_txt);
        }

        //構造体に文章を入れる
        void StructiveScenario(string[] scenario_txt)
        {

            string[] maintext;

            int scenariocount;
            //各シナリオ
            for (scenariocount = 0; scenariocount < scenario_num; scenariocount++)
            {
                string[] splittext = scenario_txt[scenariocount].Split(splitword[(int)splitpoint.over], System.StringSplitOptions.RemoveEmptyEntries);
                if (splittext.Length != 2)
                {
                    Debug.LogError(scenariocount + "scenario" + "ScenarioTextError(---)");
                    return;
                }
                
                //設定
                SettingStruct(splittext[0]);
                //メインテキスト
                MainStruct(splittext[1]);
            }
            StopStruct();

            //Setting
            bool SettingStruct(string text)
            {
                string[] settingtext;
                settingtext = text.Trim().Split(splitword[(int)splitpoint.setting], System.StringSplitOptions.RemoveEmptyEntries);
                if (settingtext.Length != 3)
                {
                    Debug.LogError(scenariocount + "scenario" + "ScenarioTextError(settingtext)");
                    return false;
                }

                //settingを入れる
                if (!Number())
                {
                    return false;
                }
                if (!Title())
                {
                    return false;
                }
                if (!Music())
                {
                    return false;
                }

                //シナリオナンバー
                bool Number()
                {
                    string[] settingnumber = settingtext[0].Split(splitword[(int)splitpoint.setting_info], System.StringSplitOptions.RemoveEmptyEntries);
                    if (settingnumber.Length != 2)
                    {
                        Debug.LogError(scenariocount + "scenario" + "NullTitleNumber");
                        return false;
                    }
                    else if (float.TryParse(settingnumber[1], out float num))
                    {
                        scenarios[scenariocount].number = num;
                    }
                    else
                    {
                        Debug.LogError(scenariocount + "scenario" + "NoTitleNumber");
                        return false;
                    }
                    return true;
                }
                //シナリオタイトル
                bool Title()
                {
                    string[] settingtitle = settingtext[1].Split(splitword[(int)splitpoint.setting_info], System.StringSplitOptions.RemoveEmptyEntries);
                    if (settingtitle.Length < 2)
                    {
                        Debug.LogError(scenariocount + "scenario" + "NullTitleName");
                        return false;
                    }
                    else if (settingtitle.Length > 2)
                    {
                        for (int j = 2; j < settingtitle.Length; j++)
                        {
                            settingtitle[1] += splitword[(int)splitpoint.setting_info] + settingtitle[j];
                        }
                    }
                    scenarios[scenariocount].title = settingtitle[1];
                    return true;
                }
                //シナリオミュージック
                bool Music()
                {
                    string[] settingmusic = settingtext[2].Split(splitword[(int)splitpoint.setting_info], System.StringSplitOptions.RemoveEmptyEntries);
                    if (settingmusic.Length < 2)
                    {
                        Debug.LogError(scenariocount + "scenario" + "NullTitleName");
                        return false;
                    }
                    else if (settingmusic.Length > 2)
                    {
                        for (int j = 2; j < settingmusic.Length; j++)
                        {
                            settingmusic[1] += splitword[(int)splitpoint.setting_info] + settingmusic[j];
                        }
                    }
                    scenarios[scenariocount].music = settingmusic[1];
                    return false;
                }

                return true;
            }

            //Main
            bool MainStruct(string text)
            {
                //:ごとに文章を分ける
                maintext = text.Trim().Split(splitword[(int)splitpoint.main], System.StringSplitOptions.RemoveEmptyEntries);
                string[] onetalk;
                int talkcount;

                for (talkcount = 0; talkcount < maintext.Length; talkcount++)
                {

                    ScenarioTalk onemain = new ScenarioTalk();
                    onetalk = maintext[talkcount].Trim().Split(splitword[(int)splitpoint.face_text], System.StringSplitOptions.RemoveEmptyEntries);
                    if (!Face(onemain) || !Talk(onemain))
                    {
                        return false;
                    }
                    System.Array.Resize(ref scenarios[scenariocount].scenariomain, talkcount + 1);
                    scenarios[scenariocount].scenariomain[talkcount] = onemain;
                }

                bool Face(ScenarioTalk main)
                {
                    main.face = onetalk[0];
                    return true;
                }
                bool Talk(ScenarioTalk main)
                {
                    string talktext = "";
                    if (onetalk.Length >= 2)
                    {
                        talktext = onetalk[1];
                        for (int k = 2; k < onetalk.Length; k++)
                        {
                            talktext += "\n" + onetalk[k];
                        }
                    }
                    main.talk = talktext;
                    return true;
                }
                return true;
            }

            //シナリオの止める場所
            bool StopStruct()
            {
                for (int i = 0; i < scenario_num; i++)
                {
                    int count = 0;
                    for (int j = 0; j < scenarios[i].scenariomain.Length; j++)
                    {
                        //#がある行での処理
                        if (scenarios[i].scenariomain[j].talk.Contains("#"))
                        {
                            //同じ行に#が二つあった場合
                            if (CountChar(scenarios[i].scenariomain[j].talk, '#') == 2)
                            {
                                StopstartPoint();
                                StopendPoint();
                                count++;
                            }
                            else
                            {
                                if (count == 0)
                                {
                                    StopstartPoint();
                                }
                                else
                                {
                                    StopendPoint();
                                }
                            }

                            void StopstartPoint()
                            {
                                scenarios[i].stopstart.taiknum = j;
                                scenarios[i].stopstart.charnum = scenarios[i].scenariomain[j].talk.IndexOf("#");
                                scenarios[i].scenariomain[j].talk = scenarios[i].scenariomain[j].talk.Remove(scenarios[i].stopstart.charnum,1);
                            }
                            void StopendPoint()
                            {
                                scenarios[i].stopend.taiknum = j;
                                scenarios[i].stopend.charnum = scenarios[i].scenariomain[j].talk.IndexOf("#");
                                scenarios[i].scenariomain[j].talk = scenarios[i].scenariomain[j].talk.Remove(scenarios[i].stopend.charnum, 1);
                            }
                            count++;
                        }
                    }
                    if (count != 2)
                    {
                        Debug.LogError(scenarios[i].scenariomain[0].talk + "で始まるシナリオの#の数が不正:" + count);
                        return false;
                    }
                }

                int CountChar(string s, char c)
                {
                    return s.Length - s.Replace(c.ToString(), "").Length;
                }


                return true;
            }
        }


        //数字と連動でソート
        void TextNumberSort()
        {
            for (int i = 0; i < scenario_num - 1; i++)
            {
                for (int j = i + 1; j < scenario_num; j++)
                {
                    if (scenarios[i].number > scenarios[j].number)
                    {
                        Scenario change = scenarios[i];
                        scenarios[i] = scenarios[j];
                        scenarios[j] = change;
                    }
                }
            }
        }
    }
}

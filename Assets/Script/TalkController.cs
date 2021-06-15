using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ReadTalk
{
    public class TalkController : MonoBehaviour
    {
        //フォルダ内のシナリオを全て読み込む
        //シナリオ番号ごとに順番を付ける
        //全てのタイトルとシナリオを保存する

        //シナリオ(シナリオタイトル(string),シナリオ番号(string),シナリオ使用音楽(string),シナリオ本文(list[]))
        class Scenario
        {
            public string title;
            public string number;
            public string music;
            public List<ScnarioTalk> scnariomain;
            
        }
        //シナリオ本文(表情差分,セリフ)
        public class ScnarioTalk
        {
            public string face;
            public string talk;
        }


        private Object[] allscenario_obj;
        private string[] allscenario_txt;

        int scenario_num;


        private void Awake()
        {
            GetScenario();
        }

        //全てのシナリオをstringとして取得
        void GetScenario()
        {
            allscenario_obj = Resources.LoadAll("Scenario",typeof(TextAsset));
            scenario_num = allscenario_obj.Length;
            allscenario_txt = new string[scenario_num];
            for (int i = 0; i < scenario_num; i++) { 
                allscenario_txt[i] = (allscenario_obj[i] as TextAsset).text;
            }
        }
        //構造体に文章を入れる
        void StructiveScenario(string[] scenarios)
        {
            string[] splittext = allscenario_txt[0].Split(char.Parse("---"));

        }

        //数字と連動でソート
        void TextNumberSort(int[] text_num, string[] text)
        {

        }
    }
}

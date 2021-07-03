using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Talk : MonoBehaviour
{
    FaceController facecs;


    //チュートリアルのテキスト構成
    public class Tutorial_Text
    {
        public ScenarioTalk[] scenariomain;
        public ScenarioStop stopstart;
        public ScenarioStop stopend;
    }
    //シナリオ本文(表情差分,セリフ)
    public class ScenarioTalk
    {
        public int face;
        public string talk;
    }
    //シナリオの#の位置(セリフ数,セリフの文字数)
    public class ScenarioStop
    {
        public int taiknum = 0;
        public int charnum = 0;
    }

    [SerializeField]
    TextAsset pretxt, maintxt;

    public ScenarioTalk[] pretext;
    public Tutorial_Text maintext = new Tutorial_Text();


    //顏差分対応表
    string[,] facesform;
    string[] onefaceform;

    private void Awake()
    {
        GetText();
    }

    public void GetText()
    {
        maintext.stopstart = new ScenarioStop();
        maintext.stopend = new ScenarioStop();


        string pretext_str = pretxt.text;
        string main_str = maintxt.text;
        pretext = PreStructive(pretext_str, pretext);
        maintext =  Structive(main_str, maintext);
    }

    private ScenarioTalk[] PreStructive(string nowtext, ScenarioTalk[] structure)
    {
        //一つの構造体ごとの分割
        string[] splitword = { ":" };
        string[] splittext = nowtext.Split(splitword, System.StringSplitOptions.RemoveEmptyEntries);
        //構造体の初期化
        structure = new ScenarioTalk[splittext.Length];
        string[] onetalk;
        for (int i = 0; i < splittext.Length; i++)
        {
            structure[i] = new ScenarioTalk();
            //顔とテキストの分割
            splitword = new string[] { "\n" };
            onetalk = splittext[i].Trim().Split(splitword, System.StringSplitOptions.RemoveEmptyEntries);
            Face(structure[i], onetalk);
            Talk(structure[i], onetalk);
        }
        return structure;
    }

    private Tutorial_Text Structive(string nowtext, Tutorial_Text structure)
    {
        //一つの構造体ごとの分割
        string[] splitword = { ":" };
        string[] splittext = nowtext.Split(splitword, System.StringSplitOptions.RemoveEmptyEntries);
        //構造体の初期化
        structure.scenariomain = new ScenarioTalk[splittext.Length];

        string[] onetalk;
        for (int i = 0; i < splittext.Length; i++)
        {
            structure.scenariomain[i] = new ScenarioTalk();
            //顔とテキストの分割
            splitword = new string[] { "\n" };
            onetalk = splittext[i].Trim().Split(splitword, System.StringSplitOptions.RemoveEmptyEntries);
            Face(structure.scenariomain[i], onetalk);
            Talk(structure.scenariomain[i], onetalk);
        }
        StopStruct();
        return structure;


        //シナリオの止める場所
        bool StopStruct()
        {
            int count = 0;

            for (int j = 0; j < structure.scenariomain.Length; j++)
            {
                //#がある行での処理
                if (structure.scenariomain[j].talk.Contains("#"))
                {
                    //同じ行に#が二つあった場合
                    if (CountChar(structure.scenariomain[j].talk, '#') == 2)
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
                        structure.stopstart.taiknum = j;
                        structure.stopstart.charnum = structure.scenariomain[j].talk.IndexOf("#");
                        structure.scenariomain[j].talk = structure.scenariomain[j].talk.Remove(structure.stopstart.charnum, 1);
                    }
                    void StopendPoint()
                    {
                        structure.stopend.taiknum = j;
                        structure.stopend.charnum = structure.scenariomain[j].talk.IndexOf("#");
                        structure.scenariomain[j].talk = structure.scenariomain[j].talk.Remove(structure.stopend.charnum, 1);
                    }
                    count++;
                }
            }
            if (count != 2)
            {
                Debug.LogError(structure.scenariomain[0].talk + "で始まるシナリオの#の数が不正:" + count);
                return false;
            }
            int CountChar(string s, char c)
            {
                return s.Length - s.Replace(c.ToString(), "").Length;
            }
            return true;

        }
    }
    bool Face(ScenarioTalk main, string[] onetalk)
    {
        GetFaceForm();

        onetalk[0] = onetalk[0].Trim();

        for (int i = 0; i < facesform.GetLength(0); i++)
        {
            if (onetalk[0] == facesform[i, 1])
            {
                facecs = GetComponent<FaceController>();
                facecs.GetCharaFaces();
                main.face = facecs.GetFaceNum(facesform[i, 0]);
                return true;
            }
        }
        Debug.LogError(onetalk[0] + "は顔差分対応表に記載のない表情です");
        return false;
    }
    bool Talk(ScenarioTalk main, string[] onetalk)
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


    //顏差分対応表を取得
    private void GetFaceForm()
    {
        object faceform_obj = Resources.Load("Facename_Form", typeof(TextAsset));
        string faceform_str = (faceform_obj as TextAsset).text;
        string[] splitfaceformword = { "\n" };
        onefaceform = faceform_str.Trim().Split(splitfaceformword, System.StringSplitOptions.RemoveEmptyEntries);

        string[] splitonefaceformword = { "," };
        facesform = new string[onefaceform.Length, 2];
        for (int formnum = 0; formnum < onefaceform.Length; formnum++)
        {
            string[] nowfaceform = onefaceform[formnum].Trim().Split(splitonefaceformword, System.StringSplitOptions.RemoveEmptyEntries);
            if (nowfaceform.Length != 2)
            {
                Debug.LogError("顔差文対応表の" + formnum + "行目が不正です");
                break;
            }
            for (int i = 0; i < 2; i++)
            {
                nowfaceform[i] = nowfaceform[i].Trim();
                facesform[formnum, i] = nowfaceform[i];
            }
        }

    }
}

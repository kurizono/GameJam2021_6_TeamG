using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Debugger = System.Diagnostics;

namespace DataControll
{

    public class SaveDataAccess : MonoBehaviour
    {
        public enum savedatanum
        {
            win,
            lose
        }

        StreamWriter writer;
        StreamReader reader;

        [System.Serializable]
        public class SaveData
        {
            public int[] save;
        }
        public static SaveData MySaveData = new SaveData();

        string savedatapath;

        //最初に一回のみするやつ
        public void First()
        {
            savedatapath = Application.dataPath + "/SaveData/savedata.json";
            TestResetData();
            MySaveData = LoadData();
        }

        //セーブのロード
        public SaveData LoadData()
        {
            string datastr = "";
            reader = new StreamReader(savedatapath);
            datastr = reader.ReadToEnd();
            reader.Close();
            Debug.Log(datastr);
            return JsonUtility.FromJson<SaveData>(datastr);
        }

        //セーブのリセット
        private void ResetData()
        {
            for (int i = 0; i < MySaveData.save.Length; i++)
            {
                MySaveData.save[i] = 1;
            }
            string jsonstr = JsonUtility.ToJson(MySaveData);
            writer = new StreamWriter(savedatapath, false);
            writer.Write(jsonstr);
            writer.Flush();
            writer.Close();
        }

        //勝利時のセーブデータ書き換え
        public static void WinData(int scenario_win)
        {
            MySaveData.save[scenario_win] = (int)savedatanum.win;
        }

        //デバッグ用(データが増えるごとにやること)
        [Debugger.Conditional("UNITY_EDITOR")]
        private void TestResetData()
        {
            int scenario_num = Resources.LoadAll("Scenario", typeof(TextAsset)).Length;
            MySaveData.save = new int[scenario_num];
            for (int i = 0; i < scenario_num; i++)
            {
                MySaveData.save[i] = (int)savedatanum.lose;
            }
            string jsonstr = JsonUtility.ToJson(MySaveData);
            Debug.Log(jsonstr);
            writer = new StreamWriter(savedatapath, false);
            writer.Write(jsonstr);
            writer.Flush();
            writer.Close();
        }

    }
}

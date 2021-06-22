using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataControll;
using ReadTalk;

public class FirstActionController : MonoBehaviour
{
    DataControll.SaveDataAccess datacs;
    ReadTalk.TalkController talkcs;
    static bool firstmethod = true;
    // Start is called before the first frame update
    void Awake()
    {
        datacs = GetComponent<DataControll.SaveDataAccess>();
        talkcs = GetComponent<ReadTalk.TalkController>();
        if (firstmethod)
        {
            datacs.First();
            talkcs.First();
        }

    }
}

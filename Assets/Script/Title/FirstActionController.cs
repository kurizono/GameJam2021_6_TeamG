using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataControll;
using ReadTalk;

public class FirstActionController : MonoBehaviour
{
    DataControll.SaveDataAccess datacs;
    ReadTalk.TalkController talkcs;
    FaceController facecs;

    OptionButton optioncs;

    [SerializeField]
    GameObject ScriptGameobj;

    static bool firstmethod = true;
    // Start is called before the first frame update
    void Awake()
    {
        datacs = GetComponent<DataControll.SaveDataAccess>();
        talkcs = GetComponent<ReadTalk.TalkController>();
        facecs = GetComponent<FaceController>();

        optioncs = ScriptGameobj.GetComponent<OptionButton>();

        if (firstmethod)
        {
            facecs.First();
            datacs.First();
            talkcs.First();
            optioncs.First();
        }

    }
}

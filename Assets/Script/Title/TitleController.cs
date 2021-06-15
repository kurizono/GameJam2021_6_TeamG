using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject Obj_Home, Obj_story, Obj_option;

    public void Start()
    {
        HomeSetting();
    }

    public void HomeSetting()
    {
        Obj_Home.SetActive(true);
        Obj_story.SetActive(false);
        Obj_option.SetActive(false);
    }
    public void StorySetting()
    {
        Obj_Home.SetActive(false);
        Obj_story.SetActive(true);
        Obj_option.SetActive(false);
    }
    public void OptionSetting()
    {
        Obj_Home.SetActive(false);
        Obj_story.SetActive(false);
        Obj_option.SetActive(true);
    }
}

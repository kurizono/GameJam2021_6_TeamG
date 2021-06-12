using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField]
    Button Button_start, Button_story, Button_option, Button_quit;
    [SerializeField]
    GameObject Obj_Title, Obj_story, Obj_option;

    // Start is called before the first frame update
    void Start()
    {
        Button_start.onClick.AddListener(StartClick);
        Button_story.onClick.AddListener(StoryClick);
        Button_option.onClick.AddListener(OptionClick);
        Button_quit.onClick.AddListener(QuitClick);
    }
    void StartClick()
    {
        Debug.Log("start");
    }
    void StoryClick()
    {
        Debug.Log("story");
    }
    void OptionClick()
    {
        Debug.Log("option");
    }
    void QuitClick()
    {
        Debug.Log("quit");
    }
}

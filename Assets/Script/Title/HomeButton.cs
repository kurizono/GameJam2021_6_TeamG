using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    TitleController Controllercs;
    StorySelect StorySelectcs;

    [SerializeField]
    Button Button_Tutorial, Button_start, Button_story, Button_option, Button_quit;


    void Awake()
    {
        Controllercs = GetComponent<TitleController>();
        StorySelectcs = GetComponent<StorySelect>();
    }
    void Start()
    {
        Button_Tutorial.onClick.AddListener(Tutorial_Click);
        Button_start.onClick.AddListener(StartClick);
        Button_story.onClick.AddListener(StoryClick);
        Button_option.onClick.AddListener(OptionClick);
        Button_quit.onClick.AddListener(QuitClick);
    }

    void Tutorial_Click()
    {
        SceneManager.LoadScene("Tutorial");
    }
    void StartClick()
    {
        StorySelectcs.StoryRandom();
        SceneManager.LoadScene("Tell");
    }
    void StoryClick()
    {
        Controllercs.StorySetting();
    }
    void OptionClick()
    {
        Controllercs.OptionSetting();
    }
    void QuitClick()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}

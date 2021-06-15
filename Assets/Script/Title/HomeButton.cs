using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    TitleController Controllercs; 

    [SerializeField]
    Button Button_start, Button_story, Button_option, Button_quit;
    
    
    void Awake()
    {
        Controllercs = GetComponent<TitleController>();
    }
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
        SceneManager.LoadScene("Game01");

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

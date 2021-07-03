using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    TitleController Controllercs;

    [SerializeField]
    InputField InputCharSpeed, InputMode, InputMusic;
    [SerializeField]
    Button Button_back;

    //テキストの速さ
    [System.NonSerialized]
    public static float textcharspeed, textspacespeed;
    //速さの倍率
    float speedStandard = 0.2f;
    float speedaccess = 5;
    float Speedprobability = 5;
    int[] speedrange = new int[2] { 1, 3 };


    //難易度
    [System.NonSerialized]
    public static int Mode;
    int[] moderange = new int[2] { 1, 3 };


    //音量
    [System.NonSerialized]
    public static int MusicSound;
    int[] musicrange = new int[2] { 1, 100 };

    void Awake()
    {
        Controllercs = GetComponent<TitleController>();

        InputCharSpeed.onEndEdit.AddListener(GetCharSpeed);
        InputMode.onEndEdit.AddListener(GetMode);
        InputMusic.onEndEdit.AddListener(GetMusic);
        Button_back.onClick.AddListener(BackClick);
    }
    void Start()
    {
        InputCharSpeed.text = (textcharspeed * speedaccess).ToString();
        InputMode.text = Mode.ToString();
        InputMusic.text = MusicSound.ToString();
    }

    public void First()
    {
        textcharspeed = speedStandard;
        textspacespeed = textcharspeed * Speedprobability;
        Mode = 1;
        MusicSound = 100;
    }

    void GetCharSpeed(string text)
    {
        if (int.TryParse(text, out int speed))
        {
            if(speed >= speedrange[0]  && speed <= speedrange[1])
            {
                textcharspeed = speedStandard / speed;
                textspacespeed = textcharspeed * Speedprobability;
            }
            else
            {
                InputCharSpeed.text = (textcharspeed * speedaccess).ToString();
            }
        }
    }

    void GetMode(string text)
    {
        if (int.TryParse(text, out int modenum))
        {
            if (modenum >= moderange[0] && modenum <= moderange[1])
            {
                Mode = modenum;
            }
            else
            {
                InputMode.text = Mode.ToString();
            }
        }
    }

    void GetMusic(string text)
    {
        if (int.TryParse(text, out int musicnum))
        {
            if (musicnum >= musicrange[0] && musicnum <= musicrange[1])
            {
                MusicSound = musicnum;
            }
            else
            {
                InputMusic.text = MusicSound.ToString();
            }
        }
    }

    void BackClick()
    {
        Controllercs.HomeSetting();
    }
}

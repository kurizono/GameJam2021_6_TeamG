using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharColor : MonoBehaviour
{
    [SerializeField]
    Outline Result;
    
    float time;

    //色変更の一周の時間
    float colorchangeTime;
    //色の周期数
    int colornum = 6;
    int nowcolornum;
    // (1.0,0),  (1,0,1), (0,0,1), (0,1,1) (0,1,0), (1,1,0)

    void Start()
    {
        Result.effectColor = new Color(1, 0, 0);
        time = 0;
        colorchangeTime = 0.5f;
        nowcolornum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > colorchangeTime)
        {
            nowcolornum = (nowcolornum + 1) % colornum;
            time = time - colorchangeTime;
        }

        switch (nowcolornum)
        {
            case 0:
                ColorChange00();
                break;
            case 1:
                ColorChange01();
                break;
            case 2:
                ColorChange02();
                break;
            case 3:
                ColorChange03();
                break;
            case 4:
                ColorChange04();
                break;
            case 5:
                ColorChange05();
                break;
        }
    }

    // (1.0,0),  (1,0,1)
    void ColorChange00()
    {
        Result.effectColor = new Color(1, 0, (time / colorchangeTime));
    }
    // (1,0,1), (0,0,1)
    void ColorChange01()
    {
        Result.effectColor = new Color(1 - (time / colorchangeTime), 0, 1);
    }
    // (0,0,1), (0,1,1) 
    void ColorChange02()
    {
        Result.effectColor = new Color(0, (time / colorchangeTime), 1);
    }
    // (0,1,1), (0,1,0)
    void ColorChange03()
    {
        Result.effectColor = new Color(0, 1, 1 - (time / colorchangeTime));
    }
    // (0,1,0), (1,1,0)
    void ColorChange04()
    {
        Result.effectColor = new Color((time / colorchangeTime), 1, 0);
    }
    //  (1,1,0), (1,0,0)
    void ColorChange05()
    {
        Result.effectColor = new Color(1, 1 - (time / colorchangeTime), 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer boss_renderer;
    [SerializeField]
    Sprite boss01, boss02;
    float time;



    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time % 1 > 0.5f)
        {
            boss_renderer.sprite = boss01;
        }
        else
        {
            boss_renderer.sprite = boss02;
        }
    }
}

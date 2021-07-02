using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    TimeContoller timecs;
    CharaStop CharaStopcs;

    enum Chara
    {
        hero, 
        boss
    }

    [SerializeField]
    public GameObject chair;
    [SerializeField]
    public GameObject hero, boss;
    GameObject[] chara;

    [SerializeField]
    public GameObject hero_chair, boss_chair;
    GameObject[] chara_chair;


    Vector3 chara_firstposi = new Vector3(0, 0, 0);
    Vector3 hero_firstposi = new Vector3(0, 1.8f, 0);
    Vector3 boss_firstposi = new Vector3(0, -1.8f, 0);

    float charamove = 45;

    bool[] stoped = new bool[2] { false, false };


    // Start is called before the first frame update
    void Start()
    {
        timecs = GetComponent<TimeContoller>();
        CharaStopcs = GetComponent<CharaStop>();

        chara = new GameObject[2] { hero, boss };
        chara_chair = new GameObject[2] { hero_chair, boss_chair };

        hero.transform.localPosition = hero_firstposi;
        boss.transform.localPosition = boss_firstposi;
    }

    public void HeroTurnMove()
    {
        //キャラが周りを回るかの判定
        if (!stoped[(int)Chara.hero]) 
        {
            Turn(hero_chair); 
        }
        if (CharaStopcs.Stop()) 
        { 
            stoped[(int)Chara.hero] = true; 
        }
    }
    public void BossTurnMove()
    {
        if (!stoped[(int)Chara.boss]) 
        {
            Turn(boss_chair);
        }
    }

    public void HeroStop()
    {
        stoped[(int)Chara.hero] = true;
    }
    public void BossStop()
    {
        stoped[(int)Chara.boss] = true;
    }

    //キャラを回す
    public void Turn(GameObject chara)
    {
        chara.transform.Rotate(new Vector3(0, 0, 1), charamove * timecs.deltatime);
    }

}

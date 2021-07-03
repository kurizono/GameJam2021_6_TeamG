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
    GameObject chair;
    [SerializeField]
    GameObject hero, boss;
    GameObject[] chara;

    [SerializeField]
    GameObject hero_chair, boss_chair;
    GameObject[] chara_chair;

    [SerializeField]
    Sprite hero01, hero02, boss01, boss02;
    Sprite[,] chara_Splite;

    SpriteRenderer hero_renderer, boss_renderer;
    SpriteRenderer[] chara_render;

    Vector3 chara_firstposi = new Vector3(0, 0, 0);
    Vector3 hero_firstposi = new Vector3(0, -1.8f, 0);
    Vector3 boss_firstposi = new Vector3(0, 1.8f, 0);

    float charamove = 45;

    //止まったかどうかの判定
    bool[] stoped = new bool[2] { false, false };



    // Start is called before the first frame update
    void Start()
    {
        timecs = GetComponent<TimeContoller>();
        CharaStopcs = GetComponent<CharaStop>();

        chara = new GameObject[2] { hero, boss };
        chara_chair = new GameObject[2] { hero_chair, boss_chair };
        chara_Splite = new Sprite[2, 2] { { hero01, hero02 }, { boss01, boss02 } };
        hero_renderer = hero.gameObject.GetComponent<SpriteRenderer>();
        boss_renderer = boss.gameObject.GetComponent<SpriteRenderer>();
        chara_render = new SpriteRenderer[2] { hero_renderer, boss_renderer };

        hero.transform.localPosition = hero_firstposi;
        boss.transform.localPosition = boss_firstposi;
    }

    public void HeroTurnMove()
    {
        //キャラが周りを回るかの判定
        if (!stoped[(int)Chara.hero]) 
        {
            Turn(chara_chair[(int)Chara.hero], chara[(int)Chara.hero], (int)Chara.hero); 
            
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
            Turn(chara_chair[(int)Chara.boss], chara[(int)Chara.boss], (int)Chara.boss);
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
    public void Turn(GameObject chair_chara, GameObject chara, int charanum)
    {
        if (timecs.time % 1 > 0.5f)
        {
            chara_render[charanum].sprite = chara_Splite[charanum, 0];
        }
        else
        {
            chara_render[charanum].sprite = chara_Splite[charanum, 1];
        }
        chair_chara.transform.Rotate(new Vector3(0, 0, 1), charamove * timecs.deltatime);
        chara.transform.Rotate(new Vector3(0, 0, -1), charamove * timecs.deltatime);
    }

}

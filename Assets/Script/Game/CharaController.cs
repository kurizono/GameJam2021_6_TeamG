using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    CharaTurn CharaTurncs;
    CharaStop CharaStopcs;

    [SerializeField]
    public GameObject chara;
    [SerializeField]
    public GameObject hero, boss;

    Vector3 chara_firstposi = new Vector3(0, 0, 0);
    Vector3 hero_firstposi = new Vector3(0, 1.8f, 0);
    Vector3 boss_firstposi = new Vector3(0, -1.8f, 0);

    public float time, deltatile;
    bool stoped = false; 


    // Start is called before the first frame update
    void Start()
    {
        CharaTurncs = GetComponent<CharaTurn>();
        CharaStopcs = GetComponent<CharaStop>();

        time = 0;

        hero.transform.localPosition = hero_firstposi;
        boss.transform.localPosition = boss_firstposi;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        deltatile = Time.deltaTime;

        //キャラが周りを回るかの判定
        if (! stoped) { CharaTurncs.Turn(); }
        if (CharaStopcs.Stop()){ stoped = true; }
    }
}

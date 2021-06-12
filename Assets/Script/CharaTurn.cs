using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//魔王と勇者が回る
public class CharaTurn : MonoBehaviour
{
    public CharaController CharaControllercs;

    float charamove = 45;

    void Start()
    {
        CharaControllercs =  GetComponent<CharaController>();
    }

    public void Turn()
    {
        CharaControllercs.chara.transform.Rotate(new Vector3(0, 0, 1), charamove * CharaControllercs.deltatile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStop : MonoBehaviour
{
    public CharaController CharaControllercs;

    void Start()
    {
        CharaControllercs = GetComponent<CharaController>();
    }


    // Update is called once per frame

    public bool Stop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{

    [SerializeField]
    public static Sprite[] CharaFaces;
    [SerializeField]
    public string[] CharaName;

    public void First()
    {
        GetCharaFaces();
    }

    public void GetCharaFaces()
    {
        Object[] CharaFaces_obj = Resources.LoadAll("Face", typeof(Sprite));
        CharaFaces = new Sprite[CharaFaces_obj.Length];
        CharaName = new string[CharaFaces_obj.Length];
        for(int i = 0; i < CharaFaces_obj.Length; i++)
        {
            CharaFaces[i] = CharaFaces_obj[i] as Sprite;
            CharaName[i] = CharaFaces[i].name;
        }
    }

    //キャラ画像の名前と数字を紐づける
    public int GetFaceNum(string name)
    {
        for (int i = 0; i < CharaFaces.Length; i++)
        {
            if (name == CharaFaces[i].name)
            {
                return i;
            }
        }
        Debug.LogError(name + "の対応する画像がありません。");
        return 0;
    }
}

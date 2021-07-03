using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Speak : MonoBehaviour
{
    Tutorial_Controller controllercs;
    TimeContoller Timecs;
    Tutorial_Talk talkcs;

    //　読み込んだテキストを出力するUIテキスト
    [SerializeField]
    private Text dataText;
    //表情
    [SerializeField]
    private SpriteRenderer Face;

    //一文字一文字の表示する速さ
    float novelSpeed;
    //テキスト間の間
    float novelSpace;

    //　文字を配列に入れる
    private string[] presplitText, mainsplitText;
    //　現在表示中テキスト番号
    private int textCount;
    //現在表示中文字番号
    private int charCount;

    private int[] pretalkFace, maintalkFace;


    //次の文字を表示する時間
    private float NextCharTime = 0;

    //テキストを全て表示しきったか否かの判定
    bool finish;
    //電話がかかったかの判定用
    int tell = 0;

    private void Awake()
    {
        controllercs = GetComponent<Tutorial_Controller>();
        Timecs = GetComponent<TimeContoller>();
        talkcs = GetComponent<Tutorial_Talk>();
    }

    void Start()
    {
        //テキストの速さを取得
        novelSpeed = Timecs.GetnovelSpeed();
        novelSpace = Timecs.GetnovelSpace();
        

        //テキストの数
        presplitText = new string[talkcs.pretext.Length];
        mainsplitText = new string[talkcs.maintext.scenariomain.Length];
        for(int i = 0; i < presplitText.Length; i++)
        {
            presplitText[i] = talkcs.pretext[i].talk;
        }
        for(int i = 0; i < mainsplitText.Length; i++)
        {
            mainsplitText[i] = talkcs.maintext.scenariomain[i].talk;
        }

        Reset();

        //顔
        pretalkFace = new int[presplitText.Length];
        maintalkFace = new int[mainsplitText.Length];
        for (int i = 0; i < pretalkFace.Length; i++)
        {
            pretalkFace[i] = talkcs.pretext[i].face;

        }
        for(int i = 0; i < mainsplitText.Length; i++)
        {
            maintalkFace[i] = talkcs.maintext.scenariomain[i].face;
        }
        Face.sprite = FaceController.CharaFaces[pretalkFace[0]];

    }

    public void Reset()
    {
        textCount = 0;
        charCount = 0;
        dataText.text = "";
        finish = false;
    }

    //PreからMainにうつる
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(novelSpace);
        controllercs.LevelChange(1);
        Reset();
        Face.sprite = FaceController.CharaFaces[maintalkFace[0]];
    }


    public void PreNovel(float time)
    {
        if (time > NextCharTime && !finish)
        {
           NextCharTime = Novel(presplitText, pretalkFace);
            if (textCount >= 2 && charCount >= 10 && tell == 0)
            {
                controllercs.TellPhone();
                tell++;
            }
        }
        if (finish)
        {
            StartCoroutine(Wait());
        }
    }
    public void MainNovel(float time)
    {
        if (time > NextCharTime && !finish)
        {
            NextCharTime = Novel(mainsplitText, maintalkFace);
        }
    }

    //表示メソッド
    private float Novel(string[] splitText, int[] talkFace)
    {
        //文字の変更
        //文字の追加
        if (splitText[textCount].Length > charCount)
        {
            if (charCount == 0)
            {
                dataText.text = "";
            }
            dataText.text += splitText[textCount][charCount];
            charCount++;
        }
        //テキストが変わる
        else
        {
            Face.sprite = FaceController.CharaFaces[talkFace[textCount]];
            textCount++;
            charCount = 0;
        }

        //時間の追加
        //文字が増える
        if (splitText[textCount].Length > charCount)
        {
            NextCharTime += novelSpeed;

        }
        //テキストが変わる
        else if (textCount < splitText.Length - 1)
        {
            NextCharTime += novelSpace;
        }
        //テキスト終了時
        else
        {
            finish = true;
        }
        return NextCharTime;

    }
}

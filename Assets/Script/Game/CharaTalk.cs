using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadTalk;

namespace ReadText
{
	public class CharaTalk : MonoBehaviour
	{
		TimeContoller Timecs;

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
		private string[] splitText;
		//　現在表示中テキスト番号
		private int textCount;
		//現在表示中文字番号
		private int charCount;

		private int[] onetalkFace;
 		
		
		//次の文字を表示する時間
		private float NextCharTime = 0;

		//テキストを全て表示しきったか否かの判定
		bool finish;

        private void Awake()
        {
			Timecs = GetComponent<TimeContoller>();
		}

        void Start()
		{
			//テキストの速さを取得
			novelSpeed = Timecs.GetnovelSpeed();
			novelSpace = Timecs.GetnovelSpace();
			
			//テキストの数
			int text_num = TalkController.scenarios[StorySelect.scenario_now].scenariomain.Length;
			splitText = new string[text_num];
			for(int i = 0; i < text_num; i++)
            {
				splitText[i] = TalkController.scenarios[StorySelect.scenario_now].scenariomain[i].talk;
			}
			textCount = 0;
			charCount = 0;
			dataText.text = "";

			//顏
			onetalkFace = new int[text_num];
			for(int i = 0; i < text_num; i++)
            {
				onetalkFace[i] = TalkController.scenarios[StorySelect.scenario_now].scenariomain[i].face;
            }
			Face.sprite = FaceController.CharaFaces[onetalkFace[0]];

			finish = false;

		}

		//表示し続けるかの判定
		public void NovelContinue(float time)
        {
			if (time > NextCharTime && !finish)
            {
				Novel();
            }
        }

		//表示メソッド
		private void Novel()
        {
			//文字の変更
			//文字の追加
			if (splitText[textCount].Length > charCount)
			{
				if(charCount == 0)
                {
					dataText.text = "";
                }
				dataText.text += splitText[textCount][charCount];
				charCount++;
			}
			//テキストが変わる
            else
            {
				Face.sprite = FaceController.CharaFaces[onetalkFace[textCount]];
				textCount++;
				charCount = 0;
            }

			//時間の追加
			//文字が増える
			if(splitText[textCount].Length > charCount)
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

		}
	}
}
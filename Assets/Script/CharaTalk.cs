using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadTalk;

namespace ReadText
{
	public class CharaTalk : MonoBehaviour
	{
		//　読み込んだテキストを出力するUIテキスト
		[SerializeField]
		private Text dataText;
		//一文字一文字の表示する速さ
		float novelSpeed = 0.05f;
		//テキスト間の間
		float novelSpace = 0.5f;

		//　文字を配列に入れる
		private string[] splitText;
		//　現在表示中テキスト番号
		private int textCount;

        void Start()
		{
			int text_num = TalkController.scenarios[StorySelect.scenario_now].scenariomain.Length;
			splitText = new string[text_num];
			for(int i = 0; i < text_num; i++)
            {
				splitText[i] = TalkController.scenarios[StorySelect.scenario_now].scenariomain[i].talk;
			}
			textCount = 0;
			dataText.text = splitText[textCount];
			StartCoroutine(Novel());
		}


		private IEnumerator Novel()
		{
			int messageCount = 0; //現在表示中の文字数
			dataText.text = ""; //テキストのリセット
			while (splitText[textCount].Length > messageCount)//文字をすべて表示していない場合ループ
			{
				dataText.text += splitText[textCount][messageCount];//一文字追加
				messageCount++;//現在の文字数
				yield return new WaitForSeconds(novelSpeed);//任意の時間待つ
			}

			yield return new WaitForSeconds(novelSpace);//任意の時間待つ

			textCount++; //次の会話文配列
			if (textCount < splitText.Length)//全ての会話を表示したか
			{
				StartCoroutine(Novel());
			}
		}

	}
}
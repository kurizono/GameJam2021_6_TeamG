using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadText
{
	public class CharaTalk : MonoBehaviour
	{
		//　読み込んだテキストを出力するUIテキスト
		[SerializeField]
		private Text dataText;

		//　Resourcesフォルダから直接テキストを読み込む
		private string loadText;
		//　改行で分割して配列に入れる
		private string[] splitText;
		//　現在表示中テキスト番号
		private int textNum;

		void Start()
		{
			loadText = (Resources.Load("memo2", typeof(TextAsset)) as TextAsset).text;
			splitText = loadText.Split(char.Parse("\n"));
			textNum = 0;
			dataText.text = "右クリックでResourcesフォルダ内のテキストファイル読み込みしたテキストが表示されます。";
		}

		void Update()
		{

			if (Input.GetButtonDown("Fire1"))
			{
				if (splitText[textNum] != "")
				{
					dataText.text = splitText[textNum];
					textNum++;
					if (textNum >= splitText.Length)
					{
						textNum = 0;
					}
				}
				else
				{
					dataText.text = "";
					textNum++;
				}
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

//簡易ナンプレ問題の作成
public class RPGSudoku : MonoBehaviour {
	int i,j,k = 0;
	/* 加算 */
	int sum = 0;

	/* 1~81の数字を格納 */
	int [,,] Number = new int[3,3,9];
	/* 配列交換 */
	int[,] ChangeNumber = new int[3, 9];
	int[,,] Solution = new int[3, 3, 9];

	/* 行 */
	int RowNumber_1 = 0;	
	int RowNumber_2 = 0;	
	int BlockRow = 0;
	/* 列 */
	int ColumnNumber_1 = 0;
	int ColumnNumber_2 = 0;
	int BlockColumn = 0;

	public static int ConboCount = 0;
	int ShuffleCount = 0;

	int MaskNumber_1 = 0;
	int MaskNumber_2 = 0;
	int MaskNumber_3 = 0;
	int MaintainNumber_1 = 0;
	int MaintainNumber_2 = 0;
	int MaintainNumber_3 = 0;
	int TextNumber = 0;

	//経過時間
	float GameTimer = 0.0f;
	//時間経過制御
	bool TimerStartFlag = false;
	/* */
	bool LoopTrigger = false;
	//出力Uiをキャッシュ
	GameObject NumberObj;
	GameObject NumberImageObj;
	//Sprite
	public Sprite BakuhaSprite;
	public Sprite NormalSprite;
	//Text
	public Text GameTimerText;	//時間表示
	public Text ConboText;		//正解(コンボ)数表示
	//Audio
	AudioSource audioSource;
	public AudioClip CorrectSE;
	//
	public GameObject ThisGameObject;

	void Start(){
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource>();
		GameTimer = 0.0f;

	}

	void Update(){
		if (TimerStartFlag) {
			//1フレーム毎の毎秒を取得していく 連続正解に応じてスピードを上げる
			GameTimer += Time.deltaTime + (0.001f * (ConboCount));
			GameTimerText.text = "Time:" + Mathf.FloorToInt (GameTimer);
		}

	}

	//敵エネミー選択後各処理を開始する
	public IEnumerator SudokuStart()
	{
		ThisGameObject.SetActive (true);

		yield return StartCoroutine (NumberSet ());

		if (LoopTrigger) {
			yield return StartCoroutine (SudokuStart());
		} 

		ConboCount = 0;

		ThisGameObject.SetActive (false);

		yield return null;


	}

	public IEnumerator NumberSet()
	{
		sum = 0;

		//1~9をずらしながら格納
		for (i = 0; i < 3; i++) {
			for (j = 0; j < 3; j++) {
				for (k = 0; k < 9; k++) {
					sum = (i * 3) + j + k + 1;
					if (sum <= 9) {
						Number [i, j, k] = sum;

					} else if (sum >= 10) {
						sum -= 9;
						Number [i, j, k] = sum;
					}
				}
			}
		}

		//行と列と3x3ブロックの9つが合計で45になるよう 2列目と4列目 3列目と7列目 6列目と8列目の配列の数値を変える
		for (j = 0; j < 9; j++) {
			ChangeNumber [0, j] = Number [0, 1, j];
			Number [0, 1, j] = Number [1, 0, j];
			Number [1, 0, j] = ChangeNumber [0, j];

			ChangeNumber [1, j] = Number [0, 2, j];
			Number [0, 2, j] = Number [2, 0, j];
			Number [2, 0, j] = ChangeNumber [1, j];

			ChangeNumber [2, j] = Number [1, 2, j];
			Number [1, 2, j] = Number [2, 1, j];
			Number [2, 1, j] = ChangeNumber [2, j];

		}
		//-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --

		yield return StartCoroutine (Shuffle ());

		yield return null;

	}

	public IEnumerator Shuffle()
	{
		ShuffleCount = 0;
		do {

			//3行・列まとめてシャッフルするか,１行・列ずつシャッフルするかをランダムで決定
			switch (UnityEngine.Random.Range (1, 3)) {

			//3行・列まとめてシャッフル
			case 1:

				//行か列のどちらかシャッフルするかをランダムで決定
				switch (UnityEngine.Random.Range (1, 3)) {
				//行でシャッフル
				case 1:
					do {
						//どの3行かをランダムに決定
						RowNumber_1 = UnityEngine.Random.Range (0, 3); //0～2をランダムで生成
						RowNumber_2 = UnityEngine.Random.Range (0, 3); //0～2をランダムで生成
	
					} while(RowNumber_1 == RowNumber_2);

					//2つの3行を交換
					for (k = 0; k < 3; k++) {
						for (j = 0; j < 9; j++) {
							ChangeNumber [0, j] = Number [RowNumber_1, k, j];
							Number [RowNumber_1, k, j] = Number [RowNumber_2, k, j];
							Number [RowNumber_2, k, j] = ChangeNumber [0, j];
						}
					}

					break;

					//列でシャッフル
				case 2:
					do {
						//どの3列かをランダムに決定 (0~2 3~5 6~8)
						ColumnNumber_1 = (UnityEngine.Random.Range (0, 3) * 3); //0,3,6をランダムで生成
						ColumnNumber_2 = (UnityEngine.Random.Range (0, 3) * 3); //0,3,6をランダムで生成

					} while(ColumnNumber_1 == ColumnNumber_2);



					//2つの3列を交換
					do {
						for (i = 0; i < 3; i++) {
							for (k = 0; k < 3; k++) {
								ChangeNumber [i, k] = Number [i, k, ColumnNumber_1];
								Number [i, k, ColumnNumber_1] = Number [i, k, ColumnNumber_2];
								Number [i, k, ColumnNumber_2] = ChangeNumber [i, k];
							}
						}
						ColumnNumber_1++;
						ColumnNumber_2++;

					} while(ColumnNumber_1 % 3 != 0 && ColumnNumber_2 % 3 != 0);

					break;
				}

				break;

				//1行・列でシャッフル
			case 2:
				//行か列のどちらかシャッフルするかをランダムで決定
				switch (UnityEngine.Random.Range (1, 3)) {
				//行でシャッフル
				case 1:
					do {
						//どの行かをランダムに決定
						RowNumber_1 = UnityEngine.Random.Range (0, 3); //0～2をランダムで生成
						RowNumber_2 = UnityEngine.Random.Range (0, 3); //0～2をランダムで生成

						//3行を指定
						BlockRow = UnityEngine.Random.Range (0, 3); //0～2をランダムで生成

					} while(RowNumber_1 == RowNumber_2);


					//2つの行を交換
					for (j = 0; j < 9; j++) {
						//同じ最上位階層の中でしか交換ができない
						ChangeNumber [0, j] = Number [BlockRow, RowNumber_1, j];
						Number [BlockRow, RowNumber_1, j] = Number [BlockRow, RowNumber_2, j];
						Number [BlockRow, RowNumber_2, j] = ChangeNumber [0, j];
					}

					break;

					//列でシャッフル
				case 2:
					do {
						//列ブロックの基準を決める
						BlockColumn = (UnityEngine.Random.Range (0, 3) * 3); //0,3,6をランダムで生成

						//決められた列ブロックのどの列かを決定
						ColumnNumber_1 = BlockColumn + UnityEngine.Random.Range (0, 3); //0~2をランダムで生成
						ColumnNumber_2 = BlockColumn + UnityEngine.Random.Range (0, 3); //0~2をランダムで生成

					} while(ColumnNumber_1 == ColumnNumber_2);


					//2つの列を交換
					for (i = 0; i < 3; i++) {
						for (k = 0; k < 3; k++) {
							ChangeNumber [i, k] = Number [i, k, ColumnNumber_1];
							Number [i, k, ColumnNumber_1] = Number [i, k, ColumnNumber_2];
							Number [i, k, ColumnNumber_2] = ChangeNumber [i, k];
						}
					}
					break;
				}
				break;
			}
			ShuffleCount++;
		} while(ShuffleCount < 20);

		//シャッフルされたものが答えとなるので答えを別の配列に格納
		for (i = 0; i < 3; i++) {
			for (j = 0; j < 3; j++) {
				for (k = 0; k < 9; k++) {
					Solution [i, j, k] = Number [i, j, k];
				}
			}
		}

		yield return StartCoroutine(QuestionCreat());

		yield return null;

	}

	public IEnumerator QuestionCreat()
	{
		//答える場所をランダムに設定
		MaskNumber_1 = UnityEngine.Random.Range (0, 3);
		MaskNumber_2 = UnityEngine.Random.Range (0, 3);
		MaskNumber_3 = UnityEngine.Random.Range (0, 9);
		Number [MaskNumber_1, MaskNumber_2, MaskNumber_3] = 0;
		//答えとなる配列番号を保存
		MaintainNumber_1 = MaskNumber_1;
		MaintainNumber_2 = MaskNumber_2;
		MaintainNumber_3 = MaskNumber_3;


		//正解数(コンボ数）に応じて問題の難易度を変化させる
		//正解数３問以下
		if (ConboCount >= 0 && ConboCount <= 3) {

			//正解数４問以上　６問以下
		} else if (ConboCount > 3 && ConboCount <= 6) {
			//行の数値を一つ消す
			do {
				MaskNumber_3 = UnityEngine.Random.Range (0, 9);

			} while(Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] == 0);
			Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] = 10;


			//正解数７問以上　８問以下
		} else if (ConboCount > 6 && ConboCount <= 8) {
			//列の数値を一つ消す
			do {
				MaskNumber_1 = UnityEngine.Random.Range (0, 3);
				MaskNumber_2 = UnityEngine.Random.Range (0, 3);

			} while(Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] == 0);
			Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] = 10;


			//正解数９問以上　１１問以下
		} else if (ConboCount > 8 && ConboCount <= 11) {
			do {
				MaskNumber_3 = UnityEngine.Random.Range (0, 9);

			} while(Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] == 0);
			Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] = 10;

			do {
				MaskNumber_1 = UnityEngine.Random.Range (0, 3);
				MaskNumber_2 = UnityEngine.Random.Range (0, 3);
	
			} while(Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] == 0);
			Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] = 10;


			//正解数１２問以上　１４問以下
		} else if (ConboCount > 11 && ConboCount <= 14) {
			//3x3ブロックの数値を一つ消す
			do {
				MaskNumber_3 = UnityEngine.Random.Range (0, 9);

			} while(Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] == 0);
			Number [MaintainNumber_1, MaintainNumber_2, MaskNumber_3] = 10;

			do {
				MaskNumber_1 = UnityEngine.Random.Range (0, 3);
				MaskNumber_2 = UnityEngine.Random.Range (0, 3);

			} while(Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] == 0);
			Number [MaskNumber_1, MaskNumber_2, MaintainNumber_3] = 10;

			do {
				MaskNumber_2 = UnityEngine.Random.Range (0, 3);
				if (MaintainNumber_3 >= 0 && MaintainNumber_3 <= 2) {
					MaskNumber_3 = UnityEngine.Random.Range (0, 3);

				} else if (MaintainNumber_3 > 2 && MaintainNumber_3 <= 5) {
					MaskNumber_3 = UnityEngine.Random.Range (3, 6);

				} else if (MaintainNumber_3 > 5 && MaintainNumber_3 <= 8) {
					MaskNumber_3 = UnityEngine.Random.Range (6, 9);
				}
			} while(Number [MaintainNumber_1, MaskNumber_2, MaskNumber_3] == 0);
			Number [MaintainNumber_1, MaskNumber_2, MaskNumber_3] = 10;


			//正解数１５問以上
		} else if (ConboCount > 14) {

		}

		yield return StartCoroutine(Questioning ());

		yield return null;

	}

	//問題を出力
	public IEnumerator Questioning()
	{
		//問題を出題した段階でゲーム時間をリセット
		GameTimer = 0.0f;
		TimerStartFlag = true;

		TextNumber = 0;
		for (i = 0; i < 3; i++) {
			for (k = 0; k < 3; k++) {
				for (j = 0; j < 9; j++) {
					TextNumber++;
					NumberObj = GameObject.Find("AttackUI/SudokuNumber/Button" + TextNumber + "/" + TextNumber);
					NumberImageObj = GameObject.Find ("AttackUI/SudokuNumber/Button" + TextNumber);
					if (Number [i, k, j] == 0) {
						NumberImageObj.GetComponent<Image> ().sprite = NormalSprite;
						NumberObj.GetComponent<Text> ().color = Color.red;
						NumberObj.GetComponent<Text> ().text = "?";

					} else if (Number [i, k, j] == 10) {
						NumberImageObj.GetComponent<Image> ().sprite = BakuhaSprite;
						NumberObj.GetComponent<Text> ().text = "";

					} else if (Number [i, k, j] != 0 && Number [i, k, j] != 10) {
						NumberImageObj.GetComponent<Image> ().sprite = NormalSprite;
						NumberObj.GetComponent<Text> ().color = Color.black;
						NumberObj.GetComponent<Text> ().text = Number [i, k, j].ToString ();
					}
				}
			}
		}

		yield return StartCoroutine(Answer());

		yield return null;

	}

	public IEnumerator Answer()
	{
		//1~9のキーボード入力を待つ
		while (!Input.GetKeyUp (KeyCode.Alpha1) && !Input.GetKeyUp (KeyCode.Alpha2) && !Input.GetKeyUp (KeyCode.Alpha3) && !Input.GetKeyUp (KeyCode.Alpha4) && !Input.GetKeyUp (KeyCode.Alpha5) && !Input.GetKeyUp (KeyCode.Alpha6) && !Input.GetKeyUp (KeyCode.Alpha7) && !Input.GetKeyUp (KeyCode.Alpha8) && !Input.GetKeyUp (KeyCode.Alpha9)) {

			//10秒以下の場合,キー入力を受け付ける
			if (GameTimer <= 10.0f) {
				//1~9のキー入力処理
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 1;

				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 2;

				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 3;

				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 4;

				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 5;

				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 6;

				} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 7;

				} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 8;

				} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
					Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] = 9;

				}

				//10秒以上たった場合全体のループ処理を終了
			} else if (GameTimer > 10.0f) {
				yield return StartCoroutine (Finish ());
				LoopTrigger = false;
				yield break;

			}
			yield return null;
		}

		yield return StartCoroutine(CheckingAnswer ());

		yield return null;


	}

	public IEnumerator CheckingAnswer()
	{
		//時間の経過を停止
		TimerStartFlag = false;

		if (Number [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3] == Solution [MaintainNumber_1, MaintainNumber_2, MaintainNumber_3]) {
			ConboCount++;
			ConboText.text = ConboCount + "CONBO!!";
			audioSource.PlayOneShot (CorrectSE);
			LoopTrigger = true;
		} else {
			LoopTrigger = false;
		}

		yield return StartCoroutine (Finish ());

		yield return null;

	}

	public IEnumerator Finish()
	{
		//時間切れか答えを間違えた場合コンボ数をBattleDisposal関数のConboに渡す
		BattleDisposal battleDisposal = FindObjectOfType<BattleDisposal> ();
		BattleDisposal.Conbo = ConboCount;

		yield return null;
	}


}

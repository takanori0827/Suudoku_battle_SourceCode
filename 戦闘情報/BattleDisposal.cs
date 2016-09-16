using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

/* バトル処理 原則バトルは常に1vs1の物として作成するが最高1vs3まで可能 */
public class BattleDisposal : MonoBehaviour {
	/* 動的にゲームオブジェクトをアタッチする */
	[SerializeField] protected GameObject Command;		//コマンド
	[SerializeField] protected GameObject TextObject;	//表示テキスト
	public StageDataBase stageDataBase;

	/* シーン上のスクリプトをアタッチ */
	protected CharacterBase characterBase;
	protected EnemyBase enemyBase;

	/* 発生したダメージ値などが敵のステータス情報によって実際にいくつになったか */
	protected int CallBackValue = 0;

	/* 数独の正解数 */
	public static int Conbo = 0;

	/* スキルによって増加される倍率値 */
	public float AttackSkillValue = 0.0f;
	public float HealSkillValue = 0.0f;
	public float DefenceSkillValue = 0.0f;

	/* プレイヤーと敵情報をキャッシュ */
	protected GameObject[] EnemyTags = new GameObject[3];
	protected GameObject Player;

	/* 敵選択のGUIButtonのキャッシュ */
	public GameObject[] EnemySelectButton = new GameObject[3];

	/* どの敵に処理を与えるかをキャッシュ */
	protected GameObject TargetEnemy;
	public GameObject NullTargetObject;

	/* 数独スクリプト */
	public RPGSudoku rpgSudoku;

	/* 敵エネミーが存在しているか */
	bool isEnemy = false;

	public AudioClip Fanfare;


	public void Start()
	{
		/* シーン上のプレイヤー・敵ゲームオブジェクトの情報を取得 */
		EnemyTags = GameObject.FindGameObjectsWithTag ("Enemy");
		Player = GameObject.FindGameObjectWithTag ("Player");

		/* スクリプトをキャッシュ */
		characterBase = FindObjectOfType<CharacterBase> ();
		enemyBase = FindObjectOfType<EnemyBase> ();

		/* 敵の数によって敵選択を行えるボタンの数を表示する */
		for (int i = 0; i < EnemyTags.Length ; i++) 
		{
			EnemySelectButton [i].SetActive (true);
			EnemySelectButton [i].GetComponentInChildren<Text> ().text = EnemyTags [(EnemyTags.Length - 1) - i].name;
		}

		/* TargetEnemyオブジェクトにからのゲームオブジェクトを入れておく */
		TargetEnemy = NullTargetObject;


	}

	/* OnClick処理で敵を選択し,戦闘処理へ入る */
	public void SelectEnemy(int i)
	{
		TargetEnemy = EnemyTags [(EnemyTags.Length - 1) - i];
		StartCoroutine (PlayerTurn());
	}
		
	/* コマンド処理⇒プレイヤーの攻撃⇒モンスターの攻撃の順で処理をしていく */
	protected virtual IEnumerator PlayerTurn()
	{
		/* コマンドを非表示 */
		Command.SetActive (false);

		/* 攻撃回数を取得 */
		yield return StartCoroutine (rpgSudoku.SudokuStart());

		/* プレイヤーの攻撃処理 */
		for (int i = 0; i < Conbo; i++) 
		{
			if (TargetEnemy != null) {
				/* プレイヤーのCharacterBaseクラスを継承しているスクリプトにアクセス */
				yield return StartCoroutine (Player.GetComponent<CharacterBase> ().Attack (AttackSkillValue, TargetEnemy, Player));
				yield return StartCoroutine (ShowText (Player, TargetEnemy));
			}  else {
				TargetEnemy = NullTargetObject;
				break;
			}
		}
		AttackSkillValue = 0.0f;

		/* 敵エネミーが存在しているかいないかチェック */
		for (int i = 0; i < EnemyTags.Length; i++) {
			if (EnemyTags [i] == null) {
				isEnemy = false;
			}  else {
				isEnemy = true;
				break;
			}
		}

		/* 敵エネミーが存在しているなら相手の攻撃処理を開始 */
		if (isEnemy == true) {
			/* 敵エネミーの攻撃処理 */
			StartCoroutine (EnemyTurn ());
		}  else {
			/* 敵が全滅していればステージクリア */
			StartCoroutine (StageClear ());
		}
	
	}


	/* 敵モンスターの攻撃処理 */
	public IEnumerator EnemyTurn()
	{
		/* 存在するだけの敵の処理 */
		for (int i = 0; i < EnemyTags.Length; i++) {
			if (EnemyTags [i] != null) {
				/* 敵のEnemyBaseを継承しているスクリプトにアクセス */
				yield return StartCoroutine (EnemyTags [i].GetComponent<EnemyBase> ().Attack (Player));
				/* 敵の攻撃からはこのスクリプト内のShowText関数にアクセスするようにする */
				yield return StartCoroutine (this.gameObject.GetComponent<BattleDisposal> ().ShowText (EnemyTags [i], Player));
			}
		}

		if (Player.GetComponent<CharacterBase> ().CharacterHP <= 0) {
			yield return StartCoroutine (GameOver ());
		}

		/* コマンドを表示 */
		Command.SetActive (true);

		yield return null;
	}

	public IEnumerator StageClear()
	{
		/* ステージIDを取得しクリアフラグを建てる */
		StageID stageID = FindObjectOfType<StageID> ();
		TextObject.GetComponentInChildren<Text> ().text = stageDataBase.Stages[stageID.ID].stageName + "クリア!!!";
		foreach (var StageDataBase in FindObjectsOfType<StageDataBase>()) {
			StageDataBase.Stages [stageID.ID].stageClearFlag = true;
		}

		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("MainTitle");
	}

	public IEnumerator GameOver()
	{
		TextObject.GetComponentInChildren<Text> ().text = "ゲームオーバー　敵の攻撃を見つつコンボを重ねよう";

		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("MainTitle");
	}

	/* 実際の受けたダメージ値やヒール値をコールバックしてもらう */
	public void onValue(int Value)
	{
		CallBackValue = Value;
	}

	/* 攻撃時のダメージを画面に表示 */
	protected virtual IEnumerator ShowText(GameObject Attacker,GameObject Damager)
	{
		/* 受けたダメージを表示 */
		TextObject.SetActive (true);
		if (TargetEnemy == null) {
			TextObject.GetComponentInChildren<Text> ().text = "敵モンスターを倒しました";
		}  else {
			TextObject.GetComponentInChildren<Text> ().text = Attacker.name + "→" + Damager.name + " ? + CallBackValue + "?amage";
		}

		/* ３秒後次の攻撃処理に戻る */
		yield return new WaitForSeconds (3.0f);

		/* ダメージの表示を非表示 */
		TextObject.GetComponentInChildren<Text>().text = "";

		yield return null;
	}
		
}


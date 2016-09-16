using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

/* 基本となる敵ベース情報 */
public class EnemyBase : MonoBehaviour {
	/* 敵ステータス */
	[SerializeField] protected int ID;					//ステータスID
	[SerializeField] protected int MaxHp;				//最大Hp
	[SerializeField] protected int MaxMp;				//最大Mp
	[SerializeField] public int AttackPower;			//攻撃力	
	[SerializeField] public int DefencePower;		//防御力
	protected int damage = 0;

	/* データベースキャッシュ */
	[SerializeField] protected GameObject characterDataBase;	
	[SerializeField] protected CharacterBase characterBase;

	public BattleDisposal battleDisposal;
	[SerializeField] protected GameObject PlayerObject;

	protected string[] RandomElement = { "Unattributed", "Fire", "Water", "Earth", "Thunder", "Wind", "Ice" };

	/* デリゲート */
	public delegate void DamageCallBack(int Damage);

	[SerializeField] protected AudioSource audioSource;
	[SerializeField] protected AudioClip AttackSE;
	[SerializeField] protected Image DamageImage;
	[SerializeField] protected Text text;


	/* 現在のHP */
	public int EnemyHP 
	{
		get {
			return _EnemyHP;
		}
		set {
			/* それぞれの敵キャラの最大体力以上の数値は設定されない */
			_EnemyHP = Mathf.Min (MaxHp, value);

			/* 死亡処理 */
			if (EnemyHP <= 0) {
				characterDataBase.GetComponent<CharacterDataBase> ().characters [ID].CharacterFlag = true;
				Destroy (this.gameObject);
			}

		}

	}

	/* 現在のMP */
	public int EnemyMP
	{
		get {
			return _EnemyMP;
		}
		set {
			/* それぞれの敵キャラの最大舞力は設定できない */
			_EnemyMP = Mathf.Min(MaxMp,value);

			/* MP枯渇処理 */
			if (EnemyMP <= 0) {
				Debug.Log ("MPがありません　魔法攻撃は撃てませんでした");

			}

		}
	}

	private int _EnemyHP;
	private int _EnemyMP;
	private int _EnemyAttack;
	private int _EnemyDefence;

	/* 属性タイプの列挙 */
	public enum ElementType {
		Unattributed,	//無属性
		Fire,			//火属性
		Water,			//水属性
		Earth,			//土属性
		Thunder,		//雷属性
		Wind,			//風属性
		Ice,			//氷属性
	}

	[SerializeField] protected ElementType elementType;



	/* 敵情報の取得 */
	public virtual void GetStatusData()
	{
		/* 各キャラデータをリスト化したデータベースからIDを設定し情報を取得する */
		MaxHp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterHP;				//このキャラの最大HP
		MaxMp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterMP;				//このキャラの最大MP
		AttackPower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterAttack;	//このキャラの最大AP
		DefencePower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterDefence;	//このキャラの最大DP

		EnemyHP = MaxHp;	//HPに適応
		EnemyMP = MaxMp;	//MPに適応
	}

	/* ダメージを敵に与える */
	public virtual IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,21);

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}
		
	/* ダメージを受ける処理 */
	public virtual IEnumerator Damage(float damage, DamageCallBack damageCallBack, GameObject PlayerCharacter)
	{
		/* 受け取ったダメージを防御力で除算を行い,HitPointからダメージ値を減算する */
		EnemyHP -= Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower;

		/* 実際にHitPointから乗算されたダメージをコールバック */
		damageCallBack (Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower);

		yield return null;
	}

	/* HPの割合で行動を変化させる */
	public virtual IEnumerator PhaseCheck(GameObject PlayerCharacter)	{yield return null;}

	/* 属性変更 */
	public virtual IEnumerator ChangeElement(){
		//文字列をランダムに取得
		var rand = new System.Random ();
		var element = RandomElement [rand.Next (0, 7)];

		//無属性へ変化
		if (element == "Unattributed") {
			elementType = ElementType.Unattributed;
			text.text = "無の属性パワーが拡散している ";
			//火属性へ変化
		} else if (element == "Fire") {
			elementType = ElementType.Fire;
			text.text = "火の属性パワーが拡散している 火の属性をまとい敵の攻撃に備えよ";
			//水属性へ変化
		} else if (element == "Water") {
			elementType = ElementType.Water;
			text.text = "水の属性パワーが拡散している 水の属性をまとい敵の攻撃に備えよ";
			//土属性へ変化
		} else if (element == "Earth") {
			elementType = ElementType.Earth;
			text.text = "地の属性パワーが拡散している 土の属性をまとい敵の攻撃に備えよ";
			//雷属性へ変化
		} else if (element == "Thunder") {
			elementType = ElementType.Thunder;
			text.text = "雷の属性パワーが拡散している 雷の属性をまとい敵の攻撃に備えよ";
			//闇属性へ変化
		} else if (element == "Wind") {
			elementType = ElementType.Wind;
			text.text = "風の属性パワーが拡散している 風の属性をまとい敵の攻撃に備えよ";
			//光属性へ変化
		} else if (element == "Ice") {
			elementType = ElementType.Ice;
			text.text = "氷の属性パワーが拡散している 氷の属性をまとい敵の攻撃に備えよ";
		}

		yield return new WaitForSeconds (3.0f);

		yield return null;
	}


}

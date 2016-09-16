using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

/* プレイヤーキャラクターの基本設定 */
public class CharacterBase : MonoBehaviour {
	/* ステータス情報 */
	[SerializeField] protected int ID;						//ステータスID
	[SerializeField] protected int MaxHp;					//最大HP	
	[SerializeField] protected int MaxMp;					//最大MP
	[SerializeField] protected int AttackPower;				//攻撃力
	[SerializeField] protected int DefencePower;			//防御力
	[SerializeField] protected float damage;				//ダメージ

	/* 各値 */
	float HealValue = 0.0f;									//ヒール値
	float DefenceValue=0.0f;								//防御値

	/* データベースをキャッシュ */
	[SerializeField] protected GameObject characterDataBase;	
	[SerializeField] protected GameObject database;				
				
	/* Audioやparticleをキャッシュ */
	[SerializeField] protected AudioSource audioSource;	
	[SerializeField] protected AudioClip[] AttackSound = new AudioClip[4];

	/* デリゲート */
	public delegate void ValueCallBack(int Value);	

	/* BattleDisposalクラスをキャッシュ */
	public BattleDisposal battleDisposal;

	public Image DamageImage;

	/* 現在のHP */
	public int CharacterHP {
		get {
			return _CharacterHP;
		}
		set {
			//
			_CharacterHP = Mathf.Min (MaxHp, value);

			//
			if (CharacterHP <= 0) {

			}

		}

	}

	/* 現在のMP */
	public int CharacterMP {
		get {
			return _CharacterMP;
		}
		set {
			
			_CharacterMP = Mathf.Min(MaxMp,value);

			if (CharacterMP <= 0) {

			}

		}
	}

	private int _CharacterHP;
	private int _CharacterMP;

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
		
	public ElementType elementType;

	/* 各キャラデータをリスト化したデータベースからIDを設定し情報を取得する */
	public virtual void GetStatusData()
	{
		MaxHp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterHP;				//このキャラの最大HP
		MaxMp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterMP;				//このキャラの最大MP
		AttackPower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterAttack;	//このキャラの最大AP
		DefencePower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterDefence;	//このキャラの最大DP

		CharacterHP = MaxHp;	//HPに適応
		CharacterMP = MaxMp;	//MPに適応
	}

	/* 攻撃を何回行うのかを引数として受け取り,回数分敵エネミーにダメージ処理を行う */
	public virtual IEnumerator Attack(float SkillValue,GameObject TargetEnemy, GameObject Player)
	{
		/* 元々の攻撃力とスキル発動の倍率を乗算し乱数１～１０を加算してダメージの幅を作る */
		damage = AttackPower * 1.5f * Mathf.Max(1.0f,SkillValue) + UnityEngine.Random.Range(1,11);

		audioSource.PlayOneShot (AttackSound [0]);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* どの敵にダメージを与えるのかを引数として受け取り,その敵に与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (TargetEnemy.GetComponent<EnemyBase> ().Damage (damage, battleDisposal.onValue, Player));

		yield return null;
	}

	/* ダメージ処理 */
	public virtual IEnumerator Damage(int damage, ValueCallBack valueCallBack)
	{
		/* 0以上の数値 */
		CharacterHP -= Mathf.Max (0, damage) / DefencePower;

		/* 実際に処理されたダメージ値をコールバック */
		valueCallBack (Mathf.Max (0, damage) / DefencePower);

		yield return null;
	}

	/* 防御力の値を加算 */
	public virtual IEnumerator Defence (float SkillValue, ValueCallBack valueCallBack)
	{
		/* 防御力を1~3の間でランダムに加算 */
		DefenceValue = UnityEngine.Random.Range (1, 4) * Mathf.Max (1.0f, SkillValue);
		DefencePower += Mathf.FloorToInt(DefenceValue);

		/* 追加防御力値をコールバック */
		valueCallBack (Mathf.FloorToInt(DefenceValue));

		yield return null;
	}
		
	/* 回復処理 */
	public virtual IEnumerator Heal(float SkillValue,ValueCallBack valueCallBack)
	{
		/* HPを1~20の間でランダムに加算 */
		HealValue = UnityEngine.Random.Range (1, 11) * Mathf.Max(1.0f,SkillValue);
		CharacterHP += Mathf.FloorToInt(HealValue);

		/* ヒール値をコールバック */
		valueCallBack (Mathf.FloorToInt(HealValue));

		yield return null;
	}




}

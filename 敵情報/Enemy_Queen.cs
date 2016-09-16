using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/* Enemybaseクラスを継承 ID:20*/
public class Enemy_Queen : EnemyBase {
	int SumDamage = 0;
	bool KilledTrigger = false;
	public AudioClip PowerUpSE;

	/* ダメージを敵に与える */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* HPの割合で攻撃方法を変化させる */
		yield return StartCoroutine (PhaseCheck (PlayerCharacter));

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;
	
		yield return null;
	}

	/* ダメージを受ける処理 */
	public override IEnumerator Damage(float damage, DamageCallBack damageCallBack, GameObject PlayerCharacter)
	{
		/* 受け取ったダメージを防御力で除算を行い,HitPointからダメージ値を減算する */
		EnemyHP -= Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower;

		/* 実際にHitPointから乗算されたダメージをコールバック */
		damageCallBack (Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower);

		/* boolがtrueの場合総ダメージを求める */
		if(KilledTrigger){
			SumDamage += Mathf.Max (0, Mathf.FloorToInt (damage)) / DefencePower;
		}

		yield return null;
	}

	/* HPの割合で行動を変化させる */
	public override IEnumerator PhaseCheck(GameObject PlayerCharacter)
	{
		if (MaxHp / 2 < EnemyHP && EnemyHP <= MaxHp)
		{
			yield return StartCoroutine (Phase1 (PlayerCharacter));
		}
		else if(MaxHp / 3  < EnemyHP && EnemyHP <= MaxHp / 2)
		{
			yield return StartCoroutine( Phase2 (PlayerCharacter));
		}
		else if(0  < EnemyHP && EnemyHP <= MaxHp / 3)
		{
			yield return StartCoroutine( Phase3 (PlayerCharacter));
		}

		yield return null;
	}

	public IEnumerator Phase1(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,21);

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}

	public IEnumerator Phase2(GameObject PlayerCharacter)
	{
		Debug.Log ("フェイズ2");
		if (elementType != ElementType.Unattributed) 
		{
			yield return StartCoroutine (CheckElementDamage (PlayerCharacter));
		}

		/* 属性をチェンジ */
		yield return StartCoroutine( ChangeElement ());

		yield return null;
	}

	public IEnumerator Phase3(GameObject PlayerCharacter)
	{
		Debug.Log ("フェイズ３");
		if (!KilledTrigger)	{
			text.text = "全属性の力が集中している　ダメージを与えて拡散させよう";
			audioSource.PlayOneShot (PowerUpSE);
			damage = AttackPower + UnityEngine.Random.Range (1, 21);
			yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			KilledTrigger = true;
		} else {
			if (SumDamage < 40) {
				text.text = "拡散に失敗した";
				damage = 9999;
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

			} else if (SumDamage >= 40) {
				text.text = "拡散に成功した";
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}
		}
		SumDamage = 0;

		yield return new WaitForSeconds (2.0f);

		yield return null;
	}

	/* プレイヤーキャラクターが同属性以外の場合追加ダメージを与える */
	public IEnumerator CheckElementDamage(GameObject PlayerCharacter){
		damage = AttackPower * 5;

		if (elementType == ElementType.Fire) 
		{
			text.text = "火属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Fire) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}
		}else if (elementType == ElementType.Water) {
			text.text = "水属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Water) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}

		}else if (elementType == ElementType.Earth) {
			text.text = "土属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Earth) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}

		}else if (elementType == ElementType.Thunder) {
			text.text = "雷属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Thunder) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}

		}else if (elementType == ElementType.Wind) {
			text.text = "風属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Wind) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}

		}else if (elementType == ElementType.Ice) {
			text.text = "氷属性以外爆発した";
			if (PlayerCharacter.GetComponent<CharacterBase> ().elementType != CharacterBase.ElementType.Ice) {
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			} else {
				damage = AttackPower + UnityEngine.Random.Range (1, 21);
				yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
			}
		}
		yield return new WaitForSeconds (2.0f);

		yield return null;
	}

}

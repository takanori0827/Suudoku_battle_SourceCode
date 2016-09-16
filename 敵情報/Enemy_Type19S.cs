using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Type19S : EnemyBase {
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,11);

		if (elementType != ElementType.Unattributed) {
			yield return StartCoroutine (CheckElementDamage (PlayerCharacter));
		}

		/* 属性をチェンジ */
		yield return StartCoroutine (ChangeElement ());

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_BlackKnight : EnemyBase {
	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		AttackPower += 2;
		text.text = "攻撃力を上げてきた";
		yield return new WaitForSeconds (2.0f);

		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,11);

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}
}

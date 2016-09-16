using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Touzoku : EnemyBase {
	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		text.text = "見えない攻撃を仕掛けてきた ？ダメージを喰らった";
		/* 元々の攻撃力に1~5の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,6);
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

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

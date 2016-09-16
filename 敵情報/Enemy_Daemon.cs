using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Daemon : EnemyBase {
	public int DeathCount = 0;

	/* ダメージを敵に与える */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		DeathCount++;
		text.text = "壁が迫ってくる　残り" + (10 - DeathCount) + "ターン";

		yield return new WaitForSeconds (2.0f);

		if (DeathCount == 10) {
			damage = 9999;

			audioSource.PlayOneShot (AttackSE);
			DamageImage.enabled = true;
			yield return new WaitForSeconds (0.1f);
			DamageImage.enabled = false;

			/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
			yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
		} else {
			/* 元々の攻撃力に1~6の乱数を用いてダメージの幅を作る */
			damage = AttackPower + UnityEngine.Random.Range (1, 6);

			audioSource.PlayOneShot (AttackSE);
			DamageImage.enabled = true;
			yield return new WaitForSeconds (0.1f);
			DamageImage.enabled = false;

			/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
			yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
		}

		yield return null;
	}
}

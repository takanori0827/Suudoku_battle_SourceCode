using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_LowKaspe : EnemyBase {
	int TurnCount = 0;
	int SumDamage = 0;
	bool DeathTrigger = false;

	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~11の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,11);
		TurnCount++;

		if (DeathTrigger) {
			if (SumDamage > 30) {
				text.text = "アルティメットバーストモードキドウ シッパイ";
				DeathTrigger = false;
				damage = 0;
				SumDamage = 0; 
			} else if(SumDamage < 30 && SumDamage > 0) {
				text.text = "アルティメットバーストモードキドウ";
				damage = 9999;
			}
		}
		yield return new WaitForSeconds (2.0f);

		if (TurnCount == 1 || TurnCount % 3 == 0) {
			text.text = "アルティメットバーストモードキドウ　ツギノフェイズ　デ　バクゲキ　シマス";
			DeathTrigger = true;
			damage = 0;
		}
		yield return new WaitForSeconds (2.0f);

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
	}

	/* ダメージを受ける処理 */
	public override IEnumerator Damage(float damage, DamageCallBack damageCallBack, GameObject PlayerCharacter)
	{
		/* 受け取ったダメージを防御力で除算を行い,HitPointからダメージ値を減算する */
		EnemyHP -= Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower;

		SumDamage += Mathf.Max (0, Mathf.FloorToInt (damage)) / DefencePower;

		/* 実際にHitPointから乗算されたダメージをコールバック */
		damageCallBack (Mathf.Max (0, Mathf.FloorToInt(damage)) / DefencePower);

		yield return null;
	}
}

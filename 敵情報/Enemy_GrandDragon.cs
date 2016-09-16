using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_GrandDragon : EnemyBase {
	public int  DeathCount = 0; 
	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,21);

		DeathCount++;
		text.text = "竜の怒りまで　残り" + (20 - DeathCount) + "ターン";
		yield return new WaitForSeconds (2.0f);

		if (DeathCount % 3 == 0) {
			text.text = "竜は力を貯めこみ始めた　攻撃力が少し下がった";
			AttackPower -= 2;
		}else if(DeathCount % 4 == 0){
			text.text = "龍は力を貯めこみ始めた　防御力が少し下がった";
			DefencePower -= 2;
		}else if(DeathCount == 20){
			text.text = "竜の怒りが爆発した！！";
			damage = 9999;
		}
		yield return new WaitForSeconds (2.0f);
		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}


}

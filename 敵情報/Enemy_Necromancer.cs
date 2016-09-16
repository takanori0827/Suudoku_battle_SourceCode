using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Necromancer : EnemyBase {
	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		switch (UnityEngine.Random.Range (0, 4)) {
		case 0:
			AttackPower += 2;
			text.text = "攻撃力を上げた";
			break;

		case 1:
			DefencePower += 2;
			text.text = "防御力を上げた";
			break;

		case 2:
			EnemyHP += 5;
			text.text = "HPを回復した";
			break;

		case 3:
			text.text = "呪文を間違えた　効果なし";
			break;

		}
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

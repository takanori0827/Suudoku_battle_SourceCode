using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_FalseStone : EnemyBase {
	/* 全体のステータスアップと攻撃処理 */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 敵全体のステータスアップ */
		foreach (var AllStatusUp in FindObjectsOfType<EnemyBase>()) {
			AllStatusUp.AttackPower += 1;
			AllStatusUp.DefencePower += 1;
		}
		text.text = "全体の攻撃力が上がった";
		yield return new WaitForSeconds (2.0f);
		text.text = "全体の防御力が上がった";
		yield return new WaitForSeconds (2.0f);
			
		/* 元々の攻撃力に1~11の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,11);		/* 元々の攻撃力に1~21の乱数を用いてダメージの幅を作る */

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}
}

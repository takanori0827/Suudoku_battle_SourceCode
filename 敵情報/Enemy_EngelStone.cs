using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_EngelStone : EnemyBase {
	/* ダメージを敵に与える */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		foreach (var AllHeal in FindObjectsOfType<EnemyBase>()) {
			AllHeal.EnemyHP += 1000;
		}
		text.text = "HPが全回復されてしまった";
		yield return new WaitForSeconds (2.0f);

		damage = 0;
		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}
}

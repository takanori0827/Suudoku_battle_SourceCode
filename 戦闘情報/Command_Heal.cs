using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Command_Heal : BattleDisposal {

	/* 回復ボタンのOnClick処理 */
	public void CoroutineStart()
	{
		StartCoroutine (PlayerTurn());
	}

	protected override IEnumerator PlayerTurn ()
	{
		/* コマンドを非表示 */
		Command.SetActive (false);
	
		yield return StartCoroutine (Player.GetComponent<CharacterBase> ().Heal (HealSkillValue,base.onValue));
		yield return StartCoroutine (ShowText (null, null));
		HealSkillValue = 0.0f;
	
		StartCoroutine (EnemyTurn());
	}

	protected override IEnumerator ShowText (GameObject Attacker, GameObject Damager)
	{
		/* 受けたダメージを表示 */
		TextObject.GetComponentInChildren<Text>().text = Player.name + "は«" +CallBackValue + "»の回復をした";

		/* ３秒後次の攻撃処理に戻る */
		yield return new WaitForSeconds (3.0f);

		/* ダメージの表示を非表示 */
		TextObject.GetComponentInChildren<Text>().text = "";

		yield return null;
	}
}

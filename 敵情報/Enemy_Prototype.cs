using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Prototype : EnemyBase {
	int TurnCount = 0;
	/* ダメージを敵に与える */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~15の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,16);
		TurnCount++;
		if (TurnCount == 1) {
			text.text = "ショウメツ　モクヒョウ　ハッケン　タダチニ　ハカイ　シマス";
		} else if (TurnCount == 3) {
			text.text = "モード　チェンジ　アタックモード　コテイ　ホウゲキ";
			damage = 20;
		} else if (TurnCount == 5) {
			text.text = "モード　チェンジ　ディフェンスモード　テンカイ";
			DefencePower += 3;
		} else if (TurnCount == 7) {
			text.text = "モード　リペアー";
			EnemyHP += 10;
		} else if (TurnCount == 9) {
			text.text = "バーストモード　ノコリ　１ターン";
		} else if (TurnCount >= 10 && TurnCount <= 15) {
			text.text = "バーストモードハツドウ フルステータス　アップ";
			AttackPower += 10;
			DefencePower += 10;
			EnemyHP += 10;
		} else if (TurnCount >= 16) {
			text.text = "バーストモード　カイジョ　オーバーヒート　フルステータス　ダウン";
			AttackPower -= 10;
			DefencePower -= 10;
			EnemyHP -= 10;
		} else {
			text.text = "ハカイ　シマス";
		}
		yield return new WaitForSeconds (2.0f);

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));

		yield return null;
	}
}

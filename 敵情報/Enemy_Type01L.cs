using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_Type01L : EnemyBase {
	public int DeathCount = 0;

	/* 攻撃処理とステータスアップ */
	public override IEnumerator Attack(GameObject PlayerCharacter)
	{
		/* 元々の攻撃力に1~11の乱数を用いてダメージの幅を作る */
		damage = AttackPower + UnityEngine.Random.Range(1,11);
		DeathCount++;

		text.text = "アルティメットモードキドウチュウ　キドウマデノコリ----";
		yield return new WaitForSeconds (2.0f);

		if (DeathCount == 15) {
			text.text = "アルティメットモード　マッサツ　 オーバーヒートレイ";
			damage = 1999;
		}else if (DeathCount % 2 == 0) {
			AttackPower++;
			text.text = "アタックモード　起動　敵攻撃力Up";
		} else if (DeathCount % 3 == 0) {
			EnemyHP += 10;
			text.text = "ヒールモード　起動　敵HP回復";
		} else if (DeathCount % 5 == 0) {
			DefencePower++;
			text.text = "ディフェンスモード　起動　敵防御力Up";
		}
		yield return new WaitForSeconds (2.0f);

		audioSource.PlayOneShot (AttackSE);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* ダメージを与えるプレイヤーキャラの情報を引数として受け取り,そのプレイヤーに与えるダメージと実際に受けたダメージをコールバックしてもらう */
		yield return StartCoroutine (PlayerCharacter.GetComponent<CharacterBase> ().Damage (damage, battleDisposal.onValue));
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* OnClickでスキルを発動 */
public class SkillAction : MonoBehaviour {
	public BattleDisposal battleDisposal;
	public AudioSource audioSource;
	public AudioClip ClickSE;
	public Text text;


	public void ActionSkill(int SkillNumber){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		audioSource.PlayOneShot (ClickSE);

		switch (SkillNumber) {
		case 0:
			if (Player.GetComponent<CharacterBase> ().CharacterMP < 15) {
				text.text = "MPがたりません";
			} else {
				/* Attackコマンド選択時攻撃力を２倍 */
				battleDisposal.AttackSkillValue = 2.0f;
				Player.GetComponent<CharacterBase> ().CharacterMP -= 15;
				text.text = "「剛力の秘術」：攻撃時　攻撃力の数値を２倍にする　を使用しました";
			}
			break;

		case 1:
			if (Player.GetComponent<CharacterBase> ().CharacterMP < 15) {
				text.text = "MPがたりません";
			} else {
				/* Defenceコマンド選択時防御力アップ値を２倍 */
				battleDisposal.DefenceSkillValue = 2.0f;
				Player.GetComponent<CharacterBase> ().CharacterMP -= 15;
				text.text = "「活力の秘術」：防御時　防御力のアップ値を２倍にする　を使用しました";
			}
			break;

		case 2:
			if (Player.GetComponent<CharacterBase> ().CharacterMP < 15) {
				text.text = "MPがたりません";
			} else {
				/* Healコマンド選択時回復力アップ値を２倍 */
				battleDisposal.HealSkillValue = 2.0f;
				Player.GetComponent<CharacterBase> ().CharacterMP -= 15;
				text.text = "「心力の秘術」：回復時　回復力の数値を２倍にする　を使用しました";
			}
			break;

		case 3:
			if (Player.GetComponent<CharacterBase> ().CharacterMP < 25) {
				text.text = "MPがたりません";
			} else {
				/* Attackコマンド選択時攻撃力を３倍 */
				battleDisposal.AttackSkillValue = 3.0f;
				Player.GetComponent<CharacterBase> ().CharacterMP -= 25;
				text.text = "「捨て身の秘術」：攻撃時　攻撃力の数値を３倍にする　を使用しました";
			}
			break;

		case 4:
			if (Player.GetComponent<CharacterBase> ().CharacterMP < 20) {
				text.text = "MPがたりません";
			} else {
				/* HPを全回復 */
				Player.GetComponent<CharacterBase> ().CharacterHP = 9999;
				Player.GetComponent<CharacterBase> ().CharacterMP -= 20;
				text.text = "「天使の秘術」：HPを全回復しました";
			}
			break;
		}
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* UIの表示や非表示はボタンのOnClickを用いる
OnClickの関数をこのクラスではまとめている */ 
public class UI_Button : MonoBehaviour {
	public GameObject SelectCommand_UI;
	public GameObject EnemySelect_UI;
	public GameObject Attack_UI;
	public GameObject Element_UI;
	public GameObject Skill_UI;
	bool ElementUiTrigger = true;
	bool SkillUiTrigger = true;

	/* コマンドアタックのOnClick処理 */
	public void UI_AttackButton()
	{
		SelectCommand_UI.SetActive (false);
		EnemySelect_UI.SetActive (true);
	}

	/* コマンドディフェンスOnClick処理 */
	public void UI_DefenceButton()
	{
		SelectCommand_UI.SetActive (false);
	}

	/* コマンドヒールのOnClick処理 */
	public void UI_Hell()
	{
		SelectCommand_UI.SetActive (false);

	}

	/* コマンドエレメントのOnClick処理 */
	public void UI_ElementButton()
	{
		if (ElementUiTrigger == true) {
			Element_UI.SetActive (true);
			ElementUiTrigger = false;
		} else {
			Element_UI.SetActive (false);
			ElementUiTrigger = true;
		}
	}

	/* コマンドスキルのOnClick処理 */
	public void UI_SkillButton()
	{
		if (SkillUiTrigger == true) {
			Skill_UI.SetActive (true);
			SkillUiTrigger = false;
		} else {
			Skill_UI.SetActive (false);
			SkillUiTrigger = true;
		}
	}

	/* 敵選択のOnClick処理 */
	public void UI_EnemySelect()
	{
		EnemySelect_UI.SetActive (false);
		SelectCommand_UI.SetActive (true);
	}
}

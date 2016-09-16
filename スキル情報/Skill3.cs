using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill3 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;

	public void OnPointerEnter (PointerEventData eventData){
		SkillText.text = "「活力の秘術」：防御時 防御力のアップ値を２倍にする";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillText.text = "";

	}
}

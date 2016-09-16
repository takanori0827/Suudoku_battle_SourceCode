using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill2 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;

	public void OnPointerEnter (PointerEventData eventData){
		SkillText.text = "「心力の秘術」：回復時 回復力の数値を２倍にする";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillText.text = "";

	}
}

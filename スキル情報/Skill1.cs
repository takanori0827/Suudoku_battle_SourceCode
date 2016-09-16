using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill1 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;

	public void OnPointerEnter (PointerEventData eventData){
		SkillText.text = "「剛力の秘術」：攻撃時 攻撃力の数値を２倍にする";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillText.text = "";

	}
}

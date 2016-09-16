using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill5 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;

	public void OnPointerEnter (PointerEventData eventData){
		SkillText.text = "「天使の秘術」：HPを全回復する";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillText.text = "";

	}
}

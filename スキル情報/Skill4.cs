﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill4 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;

	public void OnPointerEnter (PointerEventData eventData){
		SkillText.text = "「捨て身の秘術」：攻撃時 攻撃力の数値を３倍にする";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillText.text = "";

	}
}

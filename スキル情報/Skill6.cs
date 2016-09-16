using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill6 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Text SkillText;
	public GameObject SkillUI;

	// Use this for initialization
	void Start () {
		SkillUI.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnPointerEnter (PointerEventData eventData){
		SkillUI.SetActive (true);
		SkillText.text = "チャージ中のダメージを無効化する";
	}

	public void OnPointerExit (PointerEventData eventData){
		SkillUI.SetActive (false);

	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* プレイヤーのHP.MPを表示 */
public class TextUI : MonoBehaviour {
	public Text PlayerHpText;	//HpText
	public Text PlayerMpText;	//MpText
	GameObject Player;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		PlayerHpText.text = "HP:" + Player.GetComponent<CharacterBase> ().CharacterHP;
		PlayerMpText.text = "MP:" + Player.GetComponent<CharacterBase> ().CharacterMP;

	}
}

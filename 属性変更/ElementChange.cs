using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* 属性を自由に変更 */
public class ElementChange : MonoBehaviour {
	public Text ElementText;

	/* onClickのString型の引数を貰いswitch文でプレイヤーの属性変数を変更 */
	public void CharacterElementChanage(string elementName)
	{
		foreach (var CharacterElement in FindObjectsOfType<CharacterBase>()) {
			switch (elementName) {
			case "Fire":
				CharacterElement.elementType = CharacterBase.ElementType.Fire;
				break;

			case "Water":
				CharacterElement.elementType = CharacterBase.ElementType.Water;
				break;

			case "Earth":
				CharacterElement.elementType = CharacterBase.ElementType.Earth;
				break;

			case "Thunder":
				CharacterElement.elementType = CharacterBase.ElementType.Thunder;
				break;

			case "Wind":
				CharacterElement.elementType = CharacterBase.ElementType.Wind;
				break;

			case "Ice":
				CharacterElement.elementType = CharacterBase.ElementType.Ice;
				break;
			}
		}
		ElementText.text = "現在の属性は«" + elementName + "»";
	}		

}

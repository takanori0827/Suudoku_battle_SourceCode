using UnityEngine;
using System.Collections;

//キャラクターデータ引数
[System.Serializable]public class Character 
{
	public string CharacterName;		//キャラクターの名前
	public int CharacterID;				//キャラクターID
	public string CharacterDesc;		//キャラクター説明
	public int CharacterHP;				//キャラクターHP
	public int CharacterMP;				//キャラクターMP
	public int CharacterAttack;			//キャラクター攻撃力
	public int CharacterDefence;		//キャラクター防御力
	public bool CharacterFlag;			//キャラクターの撃破情報
	public CharacterType characterType;	//キャラクタータイプ 
	public ElementType elementType; 	//キャラクターの属性

	//キャラクタータイプ
	public enum CharacterType
	{
		Character,		//操作キャラ
		Enemy,			//敵キャラ(雑魚)
		Boss,			//敵キャラ(ボス)
		Debug,
	}

	public enum ElementType
	{
		Unattributed,	//無属性
		Fire,			//火属性
		Water,			//水属性
		Earth,			//土属性
		Thunder,		//雷属性
		Wind,			//風属性
		Ice,			//氷属性
	}


	//リスト化引数
	public Character(string name, int id, string desc, int hp, int mp, int attack, int defence, bool flag, CharacterType type, ElementType element)
	{
		CharacterName = name;
		CharacterID = id;
		CharacterDesc = desc;
		CharacterHP = hp;
		CharacterMP = mp;
		CharacterAttack = attack;
		CharacterDefence = defence;
		CharacterFlag = flag;
		characterType = type;
		elementType = element;
	}

}

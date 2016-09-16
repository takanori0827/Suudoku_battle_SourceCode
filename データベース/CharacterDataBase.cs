using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDataBase : MonoBehaviour {
	public List<Character> characters = new List<Character> ();

	// Use this for initialization
	void Start () {
		/* キャラクターデータの追加 名前,ID,説明,HitPoint,MagicPoint,AttackPower,DefencePower,撃破情報,キャラクタータイプ,キャラクター属性 */
		/* プレイヤーキャラ */
		characters.Add(new Character("Player",0,"操作キャラクターはこの一体のみ",100,100,30,3,false,Character.CharacterType.Character,Character.ElementType.Unattributed));
		/* Chapter1モンスター */
		characters.Add(new Character("サポテンダー",1,"森雑魚",50,5,20,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("ゴブリーン",2,"森雑魚",100,5,25,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("真実の石像",3,"森雑魚",100,100,5,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("虚像の石像",4,"森雑魚",100,100,5,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("判決の石像",5,"森雑魚",120,100,25,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("ネクロマンサー",6,"森ボス",130,100,30,10,false,Character.CharacterType.Boss,Character.ElementType.Fire));
		/* Chapter2モンスター */
		characters.Add(new Character("ドラゴンウォリアー",7,"町雑魚",150,100,25,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("盗賊",8,"町雑魚",140,100,20,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("デーモン",9,"町雑魚",150,100,25,8,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("天命の翼像",10,"町ボス",80,100,0,5,false,Character.CharacterType.Boss,Character.ElementType.Unattributed));
		characters.Add(new Character("取りつかれた亡霊",11,"町ボス",200,100,30,8,false,Character.CharacterType.Boss,Character.ElementType.Unattributed));
		/* Chapter3モンスター */
		characters.Add(new Character("鋼の兵士",12,"城雑魚",170,100,20,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("黒騎士",13,"城雑魚",180,100,25,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("無名",14,"城雑魚",200,100,25,5,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("グランドドラゴン",15,"城ボス",350,100,25,10,false,Character.CharacterType.Boss,Character.ElementType.Unattributed));
		/* Chapter4モンスター */
		characters.Add(new Character("Type0-1L",16,"宇宙雑魚",180,100,20,8,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("Type1-9S",17,"宇宙雑魚",180,100,20,8,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("LowKaspe",18,"宇宙雑魚",190,100,20,10,false,Character.CharacterType.Enemy,Character.ElementType.Unattributed));
		characters.Add(new Character("Prototype-X-",19,"宇宙ボス",210,100,25,8,false,Character.CharacterType.Boss,Character.ElementType.Unattributed));
		/* Chapter5モンスター */
		characters.Add(new Character("クイーン",20,"月ボス",250,100,25,8,false,Character.CharacterType.Boss,Character.ElementType.Unattributed));
		//デバッグ敵　消す
		characters.Add(new Character("DebugEnemy",21,"デバッグキャラ",10,100,80,5,false,Character.CharacterType.Debug,Character.ElementType.Unattributed));

		foreach (var EnemySet in FindObjectsOfType<EnemyBase>()) {
			EnemySet.GetStatusData ();
		}
		foreach (var CharacterSet in FindObjectsOfType<CharacterBase>()) {
			CharacterSet.GetStatusData ();
		}
	}

}

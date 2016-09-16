using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable] public class Stage {
	public string stageName;
	public int stageNumber;
	public string stageDesc;
	public bool stageClearFlag;

	/* リスト化引数 */
	public Stage(string name, int number, string desc, bool Flag){
		stageName = name;
		stageNumber = number;
		stageDesc = desc;
		stageClearFlag = Flag;
	}
}

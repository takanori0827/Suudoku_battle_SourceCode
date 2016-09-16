using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* ステージのリスト */
public class StageDataBase : MonoBehaviour {
	public List<Stage> Stages = new List<Stage> ();

	void Start () 
	{
		Stages.Add (new Stage ("Chapter1-1", 0, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter1-2", 1, "ステージ1-2", false));
		Stages.Add (new Stage ("Chapter1-3", 2, "ステージ1-3", false));
		Stages.Add (new Stage ("Chapter1-4", 3, "ステージ1-4", false));
		Stages.Add (new Stage ("Chapter2-1", 4, "ステージ2-1", false));
		Stages.Add (new Stage ("Chapter2-2", 5, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter2-3", 6, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter2-4", 7, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter3-1", 8, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter3-2", 9, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter3-3", 10, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter3-4", 11, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter4-1", 12, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter4-2", 13, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter4-3", 14, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter4-4", 15, "ステージ1-1", false));
		Stages.Add (new Stage ("Chapter5-1", 16, "ステージ1-1", false));

	}
	
	
}

using UnityEngine;
using System.Collections;

/* OnClickでボタンから取得したIDを使いステージのデータベースを参照*/
public class StageSelectOnClick : MonoBehaviour {

	public void OnClickStageSelect()
	{
		StageID stageID = FindObjectOfType<StageID>();
		StageDataBase stageDataBase = FindObjectOfType<StageDataBase>();

		if (stageID.ID != 0) {
			if (stageDataBase.Stages [stageID.ID - 1].stageClearFlag == true) {
				Application.LoadLevel (stageDataBase.Stages [stageID.ID].stageName);
			}
		} else {
			Application.LoadLevel (stageDataBase.Stages [stageID.ID].stageName);
		}

	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* OnClickからそのステージのIDを取得 */
public class StageSelect : MonoBehaviour {
	public int StageID = 0;

	public void ChapterIdOnClick(int NumberID){
		StageID stageID = FindObjectOfType<StageID> ();
		stageID.ID = NumberID;
	}

}

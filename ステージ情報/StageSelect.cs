using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* OnClick���炻�̃X�e�[�W��ID���擾 */
public class StageSelect : MonoBehaviour {
	public int StageID = 0;

	public void ChapterIdOnClick(int NumberID){
		StageID stageID = FindObjectOfType<StageID> ();
		stageID.ID = NumberID;
	}

}

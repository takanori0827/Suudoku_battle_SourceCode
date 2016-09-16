using UnityEngine;
using System.Collections;

/* シーンが移行しても削除されないようにし、シーン先でのID検索に用いる */
public class StageID : MonoBehaviour {
	public int ID;

	private static bool created = false;

	void Awake()
	{
		if (!created) {

			DontDestroyOnLoad (gameObject);

			created = true;

		} else {
			Destroy (this.gameObject);
		}

	}


}

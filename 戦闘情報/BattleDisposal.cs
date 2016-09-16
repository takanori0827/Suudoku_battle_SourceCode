using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

/* �o�g������ �����o�g���͏��1vs1�̕��Ƃ��č쐬���邪�ō�1vs3�܂ŉ\ */
public class BattleDisposal : MonoBehaviour {
	/* ���I�ɃQ�[���I�u�W�F�N�g���A�^�b�`���� */
	[SerializeField] protected GameObject Command;		//�R�}���h
	[SerializeField] protected GameObject TextObject;	//�\���e�L�X�g
	public StageDataBase stageDataBase;

	/* �V�[����̃X�N���v�g���A�^�b�` */
	protected CharacterBase characterBase;
	protected EnemyBase enemyBase;

	/* ���������_���[�W�l�Ȃǂ��G�̃X�e�[�^�X���ɂ���Ď��ۂɂ����ɂȂ����� */
	protected int CallBackValue = 0;

	/* ���Ƃ̐��� */
	public static int Conbo = 0;

	/* �X�L���ɂ���đ��������{���l */
	public float AttackSkillValue = 0.0f;
	public float HealSkillValue = 0.0f;
	public float DefenceSkillValue = 0.0f;

	/* �v���C���[�ƓG�����L���b�V�� */
	protected GameObject[] EnemyTags = new GameObject[3];
	protected GameObject Player;

	/* �G�I����GUIButton�̃L���b�V�� */
	public GameObject[] EnemySelectButton = new GameObject[3];

	/* �ǂ̓G�ɏ�����^���邩���L���b�V�� */
	protected GameObject TargetEnemy;
	public GameObject NullTargetObject;

	/* ���ƃX�N���v�g */
	public RPGSudoku rpgSudoku;

	/* �G�G�l�~�[�����݂��Ă��邩 */
	bool isEnemy = false;

	public AudioClip Fanfare;


	public void Start()
	{
		/* �V�[����̃v���C���[�E�G�Q�[���I�u�W�F�N�g�̏����擾 */
		EnemyTags = GameObject.FindGameObjectsWithTag ("Enemy");
		Player = GameObject.FindGameObjectWithTag ("Player");

		/* �X�N���v�g���L���b�V�� */
		characterBase = FindObjectOfType<CharacterBase> ();
		enemyBase = FindObjectOfType<EnemyBase> ();

		/* �G�̐��ɂ���ēG�I�����s����{�^���̐���\������ */
		for (int i = 0; i < EnemyTags.Length ; i++) 
		{
			EnemySelectButton [i].SetActive (true);
			EnemySelectButton [i].GetComponentInChildren<Text> ().text = EnemyTags [(EnemyTags.Length - 1) - i].name;
		}

		/* TargetEnemy�I�u�W�F�N�g�ɂ���̃Q�[���I�u�W�F�N�g�����Ă��� */
		TargetEnemy = NullTargetObject;


	}

	/* OnClick�����œG��I����,�퓬�����֓��� */
	public void SelectEnemy(int i)
	{
		TargetEnemy = EnemyTags [(EnemyTags.Length - 1) - i];
		StartCoroutine (PlayerTurn());
	}
		
	/* �R�}���h�����˃v���C���[�̍U���˃����X�^�[�̍U���̏��ŏ��������Ă��� */
	protected virtual IEnumerator PlayerTurn()
	{
		/* �R�}���h���\�� */
		Command.SetActive (false);

		/* �U���񐔂��擾 */
		yield return StartCoroutine (rpgSudoku.SudokuStart());

		/* �v���C���[�̍U������ */
		for (int i = 0; i < Conbo; i++) 
		{
			if (TargetEnemy != null) {
				/* �v���C���[��CharacterBase�N���X���p�����Ă���X�N���v�g�ɃA�N�Z�X */
				yield return StartCoroutine (Player.GetComponent<CharacterBase> ().Attack (AttackSkillValue, TargetEnemy, Player));
				yield return StartCoroutine (ShowText (Player, TargetEnemy));
			}  else {
				TargetEnemy = NullTargetObject;
				break;
			}
		}
		AttackSkillValue = 0.0f;

		/* �G�G�l�~�[�����݂��Ă��邩���Ȃ����`�F�b�N */
		for (int i = 0; i < EnemyTags.Length; i++) {
			if (EnemyTags [i] == null) {
				isEnemy = false;
			}  else {
				isEnemy = true;
				break;
			}
		}

		/* �G�G�l�~�[�����݂��Ă���Ȃ瑊��̍U���������J�n */
		if (isEnemy == true) {
			/* �G�G�l�~�[�̍U������ */
			StartCoroutine (EnemyTurn ());
		}  else {
			/* �G���S�ł��Ă���΃X�e�[�W�N���A */
			StartCoroutine (StageClear ());
		}
	
	}


	/* �G�����X�^�[�̍U������ */
	public IEnumerator EnemyTurn()
	{
		/* ���݂��邾���̓G�̏��� */
		for (int i = 0; i < EnemyTags.Length; i++) {
			if (EnemyTags [i] != null) {
				/* �G��EnemyBase���p�����Ă���X�N���v�g�ɃA�N�Z�X */
				yield return StartCoroutine (EnemyTags [i].GetComponent<EnemyBase> ().Attack (Player));
				/* �G�̍U������͂��̃X�N���v�g����ShowText�֐��ɃA�N�Z�X����悤�ɂ��� */
				yield return StartCoroutine (this.gameObject.GetComponent<BattleDisposal> ().ShowText (EnemyTags [i], Player));
			}
		}

		if (Player.GetComponent<CharacterBase> ().CharacterHP <= 0) {
			yield return StartCoroutine (GameOver ());
		}

		/* �R�}���h��\�� */
		Command.SetActive (true);

		yield return null;
	}

	public IEnumerator StageClear()
	{
		/* �X�e�[�WID���擾���N���A�t���O�����Ă� */
		StageID stageID = FindObjectOfType<StageID> ();
		TextObject.GetComponentInChildren<Text> ().text = stageDataBase.Stages[stageID.ID].stageName + "�N���A!!!";
		foreach (var StageDataBase in FindObjectsOfType<StageDataBase>()) {
			StageDataBase.Stages [stageID.ID].stageClearFlag = true;
		}

		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("MainTitle");
	}

	public IEnumerator GameOver()
	{
		TextObject.GetComponentInChildren<Text> ().text = "�Q�[���I�[�o�[�@�G�̍U�������R���{���d�˂悤";

		yield return new WaitForSeconds (5.0f);
		Application.LoadLevel ("MainTitle");
	}

	/* ���ۂ̎󂯂��_���[�W�l��q�[���l���R�[���o�b�N���Ă��炤 */
	public void onValue(int Value)
	{
		CallBackValue = Value;
	}

	/* �U�����̃_���[�W����ʂɕ\�� */
	protected virtual IEnumerator ShowText(GameObject Attacker,GameObject Damager)
	{
		/* �󂯂��_���[�W��\�� */
		TextObject.SetActive (true);
		if (TargetEnemy == null) {
			TextObject.GetComponentInChildren<Text> ().text = "�G�����X�^�[��|���܂���";
		}  else {
			TextObject.GetComponentInChildren<Text> ().text = Attacker.name + "��" + Damager.name + " ? + CallBackValue + "?amage";
		}

		/* �R�b�㎟�̍U�������ɖ߂� */
		yield return new WaitForSeconds (3.0f);

		/* �_���[�W�̕\�����\�� */
		TextObject.GetComponentInChildren<Text>().text = "";

		yield return null;
	}
		
}


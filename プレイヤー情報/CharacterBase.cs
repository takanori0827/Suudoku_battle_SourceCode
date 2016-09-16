using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

/* �ץ쥤��`����饯���`�λ����O�� */
public class CharacterBase : MonoBehaviour {
	/* ���Ʃ`������� */
	[SerializeField] protected int ID;						//���Ʃ`����ID
	[SerializeField] protected int MaxHp;					//���HP	
	[SerializeField] protected int MaxMp;					//���MP
	[SerializeField] protected int AttackPower;				//������
	[SerializeField] protected int DefencePower;			//������
	[SerializeField] protected float damage;				//����`��

	/* ���� */
	float HealValue = 0.0f;									//�ҩ`�낎
	float DefenceValue=0.0f;								//������

	/* �ǩ`���٩`���򥭥�å��� */
	[SerializeField] protected GameObject characterDataBase;	
	[SerializeField] protected GameObject database;				
				
	/* Audio��particle�򥭥�å��� */
	[SerializeField] protected AudioSource audioSource;	
	[SerializeField] protected AudioClip[] AttackSound = new AudioClip[4];

	/* �ǥꥲ�`�� */
	public delegate void ValueCallBack(int Value);	

	/* BattleDisposal���饹�򥭥�å��� */
	public BattleDisposal battleDisposal;

	public Image DamageImage;

	/* �F�ڤ�HP */
	public int CharacterHP {
		get {
			return _CharacterHP;
		}
		set {
			//
			_CharacterHP = Mathf.Min (MaxHp, value);

			//
			if (CharacterHP <= 0) {

			}

		}

	}

	/* �F�ڤ�MP */
	public int CharacterMP {
		get {
			return _CharacterMP;
		}
		set {
			
			_CharacterMP = Mathf.Min(MaxMp,value);

			if (CharacterMP <= 0) {

			}

		}
	}

	private int _CharacterHP;
	private int _CharacterMP;

	/* ���ԥ����פ��В� */
	public enum ElementType {
		Unattributed,	//�o����
		Fire,			//������
		Water,			//ˮ����
		Earth,			//������
		Thunder,		//������
		Wind,			//�L����
		Ice,			//������
	}
		
	public ElementType elementType;

	/* �������ǩ`����ꥹ�Ȼ������ǩ`���٩`������ID���O��������ȡ�ä��� */
	public virtual void GetStatusData()
	{
		MaxHp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterHP;				//���Υ��������HP
		MaxMp = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterMP;				//���Υ��������MP
		AttackPower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterAttack;	//���Υ��������AP
		DefencePower = characterDataBase.GetComponent<CharacterDataBase>().characters[ID].CharacterDefence;	//���Υ��������DP

		CharacterHP = MaxHp;	//HP���m��
		CharacterMP = MaxMp;	//MP���m��
	}

	/* ���Ĥ�λ��Ф��Τ��������Ȥ����ܤ�ȡ��,�����֔����ͥߩ`�˥���`���I����Ф� */
	public virtual IEnumerator Attack(float SkillValue,GameObject TargetEnemy, GameObject Player)
	{
		/* Ԫ���ι������ȥ�����k�Ӥα��ʤ�\�㤷����������������㤷�ƥ���`���η������� */
		damage = AttackPower * 1.5f * Mathf.Max(1.0f,SkillValue) + UnityEngine.Random.Range(1,11);

		audioSource.PlayOneShot (AttackSound [0]);
		DamageImage.enabled = true; yield return new WaitForSeconds (0.1f); DamageImage.enabled = false;

		/* �ɤΔ��˥���`�����뤨��Τ��������Ȥ����ܤ�ȡ��,���Δ����뤨�����`���Ȍg�H���ܤ�������`���򥳩`��Хå����Ƥ�餦 */
		yield return StartCoroutine (TargetEnemy.GetComponent<EnemyBase> ().Damage (damage, battleDisposal.onValue, Player));

		yield return null;
	}

	/* ����`���I�� */
	public virtual IEnumerator Damage(int damage, ValueCallBack valueCallBack)
	{
		/* 0���Ϥ����� */
		CharacterHP -= Mathf.Max (0, damage) / DefencePower;

		/* �g�H�˄I���줿����`�����򥳩`��Хå� */
		valueCallBack (Mathf.Max (0, damage) / DefencePower);

		yield return null;
	}

	/* �������΂������ */
	public virtual IEnumerator Defence (float SkillValue, ValueCallBack valueCallBack)
	{
		/* ��������1~3���g�ǥ�����˼��� */
		DefenceValue = UnityEngine.Random.Range (1, 4) * Mathf.Max (1.0f, SkillValue);
		DefencePower += Mathf.FloorToInt(DefenceValue);

		/* ׷�ӷ��������򥳩`��Хå� */
		valueCallBack (Mathf.FloorToInt(DefenceValue));

		yield return null;
	}
		
	/* �؏̈́I�� */
	public virtual IEnumerator Heal(float SkillValue,ValueCallBack valueCallBack)
	{
		/* HP��1~20���g�ǥ�����˼��� */
		HealValue = UnityEngine.Random.Range (1, 11) * Mathf.Max(1.0f,SkillValue);
		CharacterHP += Mathf.FloorToInt(HealValue);

		/* �ҩ`�낎�򥳩`��Хå� */
		valueCallBack (Mathf.FloorToInt(HealValue));

		yield return null;
	}




}

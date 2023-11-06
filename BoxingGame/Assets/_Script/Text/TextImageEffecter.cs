using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImageEffecter : MonoBehaviour
{
	private float size = 1f; //���ϴ� ������
	public float speed; //Ŀ�� ���� �ӵ�

	private float time;
	private Vector2 originScale; //���� ũ��

	private void Awake()
	{
		originScale = transform.localScale; //���� ũ�� ����
	}
	private void OnEnable()
	{

	}
	private IEnumerator Up()
	{
		while (transform.localScale.x < size)
		{
			Debug.Log("W");
			transform.localScale = originScale * (1f + time * speed);
			time += Time.deltaTime;

			if (transform.localScale.x >= size)
			{
				Debug.Log("End");
				time = 0;
				break;
			}
			yield return null;
		}
	}
	public void StartSizeUp()
	{
		StartCoroutine(Up());
	}
	private void OnDisable()
	{
		gameObject.transform.localScale = originScale;
	}
}

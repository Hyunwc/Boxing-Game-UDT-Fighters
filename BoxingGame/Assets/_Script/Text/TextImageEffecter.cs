using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImageEffecter : MonoBehaviour
{
	float time;

	// Update is called once per frame
	void Update()
	{

		transform.localScale = Vector3.one * (2 + time);
		time += Time.deltaTime;
		if (time > 0.7f)
		{
			StartCoroutine(WaitDisable());
		}
	}
	IEnumerator WaitDisable()
	{
		yield return new WaitForSeconds(0.3f);
		gameObject.SetActive(false);
	}
	public void resetAnim()
	{
		time = 0;
		transform.localScale = Vector3.one;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandEffect : MonoBehaviour {

	private void OnEnable()
	{
		StartCoroutine(Procedure());
	}

	IEnumerator Procedure()
	{
		yield return new WaitForSeconds(5f);
		gameObject.SetActive(false);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCryEffect : MonoBehaviour {

	private void OnEnable()
	{
		StartCoroutine(Procedure());
	}

	IEnumerator Procedure()
	{
		yield return new WaitForSeconds(1f);
		// still to do
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotionEffect : MonoBehaviour {

    private void OnEnable()
	{
		StartCoroutine(Procedure());
	}

	IEnumerator Procedure()
	{
		yield return new WaitForSeconds(0.2f);
        // still to do
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
	}
}

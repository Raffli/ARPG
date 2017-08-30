using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(Procedure());
    }

    IEnumerator Procedure()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMonsterCamp : MonoBehaviour {

    public LayerMask playerLayerMask;
    public float range;
    Collider[] withinRangeColliders;
    GameObject monsterCamp;
    bool isActive;


    void Start () {
        monsterCamp = transform.GetChild(0).gameObject;
	}

    private void FixedUpdate()
    {
        if (!isActive) {
            withinRangeColliders = Physics.OverlapSphere(transform.position, range, playerLayerMask);
            if (withinRangeColliders.Length != 0) {
                monsterCamp.SetActive(true);
                isActive = true;
            }
        }
    }
}

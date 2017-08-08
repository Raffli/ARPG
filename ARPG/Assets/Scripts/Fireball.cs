using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fireball : MonoBehaviour {

    public int lifeTicks;
    public int speed;
    int lightDamage;
    int aliveFor;
	NavMeshAgent playerAgent;

    void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * movement);

        aliveFor++;
        if (aliveFor == lifeTicks)
        {
            Destroy(gameObject);
        }
    }


	public void SetPlayerAgent (NavMeshAgent playerAgent) {
		this.playerAgent = playerAgent;
	}

    public void SetFireballDamage(int damage) {
        lightDamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
			other.GetComponent<EnemyAI> ().SetAttacked (playerAgent.transform);
            other.GetComponent<EnemyHealth>().ReduceHealth(lightDamage);
            Destroy(gameObject);
        }
    }
}

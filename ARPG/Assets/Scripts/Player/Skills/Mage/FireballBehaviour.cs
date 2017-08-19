using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireballBehaviour : MonoBehaviour {

	private int lifeTicks = 80;
	private int speed = 200;
	private int damage;
	private int aliveFor;
	private GameObject player;
    private bool impact;
    private SphereCollider coll;
    private GameObject effects;
    AudioSource source;
    public AudioClip hitSound;

    private void Start () {
		GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        coll = GetComponent<SphereCollider>();
        source = GetComponent<AudioSource>();
        effects = gameObject.transform.GetChild(0).gameObject;
    }
	
	private void FixedUpdate() {
		aliveFor++;
		if (aliveFor == lifeTicks && !impact)
		{
			Destroy(gameObject);
		}
	}

	public void SetAttackingPlayer (GameObject player) {
		this.player = player;
	}

	public void SetFireballDamage(int damage) {
		this.damage = damage;
	}

    IEnumerator AfterImpact() {
        GetComponent<Rigidbody>().isKinematic = true;
        coll.enabled = false;
        effects.SetActive(false);
        source.Stop();
        source.PlayOneShot(hitSound);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy" && !impact)
		{
            impact = true;
			//other.GetComponent<EnemyAI> ().SetAttacked (player.transform);
			other.GetComponent<EnemyHealth>().ReduceHealth(damage);
            StartCoroutine(AfterImpact());
        }
	}
}

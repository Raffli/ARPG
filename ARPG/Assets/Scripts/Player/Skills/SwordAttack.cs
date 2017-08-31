using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwordAttack : NetworkBehaviour {

    int lightDamage;
    int heavyDamage;
    bool lightAttack;
    bool heavyAttack;
    Collider swordColl;
    NetworkIdentity lastHitted;
    AudioSource source;
    public AudioClip[] hitSounds;

    void Start() {
        swordColl = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
        DisableSword ();
    }

    public void SetLightDamage(int damage) {
        lightDamage = damage;
    }
    public void SetHeavyDamage(int damage) {
        heavyDamage = damage;
    }

    public void SetAttack(bool lAttack,bool hAttack)
    {
        lightAttack = lAttack;
        heavyAttack = hAttack;

        if (lightAttack || heavyAttack)
        {
            swordColl.enabled = true;
        }
    }

    public void DisableSword (){

        swordColl.enabled = false;
        lastHitted = null;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.GetComponent<NetworkIdentity>());

        if (other.transform.tag == "Enemy") {

            if (lastHitted == other.GetComponent<NetworkIdentity>())
            {
                return;
            }   
            else
            {
                lastHitted = null;
                if (lightAttack)
                {
                    source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                    other.GetComponent<EnemyHealth>().ReduceHealth(lightDamage);
                    lastHitted = other.GetComponent<NetworkIdentity>();
                }
                else if (heavyAttack)
                {
                    source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                    other.GetComponent<EnemyHealth>().ReduceHealth(heavyDamage);
                    lastHitted = other.GetComponent<NetworkIdentity>();
                }
            }
        }
    }
}

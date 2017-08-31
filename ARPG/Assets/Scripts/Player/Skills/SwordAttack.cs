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
        Debug.Log("setLightDamage");
        lightDamage = damage;
    }
    public void SetHeavyDamage(int damage) {
        heavyDamage = damage;
    }

    public void SetAttack(bool lAttack,bool hAttack)
    {
        Debug.Log("setAttack" + lAttack + hAttack);
        lightAttack = lAttack;
        heavyAttack = hAttack;

        if (lightAttack || heavyAttack)
        {
            Debug.Log("Swordcoll enabled");
            swordColl.enabled = true;
        }
    }

    public void DisableSword (){
        Debug.Log("disableSword");

        swordColl.enabled = false;
        lastHitted = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ontriggerenter");

        Debug.Log(other.GetComponent<NetworkIdentity>());

        if (other.transform.tag == "Enemy") {
            Debug.Log("othertransform = enemy");

            if (lastHitted == other.GetComponent<NetworkIdentity>())
            {
                Debug.Log("return weil " + lastHitted + " = " + other.GetComponent<NetworkIdentity>());
                return;
            }   
            else
            {
                Debug.Log("alter collider != neuer collider");
                lastHitted = null;
                if (lightAttack)
                {
                    Debug.Log("lightAttack");
                    source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                    other.GetComponent<EnemyHealth>().ReduceHealth(lightDamage);
                    lastHitted = other.GetComponent<NetworkIdentity>();
                }
                else if (heavyAttack)
                {
                    Debug.Log("heavyAttack");
                    source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                    other.GetComponent<EnemyHealth>().ReduceHealth(heavyDamage);
                    lastHitted = other.GetComponent<NetworkIdentity>();
                }
            }
        }
    }
}

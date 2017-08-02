using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour {

    public Animator animator;
    public GameObject spellOrigin;
    public GameObject fireBall;
    public float lightCooldown;
    public float heavyCooldown;

    public bool leftMouseClick = false;
    public bool rightMouseClick = false;

    public bool leftAttack = false;
    public bool rightAttack = false;

    public int lightDamage;
    public int heavyDamage;


    void Start () {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate () {

        if (animator)
        {
            animator.SetBool("RightMouse", rightMouseClick);
            animator.SetBool("LeftMouse", leftMouseClick);
        }

        if (Input.GetAxis("Fire1") > 0 && !leftAttack)
        {
            GameObject obj = Instantiate(fireBall, spellOrigin.transform.position, transform.rotation);
            obj.GetComponent<Fireball>().SetFireballDamage(lightDamage);
            leftMouseClick = true;
            leftAttack = true;
            StartCoroutine(ResetLeftMouse());
        }
        else
        {
            leftMouseClick = false;
        }

        if (Input.GetAxis("Fire2") > 0 && !rightAttack)
        {
            rightMouseClick = true;
            rightAttack = true;
            StartCoroutine(ResetRightMouse());
        }
        else
        {
            rightMouseClick = false;
        }
    }

    IEnumerator ResetLeftMouse()
    {
        yield return new WaitForSeconds(lightCooldown);
        leftAttack = false;
    }

    IEnumerator ResetRightMouse()
    {
        yield return new WaitForSeconds(heavyCooldown);
        rightAttack = false;
    }

}

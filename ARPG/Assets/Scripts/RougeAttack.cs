using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeAttack : MonoBehaviour {

    public Animator animator;
    public bool leftMouseClick = false;
    public bool rightMouseClick = false;

    public bool leftAttack = false;
    public bool rightAttack = false;

    public int lightDamage;
    public int heavyDamage;

    public GameObject leftSword;
    public GameObject rightSword;



    void Start () {
        animator = GetComponent<Animator>();
        leftSword.GetComponent<SwordAttack>().SetLightDamage(lightDamage);
        leftSword.GetComponent<SwordAttack>().SetHeavyDamage(heavyDamage);
        rightSword.GetComponent<SwordAttack>().SetLightDamage(lightDamage);
        rightSword.GetComponent<SwordAttack>().SetHeavyDamage(heavyDamage);
    }

    void FixedUpdate () {

        if (animator)
        {
            animator.SetBool("RightMouse", rightMouseClick);
            animator.SetBool("LeftMouse", leftMouseClick);
        }

        if (Input.GetAxis("Fire1") > 0 && !leftAttack)
        {
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

        leftSword.GetComponent<SwordAttack>().SetAttack(leftAttack, rightAttack);
        rightSword.GetComponent<SwordAttack>().SetAttack(leftAttack, rightAttack);

    }

    IEnumerator ResetLeftMouse()
    {
        yield return new WaitForSeconds(0.5f);
        leftAttack = false;
    }

    IEnumerator ResetRightMouse()
    {
        yield return new WaitForSeconds(1.5f);
        rightAttack = false;
    }
}

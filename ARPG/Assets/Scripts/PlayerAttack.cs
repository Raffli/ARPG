using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Animator animator;
    public bool leftMouseClick = false;
    public bool rightMouseClick = false;

    public bool leftAttack = false;
    public bool rightAttack = false;

    public int lightDamage;
    public int heavyDamage;

    public SwordAttack sword;


    void Start () {
        animator = GetComponent<Animator>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordAttack>();
        sword.setLightDamage(lightDamage);
        sword.setHeavyDamage(heavyDamage);
    }

    void FixedUpdate () {

        if (animator)
        {
            animator.SetBool("RightMouse", rightMouseClick);
            animator.SetBool("LeftMouse", leftMouseClick);
        }

        if (Input.GetAxis("Fire1") > 0)
        {
            leftMouseClick = true;
            leftAttack = true;
            StartCoroutine(ResetLeftMouse());
        }
        else
        {
            leftMouseClick = false;
        }

        if (Input.GetAxis("Fire2") > 0)
        {
            rightMouseClick = true;
            rightAttack = true;
            StartCoroutine(ResetRightMouse());
        }
        else
        {
            rightMouseClick = false;
        }

        sword.setAttack(leftAttack, rightAttack);
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

    IEnumerator InAction()
    {
        yield return null;
    }


    IEnumerator AnimationEnd()
    {
        yield return null;
    }
}

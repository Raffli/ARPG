using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Animator animator;
    public bool leftMouseClick = false;
    public bool rightMouseClick = false;

    void Start () {
        animator = GetComponent<Animator>();
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
        }
        else
        {
            leftMouseClick = false;
        }

        if (Input.GetAxis("Fire2") > 0)
        {
            rightMouseClick = true;
        }
        else
        {
            rightMouseClick = false;
        }
    }

    IEnumerator ResetLeftMouse()
    {
        yield return new WaitForSeconds(0.1f);
        leftMouseClick = false;
        yield return null;

    }

    IEnumerator ResetRightMouse()
    {
        yield return new WaitForSeconds(0.1f);
        rightMouseClick = false;
        yield return null;

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

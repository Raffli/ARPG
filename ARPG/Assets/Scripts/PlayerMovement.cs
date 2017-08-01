using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    public Animator animator;

    void Awake ()
    {
        floorMask = LayerMask.GetMask ("Floor");
        playerRigidbody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator>();

    }


    void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move (h, v);
        Turning ();

        if (animator)
        {
            if (h != 0 || v != 0)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);

            }

            animator.SetFloat("Axis_Horizontal", h);
            animator.SetFloat("Axis_Vertical", v);

        }


    }
		


    void Move (float h, float v)
    {

        Vector3 hMove = Camera.main.transform.right * h * Time.deltaTime * speed;
        Vector3 vMove = Camera.main.transform.forward * v * Time.deltaTime * speed;
        playerRigidbody.MovePosition(transform.position + hMove + vMove);
    }


    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotatation);
        }
    }
}

using UnityEngine;
using System.Collections;


public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothing = 5f;

    Vector3 offset;


    void Start ()
    {

        target = transform.parent.GetComponent<Transform>();

        transform.parent = null;

        offset = transform.position - target.position;

        transform.LookAt(target);
    }


    void FixedUpdate ()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

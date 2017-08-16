using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
    }
}


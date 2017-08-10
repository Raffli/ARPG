using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            camera.transform.rotation * Vector3.up);
    }
}


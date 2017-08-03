using UnityEngine;
using System.Collections;


public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothing = 5f;

    Vector3 offset;


    void Start()
    {

        target = transform.parent.GetComponent<Transform>();

        transform.parent = null;

        offset = transform.position - target.position;

        transform.LookAt(target);
    }


    void Update()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.transform.tag.Equals("Hideable"))
        {
            Component[] renderers;
            renderers = other.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers){
                renderer.GetComponent<Renderer>().enabled = false;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Hideable"))
        {
            Component[] renderers;
            renderers = other.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.GetComponent<Renderer>().enabled = true;
            }

        }
    }
}

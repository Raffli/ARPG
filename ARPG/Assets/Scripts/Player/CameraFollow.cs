using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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

    private void OnLevelWasLoaded(int level)
    {
        if (level > 1) {
            transform.position = target.position + offset;
        }
    }

    void Update()
    {

        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Hideable"))
        {
            if (other.gameObject.layer == 8) {
                other.gameObject.layer = 0;
            } 
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

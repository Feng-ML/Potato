using UnityEngine;

// ¸úËæÉãÏñÍ·
public class Billboard : MonoBehaviour
{
    private Transform cam;   // ÉãÏñ»ú

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¸úËæÉãÏñÍ·
public class Billboard : MonoBehaviour
{
    public Transform cam;   // ÉãÏñ»ú

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

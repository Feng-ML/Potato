using UnityEngine;

// ��������ͷ
public class Billboard : MonoBehaviour
{
    private Transform cam;   // �����

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

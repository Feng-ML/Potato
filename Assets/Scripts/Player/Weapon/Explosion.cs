using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionAudio;

    private void OnEnable()
    {
        if (explosionAudio)
        {
            AudioSource.PlayClipAtPoint(explosionAudio, transform.position, 1f);
        }
    }
}

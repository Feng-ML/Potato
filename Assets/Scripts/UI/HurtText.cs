using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtText : MonoBehaviour
{
    public float bounceHeight = 50f;
    public float bounceTime = 1f;

    private RectTransform rectTransform;
    private Action releaseAction;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(HideText());
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(0.5f);
        releaseAction.Invoke();
    }

    public void SetDeactiveAction(Action releaseObj)
    {
        releaseAction = releaseObj;
    }
}

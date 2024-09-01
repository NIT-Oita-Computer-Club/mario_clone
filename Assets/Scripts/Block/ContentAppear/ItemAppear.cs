using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ItemAppear : MonoBehaviour
{
    [SerializeField] AudioClip appearSE;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D[] colliders;
    [SerializeField] float emittionTime = 1f;

    public event UnityAction OnAppeared = delegate { };

    private void Start()
    {
        StartCoroutine(AppearCoroutine());
    }

    IEnumerator AppearCoroutine()
    {
        rb.simulated = false;
        foreach(var col in colliders) col.enabled = false;
        Locator<SEManager>.I.Play(appearSE);
        Vector3 targetPosition = transform.position + Vector3.up;
        yield return CoroutineUtil.MoveToInTime(transform, targetPosition, emittionTime);
        foreach (var col in colliders) col.enabled = true;
        rb.simulated = true;
        OnAppeared.Invoke();
    }
}

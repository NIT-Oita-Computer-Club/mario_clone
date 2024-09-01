using System.Collections;
using UnityEngine;

public class BlockAnimation: MonoBehaviour
{
    const float AnimTime = 0.3f;
    const float Max = 0.3f;
    bool animating = false;

    public void Play()
    {
        if (animating) return;
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        animating = true;
        float time = 0f;
        var startPos = transform.position;
        while (time < AnimTime)
        {
            time += Time.deltaTime;
            transform.position = startPos + new Vector3 (0f, Mathf.Sin(Mathf.Clamp01(time / AnimTime) * Mathf.PI) * Max, 0f);
            yield return null;
        }
        animating = false;
    }
}
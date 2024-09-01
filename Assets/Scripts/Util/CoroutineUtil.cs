using System.Collections;
using UnityEngine;

static class CoroutineUtil
{
    public static IEnumerator MoveToInTime(Transform subject, Vector3 target, float time)
    {
        Vector3 startingPosition = subject.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / time);
            subject.position = Vector3.Lerp(startingPosition, target, t);
            yield return null;
        }
    }

    public static IEnumerator MoveToBySpeed(Transform subject, Vector3 target, float speed)
    {
        while (Vector3.Distance(subject.transform.position, target) > 0)
        {
            subject.position = Vector3.MoveTowards(subject.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
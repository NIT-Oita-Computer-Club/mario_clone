using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AfterDiePlayerMovement : PlayerMovement
{
    Rigidbody2D rb;
    float jumpHeight = 5f;
    float time = 1.5f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        var startPos = transform.position;
        yield return new WaitForSeconds(0.6f);
        float timeCount = 0f;
        while (true)
        {
            timeCount += Time.deltaTime;
            transform.position = new Vector2(startPos.x,
               startPos.y - jumpHeight * Mathf.Pow(2f/time * timeCount - 1,2) + jumpHeight);
            yield return null;
        }
    }
}
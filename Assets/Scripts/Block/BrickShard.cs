using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickShard : MonoBehaviour
{
    Vector2 initialVelocity;

    float gravity = -20f;
    float elapsed = 0f;
    Vector3 startPosition;

    public void Init(Vector2 initialVelocity)
    {
        this.initialVelocity = initialVelocity;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float x = initialVelocity.x * elapsed;
        float y = initialVelocity.y * elapsed + 0.5f * gravity * elapsed * elapsed;

        transform.position = startPosition + new Vector3(x, y, 0f);

        bool flip = Mathf.FloorToInt(elapsed / 0.2f) % 2 == 1;
        transform.localScale = new Vector3(flip ? -1 : 1, 1, 1);
    }
}

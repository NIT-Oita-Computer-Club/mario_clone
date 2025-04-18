using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ReboundMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (direction == -1 && collision.EvaluateCollision(Vector2.right, 0.8f) || direction == 1 && collision.EvaluateCollision(Vector2.left, 0.8f))
            direction *= -1;
    }


    private void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
}

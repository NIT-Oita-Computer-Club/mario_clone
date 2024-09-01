using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ReboundMovement : MonoBehaviour
{
    [SerializeField] WallCollisionTrigger wallCollision;
    Rigidbody2D rb;
    [SerializeField] float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed * wallCollision.Direction, rb.velocity.y);
    }
}

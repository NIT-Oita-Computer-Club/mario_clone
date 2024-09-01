using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallCollisionTrigger : MonoBehaviour
{
    [SerializeField] float xOffset;
    public int Direction { get; private set; } = 1;

    void Start()
    {
        transform.localPosition = new Vector2(xOffset, transform.localPosition.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localPosition = new Vector2(-transform.localPosition.x, transform.localPosition.y);
        Direction = transform.localPosition.x > 0 ? 1 : -1;
    }
}

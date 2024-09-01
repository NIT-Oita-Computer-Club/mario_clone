using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    public bool OnGround { get; private set; }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.EvaluateCollision(Vector2.up, 0.1f)) OnGround = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.EvaluateCollision(Vector2.up, 0.1f)) OnGround = true;
    }
}

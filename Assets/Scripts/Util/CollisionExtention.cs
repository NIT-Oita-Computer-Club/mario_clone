using UnityEngine;

public static class CollisionExtention
{
    public static bool EvaluateCollision(this Collision2D collision, Vector2 direction, float tolerance)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;
            if (tolerance <= Vector2.Dot(direction.normalized, normal)) return true;
        }
        return false;
    }

    public static bool EvaluateFirstCollision(this Collision2D collision, Vector2 direction, float tolerance)
    {
        var normal = collision.GetContact(0).normal;
        if (tolerance <= Vector2.Dot(direction.normalized, normal)) return true;
        return false;
    }
}
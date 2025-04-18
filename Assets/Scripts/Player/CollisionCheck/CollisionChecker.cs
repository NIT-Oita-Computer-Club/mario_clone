using UnityEngine;
using UnityEngine.Events;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public event UnityAction OnHeadCollision = delegate { };
    public event UnityAction OnEnterGround = delegate { };
    public bool OnGround { get; private set; }
    public bool OnWall { get; private set; }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.EvaluateCollision(Vector2.up, 0.8f)) OnGround = false;

        if (OnWall &&
        (!collision.EvaluateCollision(Vector2.right, 0.8f) ||
            !collision.EvaluateCollision(Vector2.left, 0.8f))) OnWall = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.EvaluateCollision(Vector2.up, 0.8f)) OnGround = true;

        if (collision.EvaluateCollision(Vector2.right, 0.8f) ||
       collision.EvaluateCollision(Vector2.left, 0.8f)) OnWall = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.EvaluateCollision(Vector2.up, 0.8f))
        {
            OnGround = true;
            OnEnterGround.Invoke();
        }

        if (collision.EvaluateCollision(Vector2.right, 0.8f) ||
        collision.EvaluateCollision(Vector2.left, 0.8f)) OnWall = true;

        if (collision.EvaluateCollision(Vector2.down, 0.8f) && rb.velocity.y == 0)
        {
            OnHeadCollision.Invoke();
        }
    }
}
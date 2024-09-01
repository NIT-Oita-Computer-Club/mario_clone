using UnityEngine;
using UnityEngine.Events;

public class HeadChecker: MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public event UnityAction OnHeadCollision = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.EvaluateCollision(Vector2.down, 0.1f) && rb.velocity.y == 0)
        {
            OnHeadCollision.Invoke();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    Rigidbody2D rb;
    CollisionChecker collisionCheck;
    Animator animator;

    public event UnityAction OnSpark = delegate { };
    [SerializeField] float speed;
    bool isSparking = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IAttackable>(out var attackable))
        {
            attackable.Attack(
                transform.position.x < collision.transform.position.x ? 1 : -1
            );
            Spark();
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponent<CollisionChecker>();

        collisionCheck.OnEnterGround += OnEnterGround;
    }


    void OnDestroy()
    {
        collisionCheck.OnEnterGround -= OnEnterGround;
    }

    void OnEnterGround()
    {
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    public void SetDirection(bool isRight)
    {
        speed *= isRight ? 1 : -1;
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (collisionCheck.OnWall) Spark();

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1f && info.IsName("Spark"))
        {
            Destroy(gameObject);
        }
    }

    void Spark()
    {
        if (isSparking) return;
        isSparking = true;

        OnSpark.Invoke();
        animator.Play("Spark");
    }
}

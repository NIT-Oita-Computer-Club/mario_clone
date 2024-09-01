using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PostGoalPlayerMovement: PlayerMovement
{
    public enum MovementPhase
    {
        Descending,
        WaitingForWalk,
        Walking,
        End
    }

    Rigidbody2D rb;
    const float descendingSpeed = 7f;
    [SerializeField] GoalPole goalPole;

    public event UnityAction OnLanded = delegate { };

    public MovementPhase Phase { get; private set; } = MovementPhase.Descending;

    private void OnEnable()
    {
        StartCoroutine(GoalMoveCoroutine());
    }

    IEnumerator GoalMoveCoroutine()
    {
        transform.position = new Vector2(goalPole.PoleBottomPos.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        yield return CoroutineUtil.MoveToBySpeed(transform, goalPole.PoleBottomPos, descendingSpeed);
        Phase = MovementPhase.WaitingForWalk;
        transform.position = transform.position + Vector3.right; // ゴールポールの右側へ
        yield return new WaitForSeconds(0.6f);
        OnLanded.Invoke();
        Phase = MovementPhase.Walking;
        rb.simulated = true;
        yield return new WaitWhile(() => {
            rb.velocity = new Vector2(4f, rb.velocity.y);
            return Vector2.Distance(transform.position, goalPole.CastleDoorPos) >= 0.2f;
            });
        Phase = MovementPhase.End;
        rb.simulated = false;
        goalPole.EndGame();
    }
}
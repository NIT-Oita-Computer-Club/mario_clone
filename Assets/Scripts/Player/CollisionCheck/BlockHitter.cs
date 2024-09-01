using UnityEngine;
using UnityEngine.Events;

class BlockHitter: MonoBehaviour
{
    [SerializeField] PlayerStateManager player;
    public event UnityAction OnHitBlock = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(Constants.Tags.Block) && collision.EvaluateCollision(Vector2.down,0.1f))
        {
            collision.gameObject.GetComponent<IHittable>().Hit(player.CurrentPowerUpState.Growth != PlayerGrowth.Small);
            OnHitBlock.Invoke();
        }
    }
}
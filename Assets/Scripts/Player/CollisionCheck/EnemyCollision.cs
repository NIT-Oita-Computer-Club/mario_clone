using UnityEngine;
using UnityEngine.Events;

class EnemyCollision : MonoBehaviour
{
    [SerializeField] PlayerStateManager player;
    public event UnityAction OnStamp = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(Constants.Tags.Stampable))
        {
            if(!collision.EvaluateCollision(Vector2.down, 0))
            {
                OnStamp.Invoke();
                collision.gameObject.GetComponent<IStampable>().Stamp();
            }
            else
            {
                player.InjurePlayer();
            }
        }
    }
}
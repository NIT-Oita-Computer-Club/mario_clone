using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// レンガブロック．パワーアップ状態で叩くとすぐに破壊される．
/// </summary>
public class BrickBlock : MonoBehaviour, IHittable
{
    [SerializeField] AudioClip breakSE;
    [SerializeField] BlockAnimation blockAnimation;

    [SerializeField] GameObject brickShardPfb;

    Vector2[] offsets = new Vector2[]
    {
        new Vector2(-0.25f,  0.25f),
        new Vector2( 0.25f,  0.25f),
        new Vector2(-0.25f, -0.25f),
        new Vector2( 0.25f, -0.25f)
    };


    public void Hit(bool isLarge)
    {
        if (isLarge)
        {
            Locator<SEManager>.I.Play(breakSE);

            for (int i = 0; i < 4; i++)
            {
                var shard = Instantiate(brickShardPfb, transform.position + (Vector3)offsets[i], Quaternion.identity);
                shard.GetComponent<BrickShard>().Init(new Vector2(offsets[i].x * 10, offsets[i].y * 20));
            }

            Destroy(gameObject);
            return;
        }

        blockAnimation.Play();
    }
}
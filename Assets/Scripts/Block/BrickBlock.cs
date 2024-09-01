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

    public void Hit(bool isLarge)
    {
        if (isLarge)
        {
            Locator<SEManager>.I.Play(breakSE);
            Destroy(gameObject);
            return;
        }

        blockAnimation.Play();
    }
}
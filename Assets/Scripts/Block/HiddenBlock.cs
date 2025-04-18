using UnityEngine;

class HiddenBlock : MonoBehaviour, IHittable
{
    [SerializeField] BlockEmittion emittion;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] BlockAnimation blockAnimation;
    [SerializeField] BlockSpriteChange spriteChange;
    [SerializeField] Animator animator;

    bool isHitted = false;

    public void Hit(bool isLarge)
    {
        if (isHitted) return;
        isHitted = true;
        collider.enabled = true;
        emittion.Emit(isLarge);
        blockAnimation.Play();
        animator.enabled = false;
        spriteChange.ChangeSprite();
    }
}
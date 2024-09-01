using UnityEngine;

class QuestionBlock: MonoBehaviour, IHittable
{
    [SerializeField] BlockEmittion emittion;
    [SerializeField] BlockAnimation blockAnimation;
    [SerializeField] BlockSpriteChange spriteChange;
    [SerializeField] Animator animator;

    bool isHitted = false;

    public void Hit(bool isLarge)
    {
        if (isHitted) return;
        isHitted = true;
        emittion.Emit(isLarge);
        blockAnimation.Play();
        animator.enabled = false;
        spriteChange.ChangeSprite();
    }
}
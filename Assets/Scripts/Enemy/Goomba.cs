using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour, IStampable, IAttackable
{
    // ���Ɋg�������ė��p���������Ȃ��̂ŁC�S���ꂩ���ɂ܂Ƃ߂�

    Animator animator;
    ReboundMovement movement;
    DieMotion dieMotion;
    Rigidbody2D rb;
    bool isAlive = true;
    const string AnimDie = "Die";

    private void Start()
    {
        movement = GetComponent<ReboundMovement>();
        rb = GetComponent<Rigidbody2D>();
        dieMotion = GetComponent<DieMotion>();
        animator = GetComponent<Animator>();
    }

    public void Stamp()
    {
        StartCoroutine(OnStampedCoroutine());
    }

    public void Attack(int direction)
    {
        StartCoroutine(OnAttackedCoroutine(direction));
    }

    IEnumerator OnStampedCoroutine()
    {
        isAlive = false;
        movement.enabled = false;
        rb.simulated = false;
        animator.Play(AnimDie);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    IEnumerator OnAttackedCoroutine(int direction)
    {
        isAlive = false;
        movement.enabled = false;
        rb.simulated = false;
        animator.enabled = false;
        dieMotion.Init(direction);
        dieMotion.enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

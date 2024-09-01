using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour, IStampable
{
    // ���Ɋg�������ė��p���������Ȃ��̂ŁC�S���ꂩ���ɂ܂Ƃ߂�

    Animator animator;
    ReboundMovement movement;
    Rigidbody2D rb;
    bool isAlive = true;
    const string AnimDie = "Die";

    private void Start()
    {
        movement = GetComponent<ReboundMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Stamp()
    {
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        isAlive = false;
        movement.enabled = false;
        rb.simulated = false;
        animator.Play(AnimDie);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

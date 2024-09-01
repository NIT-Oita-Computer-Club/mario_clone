using System.Collections;
using UnityEngine;

public class CoinAppear : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Locator<GameStateManager>.I.GetCoin();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}

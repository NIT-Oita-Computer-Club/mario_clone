using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    [SerializeField] PlayerStateManager stateManager;
    [SerializeField] DefaultPlayerMovement movement;
    [SerializeField] HeadChecker headChecker;
    [SerializeField] GroundChecker groundChecker;
    [SerializeField] EnemyCollision enemyCollision;
    [SerializeField] ItemPicker itemPicker;

    [SerializeField] AudioClip JumpSmall;
    [SerializeField] AudioClip JumpLarge;

    [SerializeField] AudioClip GrowUp;
    [SerializeField] AudioClip OneUp;

    [SerializeField] AudioClip HeadCollision;
    [SerializeField] AudioClip hitSE;

    [SerializeField] AudioClip InjuredSE;

    [SerializeField] AudioClip Stamp;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = Locator<SEManager>.I.AudioSource;

        movement.OnJump += MakeJumpSE;
        headChecker.OnHeadCollision += MakeCollisionSE;
        itemPicker.OnTakeItem += MakePickItemSE;
        enemyCollision.OnStamp += MakeStampSE;
        stateManager.OnInjured += OnInjured;
    }

    private void OnDestroy()
    {
        movement.OnJump -= MakeJumpSE;
        headChecker.OnHeadCollision -= MakeCollisionSE;
        itemPicker.OnTakeItem -= MakePickItemSE;
        enemyCollision.OnStamp -= MakeStampSE;
        stateManager.OnInjured -= OnInjured;
    }

    void OnInjured()
    {
        audioSource.PlayOneShot(InjuredSE);
    }

    void MakeCollisionSE()
    {
        audioSource.PlayOneShot(HeadCollision);
    }

    void MakeStampSE()
    {
        audioSource.PlayOneShot(Stamp);
    }

    void MakeJumpSE()
    {
        switch (stateManager.CurrentPowerUpState.Growth)
        {
            case PlayerGrowth.Small:
                audioSource.PlayOneShot(JumpSmall);
                break;
            case PlayerGrowth.Large:
            case PlayerGrowth.Fire:
                audioSource.PlayOneShot(JumpLarge);
                break;
        }
    }

    void MakePickItemSE(Item item)
    {
        switch (item)
        {
            case Item.Mushroom:
            case Item.FireFlower:
                audioSource.PlayOneShot(GrowUp);
                break;
            case Item.OneUpMushroom:
                audioSource.PlayOneShot(OneUp);
                break;
            case Item.Star:
                break;
        }
    }
}

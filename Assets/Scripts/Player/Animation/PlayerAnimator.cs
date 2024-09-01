using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    readonly struct AnimationNames
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string Slide = "Slide";
        public const string Jump = "Jump";
        public const string Die = "Die";
        public const string Pole = "Pole";
    }

    readonly struct AnimationNameSuffixes
    {
        public const string Small = "Small";
        public const string Large = "Large";
        public const string Fire = "Fire";
    }

    readonly struct ParamNames
    {
        public const string WalkAnimSpeed = "WalkAnimSpeed";
    }

    // コンポーネント
    [SerializeField] PlayerStateManager stateManager;
    [SerializeField] PlayerMovementManager movementManager;
    [SerializeField] GroundChecker ground;
    Animator animator;

    string currentGrowth;

    private void Start()
    {
        animator = GetComponent<Animator>();

        OnPowerUpStateChanged(stateManager.CurrentPowerUpState);
        stateManager.OnStateChanged += OnPowerUpStateChanged;
        movementManager.OnMovementChanged += OnMovementChanged;
    }

    private void OnDestroy()
    {
        stateManager.OnStateChanged -= OnPowerUpStateChanged;
        movementManager.OnMovementChanged -= OnMovementChanged;
    }

    void OnPowerUpStateChanged(PlayerPowerUpState state)
    {
        switch (state.Growth)
        {
            case PlayerGrowth.Small:
                currentGrowth = AnimationNameSuffixes.Small;
                break;
            case PlayerGrowth.Large:
                currentGrowth = AnimationNameSuffixes.Large;
                break;
            case PlayerGrowth.Fire:
                currentGrowth = AnimationNameSuffixes.Fire;
                break;
        }
    }

    void OnMovementChanged(PlayerMovement movement)
    {
        StopAllCoroutines();
        if (movementManager.CurrentMovement is DefaultPlayerMovement defaultMove) DefaultAnimation(defaultMove);
        else if (movementManager.CurrentMovement is PostGoalPlayerMovement goalMove)
        {
            StartCoroutine(PostGoalAnimationCoroutine(goalMove));
        }
        else if (movementManager.CurrentMovement is AfterDiePlayerMovement)
        {
            animator.Play(AnimationNames.Die);
        }
    }

    void Update()
    {
        if (movementManager.CurrentMovement is DefaultPlayerMovement defaultMove) DefaultAnimation(defaultMove);
    }

    void DefaultAnimation(DefaultPlayerMovement movement)
    {
        if (movement.DisiredXSpeed != 0 && ground.OnGround)
            transform.eulerAngles = new Vector3(0, movement.DisiredXSpeed > 0 ? 0 : 180, 0);
        if (movement.IsJumping)
        {
            animator.Play(AnimationNames.Jump + currentGrowth);
            return;
        }
        if (movement.IsSliding)
        {
            animator.Play(AnimationNames.Slide + currentGrowth);
            return;
        }
        if (movement.IsRunning)
        {
            animator.SetFloat(ParamNames.WalkAnimSpeed, Mathf.Abs(movement.XSpeedRatio));
            animator.Play(AnimationNames.Run + currentGrowth);
            return;
        }
        animator.Play(AnimationNames.Idle + currentGrowth);
    }

    IEnumerator PostGoalAnimationCoroutine(PostGoalPlayerMovement movement)
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        animator.Play(AnimationNames.Pole + currentGrowth);
        yield return new WaitUntil(() => movement.Phase == PostGoalPlayerMovement.MovementPhase.WaitingForWalk);
        transform.eulerAngles = new Vector3(0, 180, 0);
        animator.enabled = false;
        yield return new WaitUntil(() => movement.Phase == PostGoalPlayerMovement.MovementPhase.Walking);
        animator.enabled = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
        animator.SetFloat(ParamNames.WalkAnimSpeed, 0.3f);
        animator.Play(AnimationNames.Run + currentGrowth);
        yield return new WaitUntil(() => movement.Phase == PostGoalPlayerMovement.MovementPhase.End);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefaultPlayerMovement : PlayerMovement
{
    // �R���|�[�l���g
    Rigidbody2D rb;
    [SerializeField] InputReader input;
    [SerializeField] CollisionChecker collisionCheck;
    [SerializeField] EnemyCollision enemyCollision;
    [SerializeField] CollisionChecker headChecker;

    [SerializeField] PlayerStateManager stateManager;

    // �ړ��p�p�����[�^
    PlayerMovementParams currentMoveParams;
    [SerializeField] PlayerMovementParams normalMoveParams;
    [SerializeField] PlayerMovementParams starMoveParams;

    public event UnityAction OnJump = delegate { };

    // �C���v�b�g
    float horizontalInput;
    bool jumpInput = false;
    bool duckInput = false;

    // ���
    public bool IsJumping { get; private set; } = false;
    public float DisiredXSpeed { get; private set; }

    // ����J�v���p�e�B
    bool inputtingMovingDir => Mathf.Sign(horizontalInput) == Mathf.Sign(rb.velocity.x);

    // �A�j���[�V�����p�̌��J�v���p�e�B
    public bool IsRunning => Mathf.Abs(rb.velocity.x) > 0.25f || (!IsDucking && horizontalInput != 0);
    public bool IsSliding => Mathf.Abs(rb.velocity.x) > currentMoveParams.MaxXSpeed && !inputtingMovingDir;
    public bool IsDucking => duckInput;

    public float XSpeedRatio => rb.velocity.x / (currentMoveParams.MaxXSpeed * PlayerMovementParams.DashMultiplier);

    const float VELOCITY_EPSILON = 0.001f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��Ԃɉ����Ĉړ��p�����[�^��ύX
        SetMoveParamsByState(stateManager.CurrentPowerUpState);
        stateManager.OnStateChanged += SetMoveParamsByState;
        headChecker.OnHeadCollision += OnHeadCollision;
        enemyCollision.OnStamp += OnStamp;
    }

    private void OnDisable()
    {
        stateManager.OnStateChanged -= SetMoveParamsByState;
        headChecker.OnHeadCollision -= OnHeadCollision;
        enemyCollision.OnStamp -= OnStamp;
    }

    void OnHeadCollision()
    {
        rb.velocity = new Vector2(rb.velocity.x, -4);
        rb.gravityScale = currentMoveParams.DefaultGravityScale;
    }

    void OnStamp()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentMoveParams.JumpForce);
        IsJumping = true;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentMoveParams.JumpForce);
        IsJumping = true;
        OnJump.Invoke();
    }

    // �ړ��p�����[�^�̐ݒ�
    private void SetMoveParamsByState(PlayerPowerUpState current)
    {
        if (current.IsStar) currentMoveParams = starMoveParams;
        else currentMoveParams = normalMoveParams;
    }

    void Update()
    {
        if (input.RetrieveJumpInput(thisFrame: true) && collisionCheck.OnGround) Jump();

        if (IsJumping && rb.velocity.y <= VELOCITY_EPSILON && collisionCheck.OnGround)
        {
            IsJumping = false;
        }

        duckInput = input.RetrieveDuckInput();
        jumpInput = input.RetrieveJumpInput();
        horizontalInput = input.RetrieveHorizontalInput();

        if (duckInput)
        {
            DisiredXSpeed = 0;
            return;
        }

        DisiredXSpeed = horizontalInput * currentMoveParams.MaxXSpeed
            * (input.RetrieveDashInput() ? PlayerMovementParams.DashMultiplier : 1f);
    }

    private void FixedUpdate()
    {
        var xSpeed = rb.velocity.x;
        var speedDelta = currentMoveParams.Acceleration * Time.deltaTime
                            * (inputtingMovingDir ? 1f : 1.7f);
        var changedSpeed = Mathf.MoveTowards(xSpeed, DisiredXSpeed, speedDelta);
        rb.velocity = new Vector2(changedSpeed, rb.velocity.y);

        rb.gravityScale = GetGravityScale(rb.velocity.y, jumpInput);
    }

    float GetGravityScale(float ySpeed, bool jumpInputting)
    {
        if (ySpeed < VELOCITY_EPSILON || ySpeed > VELOCITY_EPSILON && !jumpInputting) return currentMoveParams.FallingGravityScale;
        else if (ySpeed > 0 && jumpInputting) return currentMoveParams.RisingGravityScale;
        else return currentMoveParams.DefaultGravityScale;
    }
}

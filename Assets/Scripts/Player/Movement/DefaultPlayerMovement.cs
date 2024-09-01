using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefaultPlayerMovement : PlayerMovement
{
    // コンポーネント
    Rigidbody2D rb;
    [SerializeField] InputReader input;
    [SerializeField] GroundChecker ground;
    [SerializeField] EnemyCollision enemyCollision;
    [SerializeField] HeadChecker headChecker;

    [SerializeField] PlayerStateManager stateManager;

    // 移動用パラメータ
    PlayerMovementParams currentMoveParams;
    [SerializeField] PlayerMovementParams normalMoveParams;
    [SerializeField] PlayerMovementParams starMoveParams;

    public event UnityAction OnJump = delegate { };

    // インプット
    float horizontalInput;
    bool jumpInput = false;
    bool duckInput = false;

    // 状態
    public bool IsJumping { get; private set; } = false;
    public float DisiredXSpeed { get; private set; }

    // 非公開プロパティ
    bool inputtingMovingDir => Mathf.Sign(horizontalInput) == Mathf.Sign(rb.velocity.x);

    // アニメーション用の公開プロパティ
    public bool IsRunning => Mathf.Abs(rb.velocity.x) > 0.25f || (!IsDucking && horizontalInput != 0);
    public bool IsSliding => Mathf.Abs(rb.velocity.x) > currentMoveParams.MaxXSpeed && !inputtingMovingDir;
    public bool IsDucking => duckInput;

    public float XSpeedRatio => rb.velocity.x / (currentMoveParams.MaxXSpeed * PlayerMovementParams.DashMultiplier);


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        // 状態に応じて移動パラメータを変更
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

    // 移動パラメータの設定
    private void SetMoveParamsByState(PlayerPowerUpState current)
    {
        if (current.IsStar) currentMoveParams = starMoveParams;
        else currentMoveParams = normalMoveParams;
    }

    void Update()
    {
        if (input.RetrieveJumpInput(thisFrame: true) && ground.OnGround) Jump();
        if (IsJumping && rb.velocity.y <= 0 && ground.OnGround)
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
        if (ySpeed < 0 || ySpeed > 0 && !jumpInputting) return currentMoveParams.FallingGravityScale;
        else if (ySpeed > 0 && jumpInputting) return currentMoveParams.RisingGravityScale;
        else return currentMoveParams.DefaultGravityScale;
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "GamePlaying/PlayerMovementParams")]
public class PlayerMovementParams : ScriptableObject
{
    public const float DashMultiplier = 1.8f;

    // ����
    [SerializeField] float maxSpeed = 6f;
    public float MaxXSpeed => maxSpeed;
    [SerializeField] float acceleration = 10f;
    public float Acceleration => acceleration;

    // ����
    [SerializeField] float defaultGravityScale = 1f;
    public float DefaultGravityScale => defaultGravityScale;
    [SerializeField] float risingGravityScale = 1.7f;
    public float RisingGravityScale => risingGravityScale;
    [SerializeField] float fallingGravityScale = 3f;
    public float FallingGravityScale => fallingGravityScale;
    [SerializeField] float jumpForce = 9f;
    public float JumpForce => jumpForce;
}
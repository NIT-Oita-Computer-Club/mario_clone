using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーの移動用コンポーネントを管理する．
/// PlayerMovementのサブクラスの1つのサブクラスを有効化し，他を無効にする．
/// </summary>
public class PlayerMovementManager: MonoBehaviour
{
    public event UnityAction<PlayerMovement> OnMovementChanged = delegate { };

    public PlayerMovement CurrentMovement { get; private set; }
    [SerializeField] DefaultPlayerMovement defaultMovement;
    public DefaultPlayerMovement DefaultMovement => defaultMovement;
    [SerializeField] PostGoalPlayerMovement postGoalMovement;
    public PostGoalPlayerMovement PostGoalMovement => postGoalMovement;
    [SerializeField] AfterDiePlayerMovement afterDieMovement;
    public AfterDiePlayerMovement GameOverMovement => afterDieMovement;
    PlayerMovement[] movements;

    private void Awake()
    {
        movements = new PlayerMovement[] { defaultMovement, postGoalMovement, afterDieMovement };
    }

    private void Start()
    {
        OnGameStateChange(Locator<GameStateManager>.I.CurrentGameState);
        Locator<GameStateManager>.I.OnStateChange += OnGameStateChange;
    }

    private void OnDestroy()
    {
        if (Locator<GameStateManager>.IsValid()) Locator<GameStateManager>.I.OnStateChange -= OnGameStateChange;
    }

    void OnGameStateChange(GameState current)
    {
        switch (current)
        {
            case GameState.Playing:
                SetMovement<DefaultPlayerMovement>();
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                SetMovement<AfterDiePlayerMovement>();
                break;
            case GameState.GameEnding:
                SetMovement<PostGoalPlayerMovement>();
                break;
            case GameState.GameClear:
                break;
        }
    }

    public void SetMovement<TMovement>() where TMovement : PlayerMovement
    {
        foreach (var movement in movements)
        {
            movement.enabled = movement is TMovement;
            if (movement is TMovement) CurrentMovement = movement;
            OnMovementChanged(CurrentMovement);
        }
    }
}
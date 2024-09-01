using UnityEngine;

class KillOffScreenPlayer : MonoBehaviour
{
    [SerializeField] float bottomMin;
    GameStateManager gameStateManager;

    private void Start()
    {
        gameStateManager = Locator<GameStateManager>.I;
    }

    private void Update()
    {
        if(gameStateManager.CurrentGameState != GameState.GameOver && transform.position.y < bottomMin)
        {
            gameStateManager.ChangeGameState(GameState.GameOver);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPole : MonoBehaviour
{
    [SerializeField] Vector2 poleBottomPos;
    public Vector2 PoleBottomPos => (Vector2)transform.position + poleBottomPos;

    [SerializeField] Vector2 castleDoorPos;
    public Vector2 CastleDoorPos => (Vector2)transform.position + castleDoorPos;

    public void EndGame()
    {
        Locator<GameStateManager>.I.ChangeGameState(GameState.GameClear);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var game = Locator<GameStateManager>.I;
        if (collision.CompareTag("Player") && game.CurrentGameState != GameState.GameEnding)
            game.ChangeGameState(GameState.GameEnding);
    }
}

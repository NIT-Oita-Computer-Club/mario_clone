using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を横断する状態を管理し，変更を公開する．
/// コインの枚数，残機の数，スコアなど．
/// </summary>
public class GameStateManager : MonoBehaviour
{
    public GameState CurrentGameState { get; private set; } = GameState.Playing;
    public UnityAction<GameState> OnStateChange = delegate { };

    public int CoinCount { get; private set; } = 0;
    public UnityAction<int> OnCoinCountChanged = delegate { };
    public int LifeCount { get; private set; } = 5;
    public UnityAction<int> OnLifeCountChanged = delegate { };
    public int Score { get; private set; } = 0;
    public UnityAction<int> OnScoreChanged = delegate { };
    public int TimeRemaining {  get; private set; } = 400;
    public UnityAction<int> OnTimeRemainingChanged = delegate { };

    private void Awake()
    {
        Locator<GameStateManager>.Bind(this);
    }

    private void OnDestroy()
    {
        Locator<GameStateManager>.Unbind(this);
    }

    public void GetCoin()
    {
        CoinCount++;
        OnCoinCountChanged?.Invoke(CoinCount);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
        OnStateChange.Invoke(CurrentGameState);

        switch (newState)
        {
            case GameState.Playing:
            case GameState.GameEnding:
                break;
            case GameState.Pause:
                //
                break;
            case GameState.GameOver:
                StartCoroutine(GameOverCoroutine());
                break;
            case GameState.GameClear:
                StartCoroutine(GameClearCoroutine());
                break;
        }
    }

    IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("1-1");
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("1-1");
    }
}

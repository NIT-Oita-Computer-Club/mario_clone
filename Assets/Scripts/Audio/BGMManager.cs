using UnityEngine;
using System;
using UnityEngine.Assertions;
using System.Collections;

class BGMManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] PlayerStateManager player;
    [SerializeField] PostGoalPlayerMovement postGoalMovement;

    [SerializeField] AudioClip normalBgm;
    [SerializeField] AudioClip downFlagPoleBgm;
    [SerializeField] AudioClip stageClearBgm;
    [SerializeField] AudioClip starBgm;
    [SerializeField] AudioClip gameOverBgm;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var gameStateManager = Locator<GameStateManager>.I;
        OnStateChange(gameStateManager.CurrentGameState, player.CurrentPowerUpState);

        Locator<GameStateManager>.I.OnStateChange += (state) => {
            OnStateChange(state, player.CurrentPowerUpState);
            };

        postGoalMovement.OnLanded += OnLanded;
    }

    private void OnDestroy()
    {
        if (Locator<GameStateManager>.IsValid())
        {
            Locator<GameStateManager>.I.OnStateChange -= (state) => {
                OnStateChange(state, player.CurrentPowerUpState);
            };
        }
        postGoalMovement.OnLanded -= OnLanded;
    }

    void OnLanded()
    {
        StartCoroutine(StageClearBgmCoroutine());
    }

    IEnumerator StageClearBgmCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        audioSource.PlayOneShot(stageClearBgm);
    }

    void OnStateChange(GameState state, PlayerPowerUpState playerPowerUp)
    {
        switch (state)
        {
            case GameState.Playing:
                audioSource.Stop();
                if (playerPowerUp.IsStar) audioSource.PlayOneShot(starBgm);
                else audioSource.PlayOneShot(normalBgm);
                audioSource.loop = true;
                break;
            case GameState.Pause:
                audioSource.Stop();
                break;
            case GameState.GameOver:
                audioSource.Stop();
                audioSource.PlayOneShot(gameOverBgm);
                audioSource.loop = false;
                break;
            case GameState.GameEnding:
                audioSource.Stop();
                audioSource.PlayOneShot(downFlagPoleBgm);
                audioSource.loop = false;
                return;
            case GameState.GameClear:
                break;
        }
    }
}
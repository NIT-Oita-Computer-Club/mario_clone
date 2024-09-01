using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField] PlayerStateManager stateManager;

    Vector2 offsetSmall = new Vector2(0, 0.01f);
    Vector2 offsetLarge = new Vector2(0, 0.51f);
    public const float SizeYSmall = 0.99f;
    public const float SizeYLarge = 1.99f;
    public const float SizeX = 0.75f;

    [SerializeField] BoxCollider2D collisionCollider;
    [SerializeField] BoxCollider2D triggerCollider;

    private void Start()
    {
        OnPlayerStateChanged(stateManager.CurrentPowerUpState);
        stateManager.OnStateChanged += OnPlayerStateChanged;
        Locator<GameStateManager>.I.OnStateChange += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        stateManager.OnStateChanged -= OnPlayerStateChanged;
        if(Locator<GameStateManager>.IsValid())Locator<GameStateManager>.I.OnStateChange -= OnGameStateChanged;
    }

    void OnGameStateChanged(GameState gameState)
    {
        if(gameState == GameState.GameOver)
        {
            collisionCollider.enabled = false;
            triggerCollider.enabled = false;
        }
    }

    void OnPlayerStateChanged(PlayerPowerUpState state)
    {
        if (state.IsInvincible) gameObject.SetLayer(Constants.Layers.InvinciblePlayer);
        else gameObject.SetLayer(Constants.Layers.Player);

        switch (state.Growth)
        {
            case PlayerGrowth.Small:
                collisionCollider.offset = offsetSmall;
                collisionCollider.size = new Vector2(SizeX, SizeYSmall);
                triggerCollider.offset = offsetSmall;
                triggerCollider.size = new Vector2(SizeX, SizeYSmall);
                break;
            case PlayerGrowth.Large:
            case PlayerGrowth.Fire:
                collisionCollider.offset = offsetLarge;
                collisionCollider.size = new Vector2(SizeX, SizeYLarge);
                triggerCollider.offset = offsetLarge;
                triggerCollider.size = new Vector2(SizeX, SizeYLarge);
                break;
        }
    }
}

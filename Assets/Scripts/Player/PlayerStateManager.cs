using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerGrowth
{
    Small = 0,
    Large = 1,
    Fire = 2,
}

public readonly struct PlayerPowerUpState
{
    public PlayerPowerUpState(bool isStar, bool isInvincible, PlayerGrowth growth)
    {
        IsStar = isStar;
        IsInvincible = isInvincible;
        Growth = growth;
    }

    public bool IsStar { get; }
    public bool IsInvincible { get; }
    public PlayerGrowth Growth { get; }

    public PlayerPowerUpState Copy(bool? isStar = null, bool? isInvincible = null, PlayerGrowth? growth = null)
    {
        return new PlayerPowerUpState(
                isStar ?? this.IsStar,
                isInvincible ?? this.IsInvincible,
                growth ?? this.Growth
            );
    }
}


public class PlayerStateManager: MonoBehaviour
{
    const float InvincibleTime = 2.0f;

    public PlayerPowerUpState CurrentPowerUpState { get; private set; } 
        = new PlayerPowerUpState(false,false, PlayerGrowth.Small); // �ŏ��̓X�^�[�ł͂Ȃ�Small��Ԃ���
    public event Action<PlayerPowerUpState> OnStateChanged = delegate { };
    public event Action OnInjured = delegate { };

    // �v���C���[�̃p���[�A�b�v�ɉ������R���|�[�l���g�̑Ή�
    Dictionary<PlayerGrowth, MonoBehaviour[]> playerComponents = new();

    private void Awake()
    {
        playerComponents[PlayerGrowth.Small] = new MonoBehaviour[] { };
        playerComponents[PlayerGrowth.Large] = new MonoBehaviour[] { };
        playerComponents[PlayerGrowth.Fire] =
            new MonoBehaviour[] { 
            // Fire�̋�����ǉ�
            };
    }

    void Start()
    {
        SetState(CurrentPowerUpState);
    }

    void DisableAll()
    {
        //
    }

    public void ChangeState(PlayerPowerUpState newState)
    {
        DisableAll();
        SetState(newState);
        if(newState.Growth - CurrentPowerUpState.Growth < 0) OnInjured.Invoke();
        CurrentPowerUpState = newState;
        OnStateChanged.Invoke(newState);
    }

    public void InjurePlayer()
    {
        if (CurrentPowerUpState.IsInvincible || CurrentPowerUpState.IsStar) return;

        StartCoroutine(OnInjuredCoroutine());
        switch (CurrentPowerUpState.Growth)
        {
            case PlayerGrowth.Small:
                Locator<GameStateManager>.I.ChangeGameState(GameState.GameOver);
                break;
            case PlayerGrowth.Large:
                ChangeState(CurrentPowerUpState.Copy(growth: PlayerGrowth.Small));
                break;
            case PlayerGrowth.Fire:
                // �����͏���}���I�̎d�l�ɍ��킹�邩���D�݂�
                ChangeState(CurrentPowerUpState.Copy(growth: PlayerGrowth.Large));
                break;
        }
    }

    IEnumerator OnInjuredCoroutine()
    {
        ChangeState(CurrentPowerUpState.Copy(isInvincible: true));
        yield return new WaitForSeconds(InvincibleTime);
        ChangeState(CurrentPowerUpState.Copy(isInvincible: false));
    }

    void SetState(PlayerPowerUpState newState)
    {
        // �R���|�[�l���g�L����Ԃ̊���U��
        if (CurrentPowerUpState.IsStar)
        {

        }
        else
        {

        }

        foreach (var component in playerComponents[CurrentPowerUpState.Growth])
        {
            component.enabled = true;
        }
    }
}
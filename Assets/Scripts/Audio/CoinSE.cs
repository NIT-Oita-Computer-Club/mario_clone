using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSE : MonoBehaviour
{
    [SerializeField] AudioClip coinSE;

    void Start()
    {
        Locator<GameStateManager>.I.OnCoinCountChanged += PlayCoinSE;
    }

    private void OnDestroy()
    {
        if(Locator<GameStateManager>.IsValid()) Locator<GameStateManager>.I.OnCoinCountChanged -= PlayCoinSE;
    }

    void PlayCoinSE(int count)
    {
        Locator<SEManager>.I.Play(coinSE);
    }
}

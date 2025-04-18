using UnityEngine;

class EmissionSE : MonoBehaviour
{
    BlockEmittion emittion;
    [SerializeField] AudioClip powerUpEmitSE;

    void Start()
    {
        emittion = GetComponent<BlockEmittion>();
        emittion.OnEmitItem += MakeSE;
    }

    void OnDestroy()
    {
        emittion.OnEmitItem -= MakeSE;
    }

    void MakeSE(BlockContent content)
    {
        if (content != BlockContent.Coin)
        {
            Locator<SEManager>.I.Play(powerUpEmitSE);
        }
    }
}
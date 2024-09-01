using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

/// <summary>
/// ブロックのアイテム排出．
/// ブロックからクリボーを出したりなどはできない設計にした（マリオメーカーの仕様ではないということで）
/// </summary>
public class BlockEmittion : MonoBehaviour
{
    [SerializeField] BlockContent blockContent;

    // EmittionBlockのインスタンス間で共有されるstaticなプレハブの参照
    static GameObject coinPfb;
    static GameObject mushroomPfb;
    static GameObject starPfb;
    static GameObject oneUpMushroomPfb;
    static GameObject fireflowerPfb;
    public event UnityAction OnEmitItem = delegate { };

    private void Awake()
    {
        string path = "Prefabs/BlockContents";
        coinPfb = coinPfb != null ?
            coinPfb : Addressables.LoadAssetAsync<GameObject>($"{path}/CoinInBlock.prefab").WaitForCompletion();
        mushroomPfb = mushroomPfb != null ?
            mushroomPfb : Addressables.LoadAssetAsync<GameObject>($"{path}/MushroomInBlock.prefab").WaitForCompletion();
        starPfb = starPfb != null ?
            starPfb : Addressables.LoadAssetAsync<GameObject>($"{path}/MushroomInBlock.prefab").WaitForCompletion();
        oneUpMushroomPfb = oneUpMushroomPfb != null ?
            oneUpMushroomPfb : Addressables.LoadAssetAsync<GameObject>($"{path}/MushroomInBlock.prefab").WaitForCompletion();
        fireflowerPfb = fireflowerPfb != null ?
            fireflowerPfb : Addressables.LoadAssetAsync<GameObject>($"{path}/FireflowerInBlock.prefab").WaitForCompletion();
    }

    public void Emit(bool isLarge)
    {
        StartCoroutine(EmitCoroutine(isLarge));
    }

    IEnumerator EmitCoroutine(bool isLarge)
    {
        OnEmitItem.Invoke();
        float emitDelaySeconds = default;

        switch (blockContent)
        {
            case BlockContent.Mushroom:
            case BlockContent.FireFlower:
            case BlockContent.Star:
            case BlockContent.OneUpMushroom:
                emitDelaySeconds = 0.3f;
                break;
            case BlockContent.Coin:
                emitDelaySeconds = 0f;
                break;
        }

        yield return new WaitForSeconds(emitDelaySeconds);

        GameObject emitPfb = default;
        switch (blockContent)
        {
            case BlockContent.FireFlower:
                if (isLarge) emitPfb = fireflowerPfb;
                else emitPfb = mushroomPfb;
                break;
            case BlockContent.Mushroom:
                emitPfb = mushroomPfb;
                break;
            case BlockContent.Star:
                emitPfb = starPfb;
                break;
            case BlockContent.OneUpMushroom:
                emitPfb = oneUpMushroomPfb;
                break;
            case BlockContent.Coin:
                emitPfb = coinPfb;
                break;
        }

        Instantiate(emitPfb, transform.position, Quaternion.identity);
    }
}

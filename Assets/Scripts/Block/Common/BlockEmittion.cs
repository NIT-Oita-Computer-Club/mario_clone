using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

/// <summary>
/// �u���b�N�̃A�C�e���r�o�D
/// �u���b�N����N���{�[���o������Ȃǂ͂ł��Ȃ��݌v�ɂ����i�}���I���[�J�[�̎d�l�ł͂Ȃ��Ƃ������ƂŁj
/// </summary>
public class BlockEmittion : MonoBehaviour
{
    [SerializeField] BlockContent blockContent;

    // EmittionBlock�̃C���X�^���X�Ԃŋ��L�����static�ȃv���n�u�̎Q��
    public event UnityAction<BlockContent> OnEmitItem = delegate { };

    const float emittionTime = 1.0f;

    public void Emit(bool isLarge)
    {
        StartCoroutine(EmitCoroutine(isLarge));
    }

    IEnumerator EmitCoroutine(bool isLarge)
    {
        float emitDelaySeconds = 0f;

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

        OnEmitItem.Invoke(blockContent);

        GameObject emitPfb = default;
        var pfbHolder = Locator<PrefabHolder>.I;

        var appearAnim = true;

        switch (blockContent)
        {
            case BlockContent.FireFlower:
                if (isLarge) emitPfb = pfbHolder.FireflowerPfb;
                else emitPfb = pfbHolder.MushroomPfb;
                break;
            case BlockContent.Mushroom:
                emitPfb = pfbHolder.MushroomPfb;
                break;
            case BlockContent.Star:
                emitPfb = pfbHolder.StarPfb;
                break;
            case BlockContent.OneUpMushroom:
                emitPfb = pfbHolder.OneUpMushroomPfb;
                break;
            case BlockContent.Coin:
                emitPfb = pfbHolder.CoinPfb;
                appearAnim = false;
                break;
        }

        var ins = Instantiate(emitPfb, transform.position, Quaternion.identity);

        if (appearAnim) StartCoroutine(AppearCoroutine(ins));

    }

    IEnumerator AppearCoroutine(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody2D>();
        var colliders = obj.GetComponents<Collider2D>();

        rb.simulated = false;
        foreach (var col in colliders) col.enabled = false;

        Vector3 targetPosition = transform.position + Vector3.up;
        yield return CoroutineUtil.MoveToInTime(obj.transform, targetPosition, emittionTime);
        foreach (var col in colliders) col.enabled = true;
        rb.simulated = true;
    }
}

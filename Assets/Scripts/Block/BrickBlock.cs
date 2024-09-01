using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����K�u���b�N�D�p���[�A�b�v��ԂŒ@���Ƃ����ɔj�󂳂��D
/// </summary>
public class BrickBlock : MonoBehaviour, IHittable
{
    [SerializeField] AudioClip breakSE;
    [SerializeField] BlockAnimation blockAnimation;

    public void Hit(bool isLarge)
    {
        if (isLarge)
        {
            Locator<SEManager>.I.Play(breakSE);
            Destroy(gameObject);
            return;
        }

        blockAnimation.Play();
    }
}
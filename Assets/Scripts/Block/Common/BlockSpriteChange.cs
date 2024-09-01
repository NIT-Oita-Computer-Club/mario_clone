using UnityEngine;

class BlockSpriteChange: MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteToChange;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = spriteToChange;
    }
}
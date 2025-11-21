using UnityEngine;
public class ArrowSprite
{
    protected SpriteRenderer spriteRenderer;
    protected GameObject gameObject;

    public ArrowSprite(GameObject gameObject)
    {
        this.gameObject = gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }
    
    public virtual void SetSprite(Sprite newSprite, Sprite defaultSprite)
    {
        spriteRenderer.sortingOrder = 14;
           spriteRenderer.sprite = defaultSprite;
        
    }
    //     public void SetSprite(Sprite newSprite, Sprite defaultSprite)
    // {
    //     spriteRenderer.sprite = defaultSprite;
    //     spriteRenderer.sortingOrder = 14;
    // }
}

public class ActualArrowSprite : ArrowSprite
{
    public ActualArrowSprite(GameObject gameObject) : base(gameObject)
    {
    }
    
    public override void SetSprite(Sprite newSprite, Sprite defaultSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
    // public void SetSprite(Sprite newSprite, Sprite defaultSprite)
    // {
    //     spriteRenderer.sprite = newSprite;
    // }
}
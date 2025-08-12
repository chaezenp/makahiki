using UnityEngine;

public class LevelNameChange : MonoBehaviour
{
    public LevelProgress levelProgress;

    [Header("Level Info")]
    public string Level1 = "Level1";
    public string Level2 = "Level 2";
    public string Level3 = "Level 3";

    [Header("Sprites")]
    public Sprite Level1Sprite;
    public Sprite Level2Sprite;
    public Sprite Level3Sprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (levelProgress.HasWonLevel(Level1))
        {
            spriteRenderer.sprite = Level2Sprite;
        }
        else
        {
            spriteRenderer.sprite = Level1Sprite;
        }
        if (Level3Sprite != null)
        {
            if (levelProgress.HasWonLevel(Level1) && levelProgress.HasWonLevel(Level2))
            {
                spriteRenderer.sprite = Level3Sprite;
            }
        }
    }
}


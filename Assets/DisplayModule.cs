using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designed for displaying images like sprites. 
/// oftenly connected with button modules.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class DisplayModule : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private int maxSpriteCount = 0;
    private int currentIndex = 0;

    [SerializeField]
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        maxSpriteCount = sprites.Length - 1;
    }

    public void NextSprite()
    {
        if (currentIndex == maxSpriteCount)
        {
            currentIndex = 0;
            spriteRenderer.sprite = sprites[currentIndex];
        }
        else
            MoveSprite(1);
    }

    public void PreviousSprite()
    {
        if(currentIndex == 0)
        {
            currentIndex = maxSpriteCount;
            spriteRenderer.sprite = sprites[currentIndex];
            return;
        }
        else
            MoveSprite(-1);
    }

    private void MoveSprite(int index)
    {
        currentIndex += index;
        spriteRenderer.sprite = sprites[currentIndex];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class HPSystem : MonoBehaviour
{
    [BoxGroup("Health Points")]
    public float maxHp, hp;

    [BoxGroup("Sprites")]
    public SpriteRenderer spriteRender;
    [BoxGroup("Sprites")]
    public Sprite[] sprites;

    private void FixedUpdate()
    {
        if((hp/maxHp) >= .66f)
        {
            spriteRender.sprite = sprites[0];
        }
        else if ((hp / maxHp) >= .33f)
        {
            spriteRender.sprite = sprites[1];
        }
        else if ((hp / maxHp) > 0)
        {
            spriteRender.sprite = sprites[2];
        }
        else
        {
            spriteRender.sprite = sprites[3];
        }
    }
}

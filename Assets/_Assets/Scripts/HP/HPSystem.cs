using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;


public class HPSystem : MonoBehaviour
{
    public Image hpBar;

    [BoxGroup("Health Points")]
    public float maxHp, hp;

    [BoxGroup("Sprites")]
    public SpriteRenderer spriteRender;
    [BoxGroup("Sprites")]
    public Sprite[] sprites;

    private void Update()
    {
        hpBar.fillAmount = hp / maxHp;
    }

    private void FixedUpdate()
    {
        if(hpBar.fillAmount >= .66f)
        {
            spriteRender.sprite = sprites[0];
        }
        else if (hpBar.fillAmount >= .33f)
        {
            spriteRender.sprite = sprites[1];
        }
        else if (hpBar.fillAmount > 0)
        {
            spriteRender.sprite = sprites[2];
        }
        else
        {
            spriteRender.sprite = sprites[3];
        }
    }
}

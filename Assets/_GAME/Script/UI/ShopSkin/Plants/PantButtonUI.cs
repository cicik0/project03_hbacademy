using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantButtonUI : MonoBehaviour
{
    [SerializeField] private Image pantButtonImage;
    [SerializeField] private EPantsType.pantsType itemType = EPantsType.pantsType.NONE;

    private bool IsBuy = false;

    public void OnInit(Sprite itemSprite, EPantsType.pantsType itemType)
    {
        pantButtonImage.sprite = itemSprite;
        this.itemType = itemType;
    }
}

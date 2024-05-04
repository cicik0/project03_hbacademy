using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadButtonUI : MonoBehaviour
{
    [SerializeField] private Image itemButtonImage;
    [SerializeField] private EHeadType.headType itemType = EHeadType.headType.NONE;

    private bool IsBuy = false;

    public void OnInit(Sprite itemSprite, EHeadType.headType itemType)
    {
        itemButtonImage.sprite = itemSprite;
        this.itemType = itemType;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonHeadSkin", menuName = "HeadSkinList" )]

public class ShopHeadButtonSO : ScriptableObject
{
    public List<ShopHeadButtonData> listHeads;
}

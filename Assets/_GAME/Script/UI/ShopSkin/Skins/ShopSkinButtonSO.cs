using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSkin", menuName = "SkinList")]

public class ShopSkinButtonSO : ScriptableObject
{
    public List<ShopSkinButtonData> listSkins; 
}

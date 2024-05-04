using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsSelectionUI : MonoBehaviour
{
    [SerializeField] private PantButtonUI pantButtonPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private ShopPantsButtonSO pantsButtonList;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPantButtonList();
    }

    private void SpawnPantButton(Sprite itemButtonSprite, EPantsType.pantsType pantType)
    {
        PantButtonUI pantButtonUI = Instantiate(pantButtonPrefab, parentTransform);
        pantButtonUI.OnInit(itemButtonSprite, pantType);
    }

    private void SpawnPantButtonList()
    {
        for (int i = 0; i < pantsButtonList.listPants.Count; i++)
        {
            SpawnPantButton(pantsButtonList.listPants[i].plantIcon, pantsButtonList.listPants[i].pantType);
        }
    }
}

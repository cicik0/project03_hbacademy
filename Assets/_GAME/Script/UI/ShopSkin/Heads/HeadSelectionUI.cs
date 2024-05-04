using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSelectionUI : MonoBehaviour
{
    [SerializeField] private HeadButtonUI headButtonPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private ShopHeadButtonSO headButtonDataSO;

    // Start is called before the first frame update
    void Start()
    {
        SpawnHeadButtonList();
    }

    private void SpawnHeadButton(Sprite itemButtonSprite, EHeadType.headType headType)
    {
        HeadButtonUI itemButtonUI = Instantiate(headButtonPrefab, parentTransform);
        itemButtonUI.OnInit(itemButtonSprite, headType);
    }

    private void SpawnHeadButtonList()
    {
        for (int i = 0; i < headButtonDataSO.listHeads.Count; i++)
        {
            SpawnHeadButton(headButtonDataSO.listHeads[i].headIcon, headButtonDataSO.listHeads[i].headType);
        }
    }
}

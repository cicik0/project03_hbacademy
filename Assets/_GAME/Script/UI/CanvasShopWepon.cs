using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopWepon : UICanvas
{
    [SerializeField] Button buyButton;
    [SerializeField] Button fontWepon;
    [SerializeField] Button backWepon;
    [SerializeField] Button cancelButton;
    [SerializeField] TextMeshProUGUI costOfWp;
    [SerializeField] WeponListSO wpListSO;
    [SerializeField] GameObject wepon;

    private List<GameObject> listWpInShop = new List<GameObject>();
    private int countOfList = 0;

    private Vector3 ScaleWpInShop = new Vector3(10f, 10f, 10f);
    private Vector3 RotationWpInShop = new Vector3(90f, 0f, 10f);


    public override void SetUp()
    {
        base.SetUp();
        SetWpInShop();
    }

    private void Start()
    {
        //SetUp();
        buyButton.onClick.AddListener(() => BuyButton());
        fontWepon.onClick.AddListener(() => FontWeponButton());
        backWepon.onClick.AddListener(() => BackWeponbutton());
        cancelButton.onClick.AddListener(() => CancelButton());
        ActiveWpInShop(countOfList);
        SetCostOfWp();
    }

    private void SetWpInShop()
    {
        for (int i=0; i<wpListSO.listWepon.Count; i++)
        {
            GameObject currentWepon = Instantiate(wpListSO.listWepon[i].weponPrefab, wepon.transform);
            if (wpListSO.listWepon[i].weponType == EWeponType.WeponType.BOOME || wpListSO.listWepon[i].weponType == EWeponType.WeponType.Z)
            {
                currentWepon.transform.localScale = ScaleWpInShop;
                currentWepon.transform.localRotation = Quaternion.Euler(currentWepon.transform.localRotation.eulerAngles + RotationWpInShop);
            }
            currentWepon.SetActive(false);
            //currentWepon.transform.localRotation = new Vector3(0, 0, 30);
            listWpInShop.Add(currentWepon);
        }

    }

    private void ActiveWpInShop(int count)
    {
        listWpInShop[count].gameObject.SetActive(true);
    }

    private void DeActiveWpInShop(int count)
    {
        listWpInShop[count].gameObject.SetActive(false);
    }

    private void SetCostOfWp()
    {
        costOfWp.text = wpListSO.listWepon[countOfList].coin.ToString();
    }

    private void CancelButton()
    {
        UIManager.Instance.CloseUI<CanvasShopWepon>(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    private void BackWeponbutton()
    {
        if(countOfList < wpListSO.listWepon.Count-1)
        {
            DeActiveWpInShop(countOfList);
            countOfList++;
            ActiveWpInShop(countOfList);
            SetCostOfWp();
        }
    }

    private void FontWeponButton()
    {
        if (countOfList > 0)
        {
            DeActiveWpInShop(countOfList);
            countOfList--;
            ActiveWpInShop(countOfList);
            SetCostOfWp();
        }
    }

    private void BuyButton()
    {

    }
}

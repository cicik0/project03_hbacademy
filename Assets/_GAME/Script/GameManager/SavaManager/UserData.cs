using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class UserData 
{
    public int coin;
    public List<int> listStatusWp;
    public List<int> listStatusHead;
    public List<int> listStatusPant;
    public List<int> listStatusSkin;

    public UserData(int coin, List<int> listWp, List<int> listHead, List<int> listPant, List<int> listSkin)
    {
        this.coin = coin;
        this.listStatusWp = listWp;
        this.listStatusHead = listHead;
        this.listStatusPant = listPant; 
        this.listStatusSkin = listSkin;
    }
}

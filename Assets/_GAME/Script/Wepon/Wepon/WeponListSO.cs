using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wepon", menuName = "WeponList")]
public class WeponListSO : ScriptableObject
{
    public List<WeponData> listWepon;
}

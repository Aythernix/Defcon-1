using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopAssets", menuName = "ShopAssets")]
public class ShopAssets : ScriptableObject
{
    [Header("Shop Assets")]
    public string assetName;
    public string assetTooltip;
    public string assetDescription;
    public int assetCost;
}

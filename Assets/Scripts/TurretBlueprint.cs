using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public int sell;

    public GameObject[] upgradedPrefabs;
    public int upCost;
    public int sellCost;
    
}

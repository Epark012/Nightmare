using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data management for enemies. 
/// Datas found by enemies are registed in this list.
/// </summary>
public  class Cortex : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemiesList = new List<Enemy>();
    [SerializeField]
    private List<Mineral> mineralList = new List<Mineral>();

    private void Start()
    {
        InitEnemies();
    }

    private void InitEnemies()
    {
        enemiesList?.Add(GetComponentInChildren<Enemy>());
    }

    private bool CheckMineralList()
    {
        return mineralList.Count > 0;
    }

    //Add Mineral found by drone
    public void AddMineral(Mineral mineral)
    {
        if(!mineralList.Contains(mineral))
            mineralList.Add(mineral);
    }

    //Remove Mineral From List
    public void RemoveMineral(Mineral mineral)
    {
        if (CheckMineralList())
            mineralList.Remove(mineral);
        else
        {
            //Call GameOver
        }
    }
}

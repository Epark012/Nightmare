using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data management for enemies. 
/// Datas found by enemies are registed in this list.
/// </summary>

[Serializable]
public class MineralInCortex
{
    public int key = 0;
    public Mineral value;

    public MineralInCortex(int droneID, Mineral mineral)
    {
        key = droneID;
        value = mineral;
    }
}


public  class Cortex : MonoBehaviour
{
    private int lastIndex = 0;
    private int index = 0;

    [SerializeField]
    private List<MineralInCortex> mineralInCortex = new List<MineralInCortex>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public int MineralCountInCortex { get { return mineralInCortex.Count; } }

    public Vector3 GetPatrolPosition()
    {
        index = (index == mineralInCortex.Count - 1) ? 0 : index + 1;
        return mineralInCortex[index].value.transform.position;
    }

    public Mineral GetTargetMineral()
    {
        Debug.Log("GetTargetMineral() is targeting : " + mineralInCortex[lastIndex]);
        return mineralInCortex[lastIndex].value;
    }

    public void AddToDictionary(int id, Mineral mineral)
    {
        lastIndex++;
        if (mineralInCortex.Count == 0)
            mineralInCortex.Add(new MineralInCortex(id, mineral));
        else
        {
            if (!IsInDictionary(mineral))
            {
                mineralInCortex.Add(new MineralInCortex(id, mineral));
                Debug.Log(mineral.transform.name + " has been added to Cortex.");
            }
            else
                Debug.Log(mineral.name + " is already in the Cortex");
        }
    }

    //Remove by drone destruction
    //When drone is destroyed, all minerals added by the drone, will be destoryed.
    public void RemoveFromDictionary(int id)
    {
        if (mineralInCortex.Count > 0)
        {
            lastIndex--;
            if(IsInDictionary(id))
            {
                for(int i = 0; i < mineralInCortex.Count -1; i++)
                {
                    if(mineralInCortex[i].key == id)
                    {
                        Debug.Log(mineralInCortex[i].value.name + " has been removed from Cortex.");
                        mineralInCortex.Remove(mineralInCortex[i]);
                    }
                }
            }
        }
    }

    private bool IsInDictionary(Mineral mineral)
    {
        if (mineralInCortex.Count > 0)
        {
            for (int i = 0; i < mineralInCortex.Count - 1; i++)
            {
                if (mineralInCortex[i].value == mineral)
                {
                    Debug.Log(mineralInCortex[i].value.name + " has found in MineralInCortices");
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsInDictionary(int id)
    {
        if (mineralInCortex.Count > 0)
        {
            for (int i = 0; i < mineralInCortex.Count - 1; i++)
            {
                if (mineralInCortex[i].key == id)
                {
                    Debug.Log(mineralInCortex[i].value.name + " has found in MineralInCortices");
                    return true;
                }
            }
        }
        return false;
    }
}

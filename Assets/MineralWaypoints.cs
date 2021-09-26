using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralWaypoints : MonoBehaviour
{
    #region Singleton
    static MineralWaypoints _instance = null;
    public static MineralWaypoints Instance()
    {
        return _instance;
    }
    #endregion

    public List<Mineral> MineralLists = new List<Mineral>();


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void DeleteMineral(Mineral mineral)
    {
        MineralLists.Remove(mineral);
    }

    private void OnDrawGizmos()
    {
        if(MineralLists.Count > 0)
        {
            for(int i = 0; i < MineralLists.Count; i++)
            {
                if (i < MineralLists.Count - 1)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(MineralLists[i].transform.position, MineralLists[i + 1].transform.position);
                }
                else
                    Gizmos.DrawLine(MineralLists[i].transform.position, MineralLists[0].transform.position);
            }
        }
    }
}

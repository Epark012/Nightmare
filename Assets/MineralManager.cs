using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    [SerializeField]
    private List<Mineral> mineralList = new List<Mineral>();

    private int lastIndexofArray = 0;
    private int arrayIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        MineralListInit();
    }
    private void MineralListInit()
    {
        lastIndexofArray = gameObject.transform.childCount;
        for (int i = 0; i < lastIndexofArray; i++)
        {
            Mineral mineral = transform.GetChild(i).GetComponent<Mineral>();
            mineralList.Add(mineral);
        }
    }

    public Transform GetNextDestinationFromArray()
    {
        arrayIndex = (arrayIndex >= mineralList.Count - 1) ? 0 : arrayIndex + 1;
        return mineralList[arrayIndex].transform;
    }

    public Mineral GetTargetMineral()
    {
        Debug.Log("GetTargetMineral() is targeting : " + mineralList[lastIndexofArray - 1]);
        return mineralList[lastIndexofArray - 1];
    }
}

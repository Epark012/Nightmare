using UnityEngine;

/// <summary>
/// A script for Mineral
/// 1. Collectable by enemies. 
/// 2. Collectable by player by protecting. (protecting memories)
/// </summary>
public class Mineral : MonoBehaviour
{
    private bool isScanned = false;
    private bool isInfected = false;

    public bool IsScanned { get { return isScanned; } set { isScanned = true; } }
    public bool IsInfected { get { return IsInfected; }  set { IsInfected = value; } }
    
    [SerializeField]
    private MineralManager mineralManager = null;
    [SerializeField]
    private bool isMineralGrenades = false;

    public bool IsMineralGrenades { get {return isMineralGrenades; } }

    // Start is called before the first frame update
    void Start()
    {
        mineralManager = GetComponentInParent<MineralManager>();
    }

    private void AddToMineralList()
    {
        if(!IsInfected)
            mineralManager.AddToList(this);
    }

    //when it is instantiated.
    public void MineralInit()
    {
        //Add to mineral manager
        AddToMineralList();
    }

    //When it is infected, removed from mineralList
    public void InfectedMineral()
    {
        isInfected = true;
        if(IsInfected)
            mineralManager.RemoveFromList(this);
    }
}

using UnityEngine;

/// <summary>
/// A script for Mineral
/// 1. Collectable by enemies. 
/// 2. Collectable by player by protecting. (protecting memories)
/// </summary>
public class Mineral : MonoBehaviour
{
    [SerializeField]
    private GameObject crackedObject;

    // Start is called before the first frame update
    void Start()
    {
        if(MineralWaypoints.Instance().MineralLists != null)
            MineralWaypoints.Instance().MineralLists.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        MineralWaypoints.Instance().DeleteMineral(this);
    }
}

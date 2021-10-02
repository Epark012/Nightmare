using UnityEngine;

/// <summary>
/// Designed for Inventory UI for inventory items and hand spot zone.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    private InventoryGrabZone grabZone;

    [SerializeField]
    private Transform handSpawnZone;

    private void Start()
    {
        grabZone = GetComponentInChildren<InventoryGrabZone>();
    }

    public void Spawn(GameObject spawnObject)
    {
        if (handSpawnZone != null && !grabZone.isOccupied)
        {
            var item = Instantiate(spawnObject, handSpawnZone);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Collider>().isTrigger = true;
            grabZone.isOccupied = true;
        }
        else
            throw new MissingReferenceException("Something went wrong.");
    }
}

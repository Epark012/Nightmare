using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for grab zones in inventory system.
/// When a hand grabs in the collider, gameobject is instantiated.
/// </summary>
public class InventoryGrabZone : MonoBehaviour
{
    public bool isOccupied = false;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IInventoryStorable item))
        {
            isOccupied = false;
            Debug.Log("Inventory Grab Zone Exit " + item);
        }
    }
}

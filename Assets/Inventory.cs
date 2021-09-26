using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(FlickDetector))]
public class Inventory : MonoBehaviour
{

    private void Start()
    {

    }

    public void TestFlick()
    {
        Debug.Log("Inventory is open.");
    }
}

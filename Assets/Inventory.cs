using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(FlickDetector))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private AudioClip inventoryClip;

    private bool isInventoryOpenned = false;
    private AudioSource audioSource;
    private Animator inventoryAnimator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventoryAnimator = inventory.GetComponent<Animator>();
    }

    public void OpenInventory()
    {
        Debug.Log("Inventory openned");
        inventory.SetActive(true);
        isInventoryOpenned = true;
        audioSource.PlayOneShot(inventoryClip);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        isInventoryOpenned = false;
        Debug.Log("Inventory closed.");
        audioSource.PlayOneShot(inventoryClip);
    }
}

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

    //Set 3 for initial socket number
    private InventorySocketInteractor[] sockets = new InventorySocketInteractor[3];


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventoryAnimator = inventory.GetComponent<Animator>();
        sockets = GetComponentsInChildren<InventorySocketInteractor>();
    }

    private bool IsInventoryOpenned()
    {
        return isInventoryOpenned;
    }

    public void CallInventory()
    {
        if(IsInventoryOpenned())
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        //Play Sound
        audioSource.PlayOneShot(inventoryClip);

        //Call Socket the method for Open Logic regarding SelectingTarget Mesh && SocketActive
        for(int i = 0; i < 3; i++)
        {
            sockets[i].InventorySocket(true);
        }

        //Inventory images
        inventoryAnimator.SetTrigger("Open");

        //Call by hand Logic
        isInventoryOpenned = true;

        Debug.Log("Inventory openned");
    }

    private void CloseInventory()
    {
        //Play Sound
        audioSource.PlayOneShot(inventoryClip);

        //Call Socket the method for Open Logic regarding SelectingTarget Mesh && SocketActive
        for (int i = 0; i < 3; i++)
        {
            sockets[i].InventorySocket(false);
        }

        //Inventory images
        inventoryAnimator.SetTrigger("Close");

        //Call by hand Logic
        isInventoryOpenned = false;

        Debug.Log("Inventory closed.");
    }
}

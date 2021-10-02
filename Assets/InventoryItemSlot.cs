using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryItemSlot : XRSimpleInteractable
{
    [SerializeField]
    private int spawnNumber = 3;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private InventoryUI ui;

    protected override void OnHoverEntered(XRBaseInteractor interactor)
    {
        base.OnHoverEntered(interactor);
        OnClick();
    }

    public void OnClick()
    {
        if (spawnNumber > 0)
        {
            ui.Spawn(item);
            Debug.Log("OnClick is called.");
            spawnNumber--;
        }
        else
            Debug.Log("SpawnNumber has reached 0.");
    }

    //Can Hover by only hand.
    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        return base.IsHoverableBy(interactor) && interactor.gameObject.tag == "Player";
    }
}

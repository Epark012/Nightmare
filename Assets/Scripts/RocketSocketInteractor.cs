using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RocketSocketInteractor : WeaponSocketInteractor
{
    /// <summary>
    /// 1. Turn On/Off the mesh of Interactable, as it is not stable while attaching to the socket.
    /// 2. Fire Logic - Turn off the socket to let the rocket fly.
    /// </summary>
    /// <param name="interactable"></param>

    private MeshRenderer mesh;
    private RocketLauncher rocketLauncher;
    [SerializeField]
    private RocketLauncherType rocketLauncherType;

    protected override bool IsCompatiable(XRBaseInteractable interactable)
    {
        if (interactable.GetComponent<Rocket>())
        {
            return ((int)rocketLauncherType == (int)interactable.GetComponent<Rocket>().rocketLauncherType);
        }

        else
            return false;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && IsCompatiable(interactable);
    }
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        rocketLauncher = GetComponentInParent<RocketLauncher>();

        InRocket(interactable);
        base.OnSelectEntered(interactable);
    }

    void InRocket(XRBaseInteractable interactable)
    {
        //1. Set the mesh from interactable.
        mesh = interactable.GetComponent<MeshRenderer>();
        //2. Turn off Mesh
        mesh.enabled = false;
        //3. Tell Launcher which mesh need to be off.
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        rocketLauncher = null;
        //OutRocket(interactable);
        base.OnSelectExited(interactable);
    }

    void OutRocket(XRBaseInteractable interactable)
    {
        //Turn on Mesh.
        mesh.enabled = true;
        //mesh to Null.
        mesh = null;
    }

    public override void ReleaseFire()
    {
        //Fire Logic 

        //Turn Off Socket Active 
        if (socketActive)
            socketActive = false;

        //Timer to cool the weapon
        Invoke("CoolOffWeapon", 3f);

        //Turn On Missile
        selectTarget.GetComponent<Rocket>().isActivated = true;
        selectTarget.GetComponent<MeshRenderer>().enabled = true;
    }

    private void CoolOffWeapon()
    {
        //set Socket Active true
        if (!socketActive)
            socketActive = true;
    }
}

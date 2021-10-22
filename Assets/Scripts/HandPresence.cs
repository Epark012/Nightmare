using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnHandModel;
    private Animator handAnimator;

    [SerializeField]
    private Collider[] colls;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialise();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialise();
        }
        else
        {
            UpdateHandAnimation();
        }
    }
    private void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    private void TryInitialise()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            spawnHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnHandModel.GetComponent<Animator>();
            GetCollsFromObject(spawnHandModel);
        }
    }
    
    private void GetCollsFromObject(GameObject hand)
    {
        colls = hand.GetComponentsInChildren<Collider>();

        if(colls.Length == 0)
        {
            Debug.Log("Physical colliders in hand hierachy is empty.");
        }
    }

    public void TogglePhysics(bool usePhysics)
    {
        if(colls.Length > 0)
        {
            for(int i = 0; i < colls.Length; i++)
            {
                colls[i].isTrigger = !usePhysics;
            }
        }
        else
        {
            GetCollsFromObject(spawnHandModel);
            for (int i = 0; i < colls.Length; i++)
            {
                colls[i].isTrigger = !usePhysics;
            }
        }
    }
}

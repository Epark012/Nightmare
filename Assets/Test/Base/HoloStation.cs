using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloStation : MonoBehaviour, IDreamReceiver
{
    [SerializeField]
    private GameObject[] holos;

    public void DataReceived()
    {
        holos[0].SetActive(true);
    }
}

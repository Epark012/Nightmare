using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReached : MonoBehaviour
{
    private bool sliderLoaded = false;
    public bool SliderLoaded { get { return sliderLoaded;} set { sliderLoaded = value; } } 

    public float threshold = 0.02f;
    public Transform target;

    public UnityEvent OnReached;

    public float distance;


    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshold && !sliderLoaded)
        {
            //Reached the target
            OnReached.Invoke();
            sliderLoaded = true;
        }

        else if (distance >= threshold)
        {
            sliderLoaded = false;
        }
    }

    private void OnDisable()
    {
        sliderLoaded = false;
    }
}

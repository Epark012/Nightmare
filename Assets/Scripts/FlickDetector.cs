using UnityEngine;
using UnityEngine.Events;

public class FlickDetector : MonoBehaviour
{
    [SerializeField]
    private float beginThreshold = 1.25f;
    [SerializeField]
    private float endThreshold = 0.25f;

    public UnityEvent OnFlick = new UnityEvent();
    private bool brokenThreshold = false;

    public void CheckFlick(Hand hand)
    {
        float speed = hand.GetRotationSpeed();
        brokenThreshold = HasFlickBegun(speed);

        if(HasFlickEnded(speed))
        {
            OnFlick.Invoke();
            hand.IsFlicked = true;
            Reset();
        }
    }

    private bool HasFlickBegun(float speed)
    {
        return brokenThreshold ? true : (speed > beginThreshold);
    }

    private bool HasFlickEnded(float speed)
    {
        return brokenThreshold ? (speed < endThreshold) : false;
    }

    private void Reset()
    {
        brokenThreshold = false;
    }
}

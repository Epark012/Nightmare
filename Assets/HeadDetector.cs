using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Designed for Head Collions. 
/// Events from the collisions are Head Blocking and Scene Management with Nightmare Headset.
/// </summary>
public class HeadDetector : MonoBehaviour
{
    private bool isCalled = false;

    public UnityEvent OnHeadSetOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VRHead") && !isCalled)
        {
            OnHeadSetOn.Invoke();
            isCalled = true;
        }
    }

    //Load Scene 
    public void SceneTransition(int index)
    {
        //Fade and SceneTransition
        NightmareSceneManagement.Instance.LoadScene(index);
    }
}

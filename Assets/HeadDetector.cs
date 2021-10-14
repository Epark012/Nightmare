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
    public UnityEvent OnHeadSetOn;

    [SerializeField]
    private SpriteRenderer fader;

    private void Awake()
    {
        fader.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out NightmareHeadset headset))
        {
            if(headset.IsHold)
            {
                Debug.Log(headset.gameObject.name + " is detected in OnTriggerEnter.");
                OnHeadSetOn.Invoke();
            }
        }
    }

    //Load Scene 
    public void SceneTransition(int index)
    {
        //Fade and SceneTransition
        NightmareSceneManagement.LoadScene(index, 1);
    }
}

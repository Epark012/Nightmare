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
            Debug.Log(other.gameObject.name + " is detected in OnTriggerEnter.");
            Debug.Log("Headset Event is triggered.");
            OnHeadSetOn.Invoke();
        }
    }

    //Load Scene 
    public void SceneTransition(int index)
    {
        //Fade and SceneTransition
        LoadScene(index, 3, 3);
    }

    public void LoadScene(int index, float fadeDuration, float sceneTransitionTime)
    {
        StartCoroutine(FadeScene(index, fadeDuration, sceneTransitionTime));
    }

    private IEnumerator FadeScene(int index, float duration, float waitTime)
    {
        fader.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(index);
        while (!ao.isDone)
            yield return null;
        yield return new WaitForSeconds(waitTime);

        fader.gameObject.SetActive(false);
    }
}

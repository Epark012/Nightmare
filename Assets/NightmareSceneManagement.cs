using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightmareSceneManagement : MonoBehaviour
{
    private static NightmareSceneManagement Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

   /* public static void LoadScene(int index, float fadeDuration, float sceneTransitionTime)
    {
        Instance.StartCoroutine(Instance.FadeScene(index, fadeDuration, sceneTransitionTime));
    }

    private IEnumerator FadeScene(int index, float duration, float waitTime)
    {
        fader.gameObject.SetActive(true);
        
        for(float t = 0; t < 1; t += Time.deltaTime/duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(index);
        while (!ao.isDone)
            yield return null;
        yield return new WaitForSeconds(waitTime);

        fader.gameObject.SetActive(false);
    }*/
}

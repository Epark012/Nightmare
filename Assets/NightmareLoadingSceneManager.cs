using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Desigined for Loading Scene Manager in Loading Scene.
/// 1. Loading for target scene.
/// 2. Animation Management while loading.
/// </summary>
public class NightmareLoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private Nightmare_XRRig xRRig;
    [SerializeField]
    private int loadingSceneTime = 5;

    private void Start()
    {
        if(xRRig == null)
        {
            SceneInIt();
        }

        StartCoroutine(ProcessLoadingScene());
    }

    private void SceneInIt()
    {
        xRRig = FindObjectOfType<Nightmare_XRRig>();
    }

    IEnumerator ProcessLoadingScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(NightmareSceneManagement.targetSceneIndex);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;


            if(op.progress < 0.9f)
            {
                //
                Debug.Log("nextSceneIndex : " + NightmareSceneManagement.targetSceneIndex);
            }
            else
            {
                timer += Time.deltaTime;
                //Last 10%
                //Set condition to finish op.progress
                if(timer > loadingSceneTime)
                {
                    StartCoroutine(LoadingNextScene(op));
                    yield break;
                }
            }
        }
    }

    IEnumerator LoadingNextScene(AsyncOperation op)
    {
        xRRig.FadeOutScreen();

        yield return new WaitForSeconds(xRRig.FadeDuration);

        op.allowSceneActivation = true;
    }
}

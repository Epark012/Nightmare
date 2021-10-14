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
    static int nextSceneIndex = 0;

    public static void SetSceneIndex(int SceneIndex)
    {
        nextSceneIndex = SceneIndex;
        //ceneManager.LoadSceneAsync(nextSceneIndex);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneIE());
    }

    IEnumerator LoadSceneIE()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneIndex);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(op.progress < 0.9f)
            {
                //
                Debug.Log("nextSceneIndex : " + nextSceneIndex );
            }
            else
            {
                //Last 10%
                //Set condition to finish op.progress
                if(timer>5.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

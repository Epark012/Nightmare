using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Desinged for Nightmare Scene Management.
/// When it is called to load next scenes or Main scene, it will visit Loading Scene. 
/// When it is loading scenes, it will call a function to fade the vision of camera. 
/// it needs to have reference for camera in each scene.
/// </summary>
public class NightmareSceneManagement : MonoBehaviour
{
    private static NightmareSceneManagement Instance;
    private Nightmare_XRRig xrRig;

    [Header("Scene Setting")]
    [SerializeField]
    private int loadingSceneIndex;

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

    //Called in each scene to find references
    private void SceneInit()
    {
        //Find XR Rig
        xrRig = FindObjectOfType<Nightmare_XRRig>();
    }

   public static void LoadScene(int nextSceneIndex, float fadeDuration)
    {
        Instance.StartCoroutine(Instance.LoadSceneIE(nextSceneIndex, fadeDuration));
    }

    private IEnumerator LoadSceneIE(int NextSceneIndex, float fadeDuration)
    {
            //Fade Operation
        if(xrRig != null)
        {
            xrRig.FadeScene();
        }
        else
        {
            SceneInit();
            xrRig.FadeScene();
        }
        yield return new WaitForSeconds(fadeDuration);
        
        //Move to Loading Scene
        NightmareLoadingSceneManager.SetSceneIndex(NextSceneIndex);
        AsyncOperation ao = SceneManager.LoadSceneAsync(loadingSceneIndex);
        while (!ao.isDone)
            yield return null;
    }
}

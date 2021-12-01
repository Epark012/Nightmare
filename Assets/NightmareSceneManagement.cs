using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Desinged for Nightmare Scene Management.
/// When it is called to load next scenes or Main scene, it will visit Loading Scene. 
/// When it is loading scenes, it will call a function to fade the vision of camera. 
/// it needs to have reference for camera in each scene.
/// </summary>
public class NightmareSceneManagement : MonoBehaviour
{
    public static int targetSceneIndex = 0;
    private static int LoadingSceneIndex;
    private AudioSource audioSource;
    public static NightmareSceneManagement Instance;

    [SerializeField]
    private Nightmare_XRRig xrRig;

    [Header("Scene Setting")]
    [SerializeField]
    private int loadingSceneIndex;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private AudioClip countDownClip;

    public UnityEvent OnSceneTransition = null;

    private void Awake()
    {
        LoadingSceneIndex = loadingSceneIndex;
        audioSource = GetComponent<AudioSource>();

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

    public void LoadScene(int nextSceneIndex)
    {

        targetSceneIndex = nextSceneIndex;
        Instance.StartCoroutine(Instance.LoadSceneIE(nextSceneIndex));

        //Trigger OnSceneTransition Event
        OnSceneTransition.Invoke();
    }

    public void onSceneTransition()
    {
        audioSource.PlayOneShot(countDownClip);
    }

    private void CallFade()
    {
        if (xrRig != null)
        {
            xrRig.FadeOutScreen();
        }
        else
        {
            SceneInit();
            xrRig.FadeOutScreen();
        }
    }

    private IEnumerator LoadSceneIE(int NextSceneIndex)
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(LoadingSceneIndex);
        op.allowSceneActivation = false;


        float timer = 0;

       while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(timer < 3.6f && !op.allowSceneActivation)
            {
                Debug.Log("Changing Scene.");
            }
            else
            {
                //Fade Operatio
                CallFade();
                yield return new WaitForSeconds(xrRig.FadeDuration);
                op.allowSceneActivation = true;
                yield break;
            }
        }
        yield return null;
    }
}

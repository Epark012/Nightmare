using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTest : MonoBehaviour
{
    public int targetSceneIndex;
    public void LoadSceneTest()
    {
        Debug.Log("Selected");
        NightmareSceneManagement.Instance.LoadScene(targetSceneIndex);
    }
}

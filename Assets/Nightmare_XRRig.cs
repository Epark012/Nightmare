using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Designed for referencing fader
/// </summary>
public class Nightmare_XRRig : XRRig
{
    [Header("Fade Settings")]
    [Tooltip("Sprite for Fade Transition")]
    [SerializeField]
    private SpriteRenderer fader;
    [Tooltip("Duration for Fade Transition")]
    [SerializeField]
    private float fadeDuration;

    public float FadeDuration { get { return fadeDuration; } }

    protected void Start()
    {
        FadeInScreen();
    }

    public void FadeInScreen()
    {
        StartCoroutine(FadeIntoScene(fadeDuration));
    }

    public void FadeOutScreen()
    {
        StartCoroutine(FadeOutScene(fadeDuration));
    }
    private IEnumerator FadeOutScene(float duration)
    {
        fader.gameObject.SetActive(true);
        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            Debug.Log("In FadeOut");
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

    }
    private IEnumerator FadeIntoScene(float duration)
    {
        fader.gameObject.SetActive(true);
        
        for(float t = 1; t > 0; t -= Time.deltaTime/duration)
        {
            Debug.Log("In FadeIn");
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        fader.gameObject.SetActive(false);
    }
}

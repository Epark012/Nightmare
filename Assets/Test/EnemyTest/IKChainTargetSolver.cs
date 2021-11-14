using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKChainTargetSolver : MonoBehaviour
{
    private float IKWeight = 0.0f;
    [SerializeField]
    private float duration = 1.0f;

    private ChainIKConstraint hand = null;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<ChainIKConstraint>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ToggleIKWeight(bool IKButton)
    {
        if(IKButton)
        {
            StartCoroutine(IKWeighting(1));
        }
        else
        {
            StartCoroutine(IKWeighting(0));
        }
    }

    IEnumerator IKWeighting(float target)
    {
        float timer = 0;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            if(target < 0.5)
            {
                hand.weight -= Time.deltaTime;
                yield return null;
            }
            else
            {
                hand.weight += Time.deltaTime;
                yield return null;
            }
        }
        yield break;
    }
}

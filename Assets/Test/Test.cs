using UnityEngine;
using UnityEngine.Rendering;

public class Test : MonoBehaviour
{
    [SerializeField]
    private RenderPipelineAsset asset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        asset = GraphicsSettings.currentRenderPipeline;    
    }
}

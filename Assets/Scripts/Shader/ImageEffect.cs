using UnityEngine;

namespace Shader
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class ImageEffect : MonoBehaviour
    {
        [SerializeField] private bool enabled;
        [SerializeField] private Material effectMaterial;
    
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (effectMaterial)
            {
                Graphics.Blit(source,destination,effectMaterial);
            }
            else
            {
                Graphics.Blit(source,destination);
            }
        
        }
    }

}


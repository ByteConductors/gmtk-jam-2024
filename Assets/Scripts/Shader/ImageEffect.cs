using System;
using UnityEngine;
using ColorPalette = Color_Palettes.ColorPalette;

namespace Shader
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class ImageEffect : MonoBehaviour
    {
        [SerializeField] private bool enabled;
        [SerializeField] private Material effectMaterial;

        [SerializeField] private ColorPalette _palette;

        private void Awake()
        {
            effectMaterial.SetTexture(1,_palette.paletteTexture);
        }

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


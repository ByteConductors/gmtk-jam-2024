using UnityEngine;

namespace Color_Palettes
{
    [CreateAssetMenu(fileName = "Color Palette")]
    public class ColorPalette : ScriptableObject
    {
        public Color[] colors = new []
        {
            Color.gray,
            Color.red,
            Color.green,
            Color.blue, 
            Color.magenta
        };

        public Texture paletteTexture;
    }
}


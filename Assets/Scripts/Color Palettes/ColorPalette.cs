using UnityEngine;

namespace Color_Palettes
{
    [CreateAssetMenu(fileName = "Color Palette")]
    public class ColorPalette : ScriptableObject
    {
        public Color[] colors = 
        {
            Color.gray,
            Color.red,
            Color.green,
            Color.blue, 
            Color.magenta
        };

        public Texture paletteTexture;

        public Sprite[] shapeSprites;
        public Sprite[] blockShapeSprites;
    }
}


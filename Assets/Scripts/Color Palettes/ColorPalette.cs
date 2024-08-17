using UnityEngine;
using UnityEngine.Serialization;

namespace Color_Palettes
{
    [CreateAssetMenu(fileName = "Color Palette")]
    public class ColorPalette : ScriptableObject
    {
        public Color[] displayColors = 
        {
            Color.gray,
            Color.red,
            Color.green,
            Color.blue, 
            Color.magenta
        };

        public Color[] internalColors =
        {
            Color.gray,
            Color.red,
            new (255, 255, 0, 0),
            new (0, 255, 255, 0),
            new (255, 0, 255, 0),
        };

        public Texture paletteTexture;

        public Sprite[] shapeSprites;
        public Sprite[] blockShapeSprites;
    }
}


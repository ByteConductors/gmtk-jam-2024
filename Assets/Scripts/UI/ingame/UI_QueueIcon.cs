using UnityEngine;
using Image = UnityEngine.UI.Image;
using ColorPalette = Color_Palettes.ColorPalette;

public class UI_QueueIcon : MonoBehaviour
{
    public GameObject icon;
    private Image image;
    
    public void setIconColor(Color color)
    {
        
        Debug.Log("setIconColorr");
        Debug.Log(transform.GetChild(0).name);
        image = icon.GetComponent<Image>();
        Debug.Log(image);
        image.color = color;
        Debug.Log(image.color);
    }

    void Start()
    {
        transform.SetAsFirstSibling();
    }
}

using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI.ingame
{
    public class UIQueueIcon : MonoBehaviour
    {
        public GameObject icon;
        private Image _image;
    
        public void SetIconColor(Color color)
        {
            _image = icon.GetComponent<Image>();
            _image.color = color;
        }

        void Start()
        {
            transform.SetAsFirstSibling();
        }
    }
}

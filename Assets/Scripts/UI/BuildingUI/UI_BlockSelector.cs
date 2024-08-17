using System;
using Build_System;
using Color_Palettes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.BuildingUI
{
    public class UI_BlockSelector : MonoBehaviour
    {
        private static UI_BlockSelector _instance;
        public static UI_BlockSelector Instance => _instance;
        
        [SerializeField] public UI_BlockIcon[] icons = new UI_BlockIcon[5];
        [SerializeField] public ColorPalette colorPalette;
        [SerializeField] public Sprite[] shapeSprites;

        [SerializeField] private BlockQueue queue;

        private void Awake()
        {
            _instance = this;
            for(var i = 0; i < icons.Length; i++)
            {
                var i1 = i;
                icons[i1].Id = i1;
                icons[i1].OnClick.AddListener(() =>
                {
                    foreach (var listIcon in icons) listIcon.SetSelected(false);
                    icons[i1].SetSelected(true);
                    queue.SelectedBlock = i1;
                });
            }
            
            queue.OnBlockGenerate.AddListener((i,b) => icons[i].SetIcon(b));
        }
    }
}

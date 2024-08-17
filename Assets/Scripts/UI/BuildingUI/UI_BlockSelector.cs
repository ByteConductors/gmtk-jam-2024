using System;
using System.Collections.Generic;
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
        public GameObject blockSelector;
        
        [SerializeField] public List<UI_BlockIcon> icons = new();
        [SerializeField] public ColorPalette colorPalette;
        [SerializeField] public Sprite[] shapeSprites;

        [SerializeField] private BlockQueue queue;

        private void Awake()
        {
            _instance = this;
            queue.OnBlockGenerate.AddListener(b =>
            {
                var icon = Instantiate(blockSelector, transform).GetComponent<UI_BlockIcon>();
                icon.SetBlock(b);
                icon.OnClick.AddListener(() => queue.SelectedBlock = b);
                icon.OnClick.AddListener(() =>
                {
                    for (int i = 0; i < icons.Count; i++)
                    {
                        icons[i].SetSelected(false);
                    }

                    icon.SetSelected(true);
                });
                icons.Add(icon);
            });
            
            queue.OnBlockDestroy.AddListener(b =>
            {
                Destroy(icons.Find(i => i.Block == b).gameObject);
                
            });
        }
    }
}

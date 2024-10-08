using System;
using Build_System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.BuildingUI
{
    public class UI_BlockIcon : MonoBehaviour
    {
        [SerializeField] private Image blockIcon;
        [SerializeField] private Image shapeIcon;
        [SerializeField] private Vector3 targetPosition;
        
        [SerializeField] private Button button;
        public BlockQueue.GeneratedBlock Block { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log("Click Interaction");
                OnClick.Invoke();
            });
        }

        public void SetBlock(BlockQueue.GeneratedBlock block)
        {
            blockIcon.color = UI_BlockSelector.Instance.colorPalette.displayColors[(int)block.WorkerColor + 1];
            shapeIcon.sprite = UI_BlockSelector.Instance.colorPalette.blockShapeSprites[(int)block.Shape];
            Block = block;
        }

        public void SetSelected(bool selected)
        {
            targetPosition.y = selected ? 24 : 0;
        }
        
        public UnityEvent OnClick = new ();

        private void Update()
        {
            blockIcon.transform.localPosition = 
                Vector3.Lerp(blockIcon.transform.localPosition, targetPosition, Time.deltaTime * 5);
        }
    }
}

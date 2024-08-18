using UnityEngine;
using UnityEngine.UI;

namespace Workers
{
    public class DifficultySlider : MonoBehaviour
    {
        public Slider slider;

        private void Awake()
        {
            slider.onValueChanged.AddListener(value =>
            {
                WorkerManager.Instance.SetDifficulty((int)value);
            });
        }

        private void OnEnable()
        {
            slider.value = WorkerManager.Instance.GetDifficulty();
        }
    }
}

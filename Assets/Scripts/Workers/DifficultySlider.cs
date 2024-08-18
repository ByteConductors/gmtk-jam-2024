using UnityEngine;
using UnityEngine.UI;

namespace Workers
{
    public class DifficultySlider : MonoBehaviour
    {
        public Slider slider;

        private readonly int[] _difficulties =
        {
            6, //Beginner
            3, //Advanced
            2  //Masochist
        };
        
        private string DIFFICULTY = "Difficulty";
        
        private void Awake()
        {
            slider.onValueChanged.AddListener(value =>
            {
                SetDifficulty((int)value);
            });
            
            WorkerManager.Instance?.SetDifficulty(_difficulties[GetDifficulty()]);
        }

        private void OnEnable()
        {
            slider.value = GetDifficulty();
        }
        
        public void SetDifficulty(int difficulty)
        {
            PlayerPrefs.SetInt(DIFFICULTY, difficulty);
        }

        public int GetDifficulty()
        {
            return PlayerPrefs.GetInt(DIFFICULTY, 0);
        }
    }
}

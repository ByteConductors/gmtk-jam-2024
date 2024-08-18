using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.ingame
{
    public class UIStateManager : MonoBehaviour
    {

        [SerializeField] public GameObject settings;
    
        void UI_GameOver(string reason)
        {
            Debug.Log("Game Over");
            PlayerPrefs.SetString("GameOver", reason);
            SceneManager.LoadScene("GameOverOverlay", LoadSceneMode.Additive);
            GameManager.Instance.GameOver.RemoveListener(UI_GameOver);  
        }
    
        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.GameOver.AddListener(UI_GameOver);
            GameManager.Instance.GamePause.AddListener(UI_GamePause);
            
            settings.SetActive(false);
        }

        public void UI_GamePause(Boolean isPaused)
        {
            Debug.Log("Game Pause - State Manager");
            settings.SetActive(isPaused);
        }

    }
}

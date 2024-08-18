using TMPro;
using UnityEngine;


namespace UI.GameOver
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        private void Start()
        {
            text.text = PlayerPrefs.GetString("GameOver", "You lost");
        }

        public void PlayButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }

        public void MainMenuButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    
    }
}

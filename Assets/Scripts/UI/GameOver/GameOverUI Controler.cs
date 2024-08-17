using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.GameOver
{
    public class GameOverUI : MonoBehaviour
    {

        public void PlayButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CameraExample");
        }

        public void MainMenuButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    
    }
}

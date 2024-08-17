using UnityEngine;

namespace UI.MainMenu
{
    public class MenuControler : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject optionsMenu;

        // Start is called before the first frame update
        void Awake()
        {
            MainMenuButton();
        }

        public void PlayButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CameraExample");
        }

        public void OptionsButton()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }

        public void MainMenuButton()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
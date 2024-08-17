using Unity.VisualScripting;
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
            if(PlayerPrefs.GetInt("HAS_PLAYED_BEFORE") == 1) UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            else UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
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
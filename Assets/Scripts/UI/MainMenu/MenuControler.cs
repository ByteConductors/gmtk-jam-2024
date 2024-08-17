using UnityEngine;

public class MenuControler : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CameraExample");
    }

    public void OptionsButton()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
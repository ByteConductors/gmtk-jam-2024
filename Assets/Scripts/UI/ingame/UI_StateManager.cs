using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.ingame
{
    public class UIStateManager : MonoBehaviour
    {
    
        void UI_GameOver()
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOverOverlay", LoadSceneMode.Additive);
            GameManager.Instance.GameOver.RemoveListener(UI_GameOver);  
        }
    
        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.GameOver.AddListener(UI_GameOver);     
        }

    }
}

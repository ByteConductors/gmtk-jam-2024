using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Tutorial : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("HAS_PLAYED_BEFORE") == 1)
                SceneManager.LoadScene("MainMenu");
            else
            {
                PlayerPrefs.SetInt("HAS_PLAYED_BEFORE",1);
                SceneManager.LoadScene("GameScene");
            }
        });
    }
}

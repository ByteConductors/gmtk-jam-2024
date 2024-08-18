using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Tutorial : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Texture2D[] textures;
    [SerializeField] private RawImage image;
    private int frameIndex = 0;
    private int frameCount = 0;

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

        frameCount = textures.Length * 4;
    }

    private void FixedUpdate()
    {
        DoTutorialAnimation();
    }

    void DoTutorialAnimation()
    {
        if (frameIndex >= frameCount)
            frameIndex = 0;
        else frameIndex++;
        image.texture = textures[frameIndex / 4];
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScroeSystem : MonoBehaviour
{
    public int menschScore = 10;
    public int blockFallScore = -20;
    public int startScore = 10;
    public GameObject highScore;
    public GameObject score;
    
    
    private int _highScore;
    private int _score;

    private TextMeshProUGUI _highScoreTextMeshPro;
    private TextMeshProUGUI _scoreTextMeshPro;
    private void Start()
    {
        BuildSystem.BlockFalling += this.BlockFallingScore;
        _highScoreTextMeshPro = highScore.GetComponent<TextMeshProUGUI>();
        _scoreTextMeshPro = score.GetComponent<TextMeshProUGUI>();
        UpdateScore(startScore);
    }

    private void BlockFallingScore()
    {
        UpdateScore(blockFallScore);
    }

    private void UpdateScore(int val)
    {
        _score += val;
        _scoreTextMeshPro.text = _score.ToString();
        if (_highScore < _score)
        {
            _highScore = _score;
            _highScoreTextMeshPro.text = _score.ToString();
        }

        if (_score <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}

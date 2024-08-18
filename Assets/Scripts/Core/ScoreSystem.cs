using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Workers;

public class ScroeSystem : MonoBehaviour
{
    public int workerScore = 10;
    public int blockFallScore = -10;
    public int startScore = 10;
    public GameObject highScore;
    public GameObject score;
    
    private int _highScore;
    private int _score;

    private TextMeshProUGUI _highScoreTextMeshPro;
    private TextMeshProUGUI _scoreTextMeshPro;
    private String HIGHSCORE = "highscore";
    private void Start()
    {
        BuildSystem.Instance.OnBlockFall.AddListener(BlockFallingScore);
        BuildSystem.Instance.OnBlockBuild.AddListener(BlockBuildingScore);
        WorkerManager.Instance.onWorkerQueueRelieved.AddListener(_ => UpdateScore(workerScore));
        _highScoreTextMeshPro = highScore.GetComponent<TextMeshProUGUI>();
        _scoreTextMeshPro = score.GetComponent<TextMeshProUGUI>();
        _highScore = PlayerPrefs.HasKey(HIGHSCORE) ? PlayerPrefs.GetInt(HIGHSCORE) : startScore;
        _highScoreTextMeshPro.text = _highScore.ToString();
        UpdateScore(startScore);
    }

    private void BlockFallingScore(Vector3Int position)
    {
        UpdateScore(blockFallScore * (position.y+1));
    }

    private void BlockBuildingScore(Vector3Int position)
    {
        UpdateScore(position.y * 2);
    }

    private void UpdateScore(int val)
    {
        _score += val;
        _scoreTextMeshPro.text = _score.ToString();
        if (_highScore < _score)
        {
            _highScore = _score;
            _highScoreTextMeshPro.text = _highScore.ToString();
            PlayerPrefs.SetInt(HIGHSCORE, _highScore);
        }

        if (_score <= 0)
        {
            GameManager.Instance.TriggerGameOver("Too many blocks fell down.");
        }
    }
}

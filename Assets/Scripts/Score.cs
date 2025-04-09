using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    public static Score instance;
    private int score;

    private void Awake()
    {
        instance = this;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void SaveScore(GameData gameData)
    {
        gameData.playerData.score = score;
    }

    public void LoadScore(ref GameData gameData)
    {
        score = gameData.playerData.score;
        scoreText.text = "Score: " + score;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI DeathScoreText;
    public TextMeshProUGUI WinscoreText;
    public void Increase(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        DeathScoreText.text = score.ToString();
        WinscoreText.text = score.ToString();
    }
}
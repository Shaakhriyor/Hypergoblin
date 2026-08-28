using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreO : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public int Taso = 1;
    public int Check = 0;
    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore()
    {
        FixScore();
       

    }
    public void FixScore()
    {
        Check++;
        if (Check == 3)
        {
            score += 10;
            UpdateScoreUI();
            if (score >= 100)
            {
                SceneManager.LoadScene(Taso);
            }
            Check = 0;
        }
        

    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
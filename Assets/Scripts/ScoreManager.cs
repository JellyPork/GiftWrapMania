using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scoreValue;

    [SerializeField] private Text scoreText;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<Text>();
        Time.timeScale = 1f;
    }

    public void addScore(int points)
    {
        scoreValue += points;
    }

    public void updateScore()
    {
        scoreText.text = "Score: " + scoreValue;
    }

    public int getScore()
    {
        return scoreValue;
    }
}
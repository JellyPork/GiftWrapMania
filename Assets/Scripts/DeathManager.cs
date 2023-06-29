using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject deathMenuUI;
    private bool isDead;

    private void Update()
    {
        if (isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }

    public void Setup(int score)
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pointsText.text = score + " POINTS";
        print("Game Over");
        isDead = true;
    }

    public void RestartButton()
    {
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        isDead = false;
        SceneManager.LoadScene(0);
    }
}
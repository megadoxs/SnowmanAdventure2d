using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
            Time.timeScale = 1;
    }

    public void RestartGame()
    {
        GameManager.instance.SaveScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private GameObject buttons;
    [SerializeField]
    private GameObject highScoreMenu;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField] 
    private GameObject highScorePrefab;
    [SerializeField] 
    private Transform highScoreList;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !buttons.activeSelf)
        {
            buttons.SetActive(true);
            highScoreMenu.SetActive(false);
        }
    }
    
    public void NewGame()
    {
        GameManager.instance.SaveUserName(inputField.text);
        AsyncOperation load = SceneManager.LoadSceneAsync(1);
        load.completed += operation =>
        {
            GameManager.instance.NewGame();
        };
    }

    public void SavedGame()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1);
        load.completed += operation =>
        {
            GameManager.instance.LoadGame();
        };
    }

    public void HighScores()
    {
        buttons.SetActive(!buttons.activeSelf);
        highScoreMenu.SetActive(!highScoreMenu.activeSelf);

        foreach (var highScore in FileDataHandler.instance.GetHighScores())
        {
            var instance = Instantiate(highScorePrefab, highScoreList);
            instance.GetComponent<TMP_Text>().text = highScore.key + " - " + highScore.value;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

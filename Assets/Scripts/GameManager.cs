using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
      [SerializeField]
      private GameObject SnowBallPrefab;

      public static GameManager instance { get; private set; }

      private GameData gameData;
      private string userName;
      
      private void Awake()
      {
            instance = this;
            DontDestroyOnLoad(this);
      }
      
      public void Start()
      {
            gameData = FileDataHandler.instance.LoadGame();
      }

      private void OnApplicationQuit()
      {
            SceneManager.GetActiveScene().GetRootGameObjects();
            
            if(SceneManager.GetActiveScene().buildIndex == 0)
                  return;
            
            SaveScore();
            if (gameData.playerData.score != 0)
            {
                  FileDataHandler.instance.SaveScore(gameData.playerData.userName, gameData.playerData.score);
                  FileDataHandler.instance.DeleteGameSave();
            }
      }

      public void SaveScore()
      {
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Score>().First().SaveScore(gameData);
      }

      public void SaveUserName(string userName)
      {
            this.userName = userName;
      }

      public void LoadGame()
      {
            gameData = FileDataHandler.instance.LoadGame();
            if(gameData == null)
                  NewGame();
            else
                  LoadGameData();
      }

      public void NewGame()
      {
            if (userName != "")
                  gameData = new GameData(userName);
            else
                  gameData = new GameData("Anonymous");
            
            LoadGameData();
      }

      private void LoadGameData()
      {
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Score>().First().LoadScore(ref gameData);
      }
}
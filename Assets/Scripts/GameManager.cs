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
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                  FindObjectsByType<Movement>(FindObjectsSortMode.None).First().SavePlayer(gameData);
                  
                  SnowBall snowball = FindObjectsByType<SnowBall>(FindObjectsSortMode.None).FirstOrDefault();
                  if(snowball != null)
                        snowball.SaveSnowBall(gameData);

                  foreach (var balloon in FindObjectsByType<Balloon>(FindObjectsSortMode.None))
                  {
                        //TODO save ballon location here?
                  }

                  for (var i = 0; i < gameData.launcherData.Length; i++) 
                  { 
                        gameData.launcherData[i].cooldown = FindObjectsByType<SnowLauncher>(FindObjectsSortMode.None)[i].cooldown;
                  }
                  
                  FileDataHandler.instance.SaveGame(gameData);
            }
            else if (gameData.playerData.score != 0)
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
            if (userName != null)
                  gameData = new GameData(userName);
            else
                  gameData = new GameData("Anonymous");
            
            LoadGameData();
      }

      private void LoadGameData()
      {
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Movement>().First().LoadPlayer(ref gameData);
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<Score>().First().LoadScore(ref gameData);
            if(gameData.snowBallData.lifeTime != 0)
                  Instantiate(SnowBallPrefab).GetComponent<SnowBall>().LoadSnowBall(ref gameData);
            
            foreach (var balloonData in gameData.balloonData)
            {
                  //TODO load ballons
            }
            
            for (var i = 0; i < gameData.launcherData.Length; i++) 
            {
                  FindObjectsByType<SnowLauncher>(FindObjectsSortMode.None)[i].cooldown =  gameData.launcherData[i].cooldown;
            }
      }
}
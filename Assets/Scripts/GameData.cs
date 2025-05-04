using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class GameData
{
    public PlayerData playerData;

    public GameData(string userName)
    {
        playerData = new PlayerData(userName);
    }
}

[Serializable]
public class PlayerData
{
    public string userName;
    public int score;

    public PlayerData(string userName)
    {
        this.userName = userName;
        score = 0;
    }
}
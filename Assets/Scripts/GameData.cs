using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class GameData
{
    public PlayerData playerData;
    public SnowBallData snowBallData;
    public BalloonData[] balloonData;
    public LauncherData[] launcherData;

    public GameData(string userName)
    {
        playerData = new PlayerData(userName);
        snowBallData = new SnowBallData();
        balloonData = new BalloonData[4];
        balloonData[0] = new BalloonData(BalloonPosition.HORIZONTAL, new Vector3(0, 3.75f), new Vector3(180, 0, 0));
        balloonData[1] = new BalloonData(BalloonPosition.VERTICAL, new Vector3(8, 0), new Vector3(0, 0, 30));
        balloonData[2] = new BalloonData(BalloonPosition.HORIZONTAL, new Vector3(0, -3.85f), Vector3.zero);
        balloonData[3] = new BalloonData(BalloonPosition.VERTICAL, new Vector3(-8.6f, 0), new Vector3(0, 0, -30));
        launcherData = new LauncherData[2];
        launcherData[0] = new LauncherData(0);
        launcherData[1] = new LauncherData(1);
    }
}

[Serializable]
public class PlayerData
{
    public string userName;
    public int score;
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 velocity;

    public PlayerData(string userName)
    {
        this.userName = userName;
        score = 0;
        position = Vector3.zero;
        rotation = Quaternion.identity;
        velocity = Vector2.zero;
    }
}

[Serializable]
public class SnowBallData
{
    public Vector3 position;
    public Vector3 velocity;
    public float lifeTime;

    public SnowBallData()
    {
        position = Vector3.zero;
        velocity = Vector3.zero;
        lifeTime = 0;
    }
}

[Serializable]
public class BalloonData
{
    public Vector3 position;
    [FormerlySerializedAs("balloon")] public BalloonPosition balloonPosition;
    public Vector3 rotation;

    public BalloonData(BalloonPosition balloonPosition, Vector3 position, Vector3 rotation)
    {
        this.balloonPosition = balloonPosition;
        this.position = position;
        this.rotation = rotation;
    }
}

[Serializable]
public class LauncherData
{
    public float cooldown;

    public LauncherData(float index)
    {
        if (index == 0)
            cooldown = 0;
        else
            cooldown = 20;
    }
}
using UnityEngine;

public class BlackBalloon : Balloon
{
    protected override void OnBallonCollision()
    {
        Score.instance.RemoveScore();
    }
}

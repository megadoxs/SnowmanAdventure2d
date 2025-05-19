using UnityEngine;

public class YellowBalloon : Balloon
{
    protected override void OnBallonCollision()
    {
        Score.instance.AddScore();
    }
}

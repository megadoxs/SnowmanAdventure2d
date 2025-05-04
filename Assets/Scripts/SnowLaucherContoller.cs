using System;
using System.Collections.Generic;
using UnityEngine;

public class SnowLaucherContoller : MonoBehaviour
{
    [SerializeField] private List<SnowLauncher> snowLaunchers;

    private float cooldown;
    private int currentLauncherIndex;

    private void Update()
    {
        if (cooldown > 0)
            cooldown = Math.Max(cooldown - Time.deltaTime, 0);
        else
        {
            cooldown = UnityEngine.Random.Range(1f, 5f);
            snowLaunchers[currentLauncherIndex].Shot();
            
            if(currentLauncherIndex == snowLaunchers.Count - 1)
                currentLauncherIndex = 0;
            else
                currentLauncherIndex++;
        }
    }
}

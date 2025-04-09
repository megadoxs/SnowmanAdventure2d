using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBalloons : MonoBehaviour
{
    [SerializeField]
    private Transform TopWall;
    [SerializeField]
    private Transform RightWall;
    
    private Bounds[] bounds = new Bounds[2];
    
    public static SpawnBalloons instance;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        bounds[0] = SetBounds(TopWall);
        bounds[1] = SetBounds(RightWall);
    }

    private Bounds SetBounds(Transform transform)
    {
        var bounds = new Bounds(transform.GetChild(0).position, Vector3.zero);
        foreach (Transform child in transform)
        {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        
        return bounds;
    }
    
    public void SpawnBalloon(GameObject balloon)
    {
        StartCoroutine(SpawnBalloonAfterDelay(balloon));
    }

    private IEnumerator SpawnBalloonAfterDelay(GameObject balloon)
    {
        yield return new WaitForSeconds(2);

        if (balloon.GetComponent<Balloon>().balloonPosition == BalloonPosition.HORIZONTAL)
            balloon.transform.position = new Vector3(Random.Range(bounds[0].min.x, bounds[0].max.x), balloon.transform.position.y);
        else if (balloon.GetComponent<Balloon>().balloonPosition == BalloonPosition.VERTICAL)
            balloon.transform.position = new Vector3(balloon.transform.position.x, Random.Range(bounds[1].min.y, bounds[1].max.y));
        
        balloon.SetActive(true);
    }
}

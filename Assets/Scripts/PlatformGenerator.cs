using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject ballon;
    
    [SerializeField] 
    private Tilemap ground;

    [SerializeField] 
    private Tilemap water;
    
    [SerializeField] 
    private List<TileGroup> platforms;

    [SerializeField] 
    private List<Bounds> platformsBounds;

    private Vector3Int position;

    private int platformOn;

    private Transform groundCheck;

    private void Awake()
    {
        position = new Vector3Int(25, 0, 0);
        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    private void Update()
    {
        if (groundCheck == null)
            return;
        
        for (int i = 0; i < platformsBounds.Count; i++)
        {
            if (Vector3.Distance(groundCheck.position, platformsBounds[i].ClosestPoint(groundCheck.position)) < 0.1f)
            {
                platformOn = i;
                break;
            }
        }
        
        if(platformsBounds.Count - platformOn < 4)
            GeneratePlatform();
    }

    private void GeneratePlatform()
    { 
        var platform = platforms[UnityEngine.Random.Range(0, 4)];
        
        var width = platform.tileRows[0].tiles.Length;
        var height = platform.tileRows.Length + (platform.waterRows != null ? platform.waterRows.Length : 0) + (platform.lastRows != null ? platform.lastRows.Length : 0);
        for (int y = 0; y < height; y++)
        {
            TileBase[] row;
            Tilemap map;
            if (y < platform.tileRows.Length)
            {
                row = platform.tileRows[y].tiles;
                map = ground;
            }
            else if (y < platform.tileRows.Length + platform.waterRows.Length)
            {
                row = platform.waterRows[y - platform.tileRows.Length].tiles;
                map = water;
            }
            else
            {
                row = platform.lastRows[y - (platform.tileRows.Length + platform.waterRows.Length)].tiles;
                map = ground;
            }
            
            
            for (int x = 0; x < row.Length; x++)
            {
                TileBase tile = row[x];
                if (tile != null)
                {
                    Vector3Int tilePos = position + new Vector3Int(x, -y, 0);
                    map.SetTile(tilePos, tile);
                }
            }
        }
        
        SpriteRenderer balloonRenderer = ballon.GetComponent<SpriteRenderer>();
        if (ballon != null)
        {
            Vector3Int balloonCell = position + new Vector3Int(width/2, 1, 0);
            Vector3 worldPos = ground.CellToWorld(balloonCell) + ground.tileAnchor;
            worldPos.y += balloonRenderer.bounds.size.y / 2;
            worldPos.x -= balloonRenderer.bounds.size.x / 2;

            Instantiate(ballon, worldPos, Quaternion.identity);
        }
        
        Vector3 minWorld = ground.CellToWorld(position) + new Vector3(0, ground.cellSize.y, 0);
        Vector3 maxWorld = ground.CellToWorld(position + new Vector3Int(platform.tileRows[0].tiles.Length, -height + 1, 0));

        Vector3 center = (minWorld + maxWorld) / 2f;
        Vector3 size = maxWorld - minWorld;

        Bounds platformBound = new Bounds(center, size);
        platformsBounds.Add(platformBound);
        
        ground.CompressBounds();
        ground.RefreshAllTiles();

        var collider = ground.GetComponent<TilemapCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
        
        water.CompressBounds();
        water.RefreshAllTiles();

        var collider2 = water.GetComponent<TilemapCollider2D>();
        if (collider2 != null)
        {
            collider2.enabled = false;
            collider2.enabled = true;
        }

        int yOffset;
        if (UnityEngine.Random.Range(0, 4) < 3)
            yOffset = UnityEngine.Random.Range(0, 3);
        else 
            yOffset = UnityEngine.Random.Range(-5, 0);
        var nextY = Mathf.Clamp(position.y + yOffset, -3, 5);
        
        var minX = position.x + width + 2;
        var maxX = minX + 1;
        var nextX = UnityEngine.Random.Range(minX, maxX + 1);
        
        position = new Vector3Int(nextX, nextY, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (platformsBounds == null)
            return;

        foreach (var bounds in platformsBounds)
        {
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}

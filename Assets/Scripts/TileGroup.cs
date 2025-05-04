using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileGroup : ScriptableObject
{
    public TileRow[] tileRows;
    public TileRow[] waterRows;
    public TileRow[] lastRows;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    //Width of road in tiles
    [SerializeField] private int roadWidth = 5;
    private TileMapDrawer tileMapDrawer;
    //Only stores positions of roads instead of tiles. E.g., (0,15) is a road that runs along the y-axis at 15.
    private HashSet<Vector2Int> roadPositions;

    public const int CHUNKSIZE = 100;
    // Start is called before the first frame update
    void Start()
    {
        tileMapDrawer = GetComponent<TileMapDrawer>();
        roadPositions = tileMapDrawer.PaintRoadTiles(new Vector2Int(0, 0), CHUNKSIZE);
    }
}

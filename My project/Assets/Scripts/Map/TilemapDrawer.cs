using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapDrawer : MonoBehaviour
{
    [SerializeField] private Tilemap roadTileMap;
    [SerializeField] private TileBase roadTile;
    [SerializeField] private int roadWidth = 5;
    [SerializeField] private int blockWidth = 15;


    //TODO: Refactor to run asyonchronously if there is perfomance issues
    public HashSet<Vector2Int> PaintRoadTiles(Vector2Int startPosition, int chunksize)
    {
        HashSet<Vector2Int> roadPositions = new HashSet<Vector2Int>();
        //X direction
        for (int i = startPosition.x; i < chunksize; i += blockWidth)
        {
            roadPositions.Add(new Vector2Int(i, 0));
            for (int j = 0; j < chunksize; j++)
            {
                for (int k = 0; k < roadWidth; k++)
                {
                    PaintSingleTile(roadTileMap, roadTile, new Vector2Int(i + k, j));
                }
            }
        }

        //Y direction
        for (int i = startPosition.y; i < chunksize; i += blockWidth)
        {
            roadPositions.Add(new Vector2Int(0, i));
            for (int j = 0; j < chunksize; j++)
            {
                for (int k = 0; k < roadWidth; k++)
                {
                    PaintSingleTile(roadTileMap, roadTile, new Vector2Int(j, i + k));
                }
            }
        }
        return roadPositions;
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }


}

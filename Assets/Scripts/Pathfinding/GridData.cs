using UnityEngine;
using UnityEngine.Tilemaps; 
using System;
using System.Collections.Generic; 

public class GridData : MonoBehaviour
{
    [SerializeField] private Grid grid; 
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private CustomTileData[] customTileData;

    [SerializeField] private Tile debugTile; 
    [SerializeField] private Tile frontierTile; 
    [SerializeField] private Tile pathTile;

    [SerializeField] private Vector3Int startPos; 
    [SerializeField] private Vector3Int endPos; 

    [SerializeField] private Cell[,] gridData;
    [SerializeField, Range(-1, 999)] private int step = -1; 

    public void GenerateGridData()
    {
        Vector3Int _size = tilemap.size;
        gridData = new Cell[_size.x, _size.y];
        Vector3Int _position = Vector3Int.zero;
        TileBase _data;
        for (int y = 0; y < gridData.GetLength(1); y++)
        {
            _position.y = (-_size.y / 2) + y;
            for (int x = 0; x < gridData.GetLength(0); x++)
            {
                _position.x = (-_size.x / 2) + x;
                _data = tilemap.GetTile(_position);
                if(_data is null)
                {
                    gridData[x, y] = new Cell()
                    {
                        IndexX = x,
                        IndexY = y,
                        Position = _position,
                        Cost = int.MaxValue
                    };
                    continue; 
                }
                for (int i = 0; i < customTileData.Length; i++)
                {
                    if (_data == customTileData[i].Tile)
                    {
                        gridData[x, y] = new Cell()
                        {
                            IndexX = x, IndexY = y,
                            Position = _position,
                            Cost = customTileData[i].Cost
                        };
                        break;
                    }
                }
            }
        }
    }

    public void FindPath()
    {
        GenerateGridData();

        Vector3Int _startCellPosition = grid.WorldToCell(startPos); 
        Vector3Int _endCellPosition = grid.WorldToCell(endPos); 

        Cell _start = null;
        Cell _end = null; 
        for (int y = 0; y < gridData.GetLength(1); y++)
        {
            for (int x = 0; x < gridData.GetLength(0); x++)
            { 
                if(_startCellPosition == gridData[x,y].Position)
                    _start = gridData[x,y];
                if(_endCellPosition == gridData[x,y].Position)
                    _end = gridData[x,y];

                if (_start != null && _end != null) break;
            }
        }

        StartCoroutine(Pathfinder.DebugAstar(_start, _end, gridData, tilemap, frontierTile, debugTile, pathTile, step)); 
        //Cell[] _path = Pathfinder.FindBFSPath(_start, _end, gridData, tilemap, frontierTile, debugTile, pathTile, step); 
    }
}

[Serializable]
public class Cell
{
    public int IndexX, IndexY; 
    public Vector3Int Position;
    public int Cost = 1;

    public float GetHeuristicCost(Vector3Int end)
    {
        return Vector3Int.Distance(Position, end) + Cost; 
    }

    public float GetHeuristicCost(Vector3Int _end, Vector3Int _start)
    {
        return Vector3Int.Distance(Position, _end) + Vector3Int.Distance(Position, _start) + Cost;
    }
}
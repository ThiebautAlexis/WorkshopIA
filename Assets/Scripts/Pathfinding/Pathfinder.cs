using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public static class Pathfinder
{
    private static Cell[] BuildPath(Cell _startCell, Cell _endCell, Dictionary<Cell, Cell> _cameFrom, Tilemap _map, Tile _pathTile)
    {
        List<Cell> _path = new List<Cell>{ _endCell};
        Cell _currentCell = null;
        Vector3Int _pos = Vector3Int.zero; 
        while (_currentCell != _startCell)
        {
            _currentCell = _cameFrom[_currentCell]; 
            _path.Add(_currentCell);
            _map.SetTile(_currentCell.Position, _pathTile); 
        }
        _path.Reverse();

        return _path.ToArray(); 
    }


    public static Cell[] FindBFSPath(Cell _startCell, Cell _endCell, Cell[,] _cells, Tilemap _map, Tile _frontierTile, Tile _cameFromTile, Tile _pathTile, int _step = -1)
    {
        Dictionary<Cell, Cell> _cameFrom = new Dictionary<Cell, Cell>();
        List<Cell> _frontier = new List<Cell>{ _startCell };
        Cell _currentCell = null;
        Cell _neighbourCell = null; 
        while (_frontier.Count > 0)
        {
            _currentCell = _frontier[0];
            _map.SetTile(_currentCell.Position, _frontierTile);

            if (_currentCell == _endCell)
            {
                return BuildPath(_startCell, _endCell, _cameFrom, _map, _pathTile); 
            }
            
            if (_currentCell.IndexX > 0 && 
                (_neighbourCell = _cells[_currentCell.IndexX - 1, _currentCell.IndexY]).Cost < int.MaxValue && 
                !_cameFrom.ContainsKey(_neighbourCell) )
            {
                _frontier.Add(_neighbourCell);
                _cameFrom.Add(_neighbourCell, _currentCell); 
            }

            if (_currentCell.IndexX < _cells.GetLength(0) - 1  &&
                (_neighbourCell = _cells[_currentCell.IndexX + 1, _currentCell.IndexY]).Cost < int.MaxValue &&
                !_cameFrom.ContainsKey(_neighbourCell))
            {
                _frontier.Add(_neighbourCell);
                _cameFrom.Add(_neighbourCell, _currentCell);
            }

            if (_currentCell.IndexY > 0 &&
                (_neighbourCell = _cells[_currentCell.IndexX, _currentCell.IndexY - 1]).Cost < int.MaxValue &&
                !_cameFrom.ContainsKey(_neighbourCell))
            {
                _frontier.Add(_neighbourCell);
                _cameFrom.Add(_neighbourCell, _currentCell);
            }

            if (_currentCell.IndexY < _cells.GetLength(1) -1  &&
                (_neighbourCell = _cells[_currentCell.IndexX, _currentCell.IndexY + 1]).Cost < int.MaxValue &&
                !_cameFrom.ContainsKey(_neighbourCell))
            {
                _frontier.Add(_neighbourCell);
                _cameFrom.Add(_neighbourCell, _currentCell);
            }

            _frontier.RemoveAt(0); 
        }

        return null; 
    }
}

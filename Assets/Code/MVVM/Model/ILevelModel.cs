using System;
using UnityEngine;


namespace MVVM
{
    internal interface ILevelModel
    {
        GameObject LevelViewPrefab { get; }
        ViewProperties ViewProperties { get; }

        event Action<Vector2Int, CellState> OnCellStateChanged;

        void AssignViewModel(ILevelViewModel levelViewModel);
        void DisassignViewModel(ILevelViewModel levelViewModel);
        void UpdateCellState(Vector2Int cellIndex, CellState cellState);
    }
}

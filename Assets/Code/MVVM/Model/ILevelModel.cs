using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal interface ILevelModel
    {
        GameObject LevelViewPrefab { get; }
        Dictionary<int, CellState> FieldState { get; }
        int [] FieldSize { get; }

        event Action<int, CellState> OnCellStateChanged;

        void UpdateCellState(int cellIndex, CellState cellState);
    }
}

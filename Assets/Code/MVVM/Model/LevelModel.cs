using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class LevelModel : ILevelModel
    {
        #region Properties

        public GameObject LevelViewPrefab { get; }
        public Dictionary<int, CellState> FieldState { get; }
        public int[] FieldSize { get; }
        public event Action<int, CellState> OnCellStateChanged;

        #endregion


        #region ClassLifeCycles

        internal LevelModel(GameObject levelPrefab, int lines = 10, int columns = 10)
        {
            LevelViewPrefab = levelPrefab;
            FieldSize = new int[2] { lines, columns };
            FieldState = new Dictionary<int, CellState>();
        }

        #endregion


        #region Methods

        public void UpdateCellState(int cellIndex, CellState cellState)
        {
            if (!cellState.Equals(CellState.Changing))
            {
                FieldState[cellIndex] = cellState;
                OnCellStateChanged(cellIndex, cellState);
            }
        }

        #endregion
    }
}

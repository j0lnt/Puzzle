using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class LevelModel : ILevelModel
    {
        #region Properties

        public GameObject LevelViewPrefab { get; }
        public ViewProperties ViewProperties { get; private set; }

        public event Action<Vector2Int, CellState> OnCellStateChanged;

        #endregion


        #region ClassLifeCycles

        internal LevelModel(GameObject levelPrefab, int lines = 10, int columns = 10)
        {
            LevelViewPrefab = levelPrefab;
            ViewProperties = new ViewProperties
            {
                Resolution = new Rect(),
                FieldProperties = new FieldProperties
                {
                    FieldSize = new Vector2Int(lines, columns),
                    FieldMap = new Dictionary<Vector2Int, CellState>()
                },
                Visibility = true,
                Fullness = 0.0f
            };
        }

        #endregion


        #region Methods

        public void UpdateCellState(Vector2Int cellIndex, CellState cellState)
        {
            if (!cellState.Equals(CellState.Changing))
            {
                ViewProperties.FieldProperties.FieldMap[cellIndex] = cellState;
                OnCellStateChanged(cellIndex, cellState);
            }
        }

        public void AssignViewModel(ILevelViewModel levelViewModel)
        {
            levelViewModel.OnResolutionChanged += ChangeResolutionProperties;
        }

        private void ChangeResolutionProperties(Rect resolution)
        {
            ViewProperties = new ViewProperties {
                Resolution = resolution,
                FieldProperties = ViewProperties.FieldProperties,
                Visibility = ViewProperties.Visibility,
                Fullness = ViewProperties.Fullness
            };
        }

        public void DisassignViewModel(ILevelViewModel levelViewModel)
        {
            levelViewModel.OnResolutionChanged -= ChangeResolutionProperties;
        }

        #endregion
    }
}

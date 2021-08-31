using System;
using UnityEngine;
using UnityEngine.UI;


namespace MVVM
{
    internal sealed class LevelView : MonoBehaviour
    {
        #region Fields

        private ILevelViewModel _levelViewModel;

        private Canvas _mainCanvas;

        private Button _menuButton;
        private Button _restartButton;

        readonly Sprite _cellSprite = Resources.Load<Sprite>("cell");
        readonly Sprite _ballSprite = Resources.Load<Sprite>("ball");

        //private CellsAreaHandler _cellsAreaHandler;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var spwndbttn = new GameObject();
            spwndbttn.name = "button";
            spwndbttn.AddComponent<Image>();
            spwndbttn.AddComponent<Button>();
            _menuButton = spwndbttn.GetComponent<Button>();
            _menuButton.image = spwndbttn.GetComponent<Image>();
        }

        #endregion


        #region ClassLifeCycle

        ~LevelView()
        {
            RemoveAllListeners();
        }

        #endregion


        #region Methods

        internal void Initialize(ILevelViewModel levelViewModel)
        {

            _levelViewModel = levelViewModel;
            _mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            
            DrawField(levelViewModel.FieldMap);

            RemoveAllListeners();

            _menuButton.onClick.AddListener(()=> _levelViewModel.MenuButtonHandle());
            _restartButton.onClick.AddListener(()=> _levelViewModel.RestartButtonHandle()); 
        }

        private void RemoveAllListeners()
        {
            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }

        private void DrawField(int[,] fieldMap)
        {
            var linesCount = fieldMap.GetLength(0);
            var columnsCount = fieldMap.GetLength(1);

            var pos = new Vector3(0, 0, 0);

            var cellsParent = new GameObject();
            cellsParent.transform.SetParent(_mainCanvas.transform);
            cellsParent.name = "cells";
            cellsParent.AddComponent<CanvasGroup>();

            var ballsParent = new GameObject();
            ballsParent.transform.SetParent(_mainCanvas.transform);
            ballsParent.name = "balls";
            ballsParent.AddComponent<CanvasGroup>();

            for (int i = 0; i < linesCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    var newCell = new GameObject();
                    newCell.name = $"cell{i}-{j}";
                    newCell.transform.position = pos;
                    newCell.transform.SetParent(cellsParent.transform);
                    newCell.AddComponent<Image>().sprite = _cellSprite;

                    if (fieldMap[i, j] == 1)
                    {
                        var newBall = new GameObject();
                        newBall.name = $"ball{i}-{j}";
                        newBall.transform.position = pos;
                        newBall.transform.SetParent(ballsParent.transform);
                        newBall.AddComponent<Image>().sprite = _ballSprite;
                        //newBall.GetComponent<Image>().sortingOrder = 1;
                    }
                    pos += new Vector3(_cellSprite.bounds.size.x, 0, 0);
                }
                pos += new Vector3(0, -1 * _cellSprite.bounds.size.y, 0);
                pos -= new Vector3(_cellSprite.bounds.size.x * columnsCount, 0, 0);
            }
        }

        #endregion
    }
}

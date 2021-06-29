using System;
using UnityEngine;
using UnityEngine.UI;


namespace MVVM
{
    internal sealed class LevelView : MonoBehaviour
    {
        #region Fields

        private ILevelViewModel _levelViewModel;

        private Button _menuButton;
        private Button _restartButton;

        //private CellsAreaHandler _cellsAreaHandler;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var spwndbttn = new GameObject();
            spwndbttn.AddComponent<Image>();
            spwndbttn.AddComponent<Button>();
            _menuButton = spwndbttn.GetComponent<Button>();
            _menuButton.image = spwndbttn.GetComponent<Image>();
        }

        #endregion


        #region Methods

        internal void Initialize(ILevelViewModel levelViewModel)
        {
            _levelViewModel = levelViewModel;

            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();

            _menuButton.onClick.AddListener(()=> _levelViewModel.MenuButtonHadle());
            _restartButton.onClick.AddListener(()=> _levelViewModel.RestartButtonHandle()); 
        }

        #endregion
    }
}

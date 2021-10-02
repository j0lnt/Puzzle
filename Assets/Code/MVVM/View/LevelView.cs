using System;
using UnityEngine;
using UnityEngine.UI;


namespace MVVM
{
    [RequireComponent(typeof(Canvas))]
    internal sealed class LevelView : MonoBehaviour, ILevelView
    {
        #region Fields

        private ILevelViewModel _levelViewModel;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Sprite _cellSprite;
        [SerializeField] private GridLayoutGroup _playingField;

        private Image[] _cells;

        #endregion


        #region Properties

        public Canvas MainCanvas { get; private set; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            MainCanvas = GetComponent<Canvas>();
        }

        #endregion


        #region ClassLifeCycle

        ~LevelView()
        {
            RemoveAllListeners();
        }

        #endregion


        #region Methods

        public void Initialize(ILevelViewModel levelViewModel, int cellCount, bool isEmpty)
        {

            _levelViewModel = levelViewModel;

            RemoveAllListeners();

            _menuButton.onClick.AddListener(()=> _levelViewModel.MenuButtonHandle());
            _restartButton.onClick.AddListener(()=> _levelViewModel.RestartButtonHandle());

            _cells = new Image[cellCount];
            for (int i = 0; i < cellCount; i++)
            {
                var newCell = new GameObject();
                newCell.transform.SetParent(_playingField.transform);
                var cellImage = newCell.AddComponent<Image>();
                cellImage.sprite = _cellSprite;
                cellImage.color = Color.gray;
                _cells[i] = cellImage;
            }

            MainCanvas.enabled = false;
        }

        private void RemoveAllListeners()
        {
            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

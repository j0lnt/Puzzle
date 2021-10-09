using System.Collections.Generic;
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

        private Dictionary<int, GameObject> _cells;

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
            _levelViewModel.OnCellChange -= ChangeGameObject;
        }

        #endregion


        #region Methods

        public void Initialize(ILevelViewModel levelViewModel, int cellCount, bool isEmpty)
        {

            _levelViewModel = levelViewModel;
            _levelViewModel.OnCellChange += ChangeGameObject;

            RemoveAllListeners();

            _menuButton.onClick.AddListener(()=> _levelViewModel.MenuButtonHandle());
            _restartButton.onClick.AddListener(()=> _levelViewModel.RestartButtonHandle());

            _cells = new Dictionary<int, GameObject>();
            for (int i = 0; i < cellCount; i++)
            {
                var newCell = new GameObject();
                newCell.name = $"cell-{i}";
                newCell.transform.SetParent(_playingField.transform);
                newCell.layer = 8;
                var cellImage = newCell.AddComponent<Image>();
                cellImage.sprite = _cellSprite;
                cellImage.color = Color.gray;
                _cells.Add(newCell.GetInstanceID(), newCell);
            }

            MainCanvas.enabled = false;
        }

        private void ChangeGameObject(int id)
        {
            _cells[id].GetComponent<Image>().color = Color.red;
        }

        private void RemoveAllListeners()
        {
            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

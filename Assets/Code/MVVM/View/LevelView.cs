using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MVVM
{
    [RequireComponent(typeof(Canvas))]
    internal sealed class LevelView : MonoBehaviour, ILevelView
    {
        #region Fields

        public event Action<Rect> SetUpResolution;

        private ILevelViewModel _levelViewModel;
        private Dictionary<int, GameObject> _spawnedObjects;
        private Dictionary<Vector2Int, int> _dotsPull;
        private Dictionary<Vector2Int, int> _cellsPull;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Sprite _cellSprite;
        [SerializeField] private GridLayoutGroup _playingField;

        #endregion


        #region Properties

        public ViewProperties CurrentViewProperties { get; private set; }
        private Canvas MainCanvas { get; set; }

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
            _levelViewModel.UpdateView -= UpdatePlayingField;
            _levelViewModel.OnBallSpawned -= SpawnDot;
            _levelViewModel.DisassignView(this);
        }

        #endregion


        #region Methods

        public void Initialize(ILevelViewModel levelViewModel, ViewProperties viewProperties)
        {
            CurrentViewProperties = viewProperties;

            _levelViewModel = levelViewModel;
            _levelViewModel.OnCellChange += ChangeGameObject;
            _levelViewModel.UpdateView += UpdatePlayingField;
            _levelViewModel.OnBallSpawned += SpawnDot;
            SetUpResolution.Invoke(GetComponent<RectTransform>().rect);

            RemoveAllListeners();

            _menuButton.onClick.AddListener(()=> _levelViewModel.MenuButtonHandle());
            _restartButton.onClick.AddListener(()=> _levelViewModel.RestartButtonHandle());

            _spawnedObjects = new Dictionary<int, GameObject>();
            _cellsPull = new Dictionary<Vector2Int, int>();
            _dotsPull = new Dictionary<Vector2Int, int>();
            for (int xPos = 0; xPos < CurrentViewProperties.FieldProperties.FieldSize.x; xPos++)
            {
                for (int yPos = 0; yPos < CurrentViewProperties.FieldProperties.FieldSize.y; yPos++)
                {
                    var currentIndex = new Vector2Int(xPos, yPos);
                    var newCell = new GameObject();
                    newCell.name = $"[{xPos},{yPos}]cell";
                    newCell.transform.SetParent(_playingField.transform);
                    newCell.layer = 8;
                    var cellImage = newCell.AddComponent<Image>();
                    cellImage.sprite = _cellSprite;
                    //cellImage.color = Color.green;
                    _spawnedObjects.Add(newCell.GetInstanceID(), newCell);
                    _cellsPull.Add(currentIndex, newCell.GetInstanceID());
                }
            }
            MainCanvas.enabled = CurrentViewProperties.Visibility;
        }

        private void SpawnDot(Vector2Int index, GameObject prefab, Color color)
        {
            var spawnedDot = GameObject.Instantiate(prefab);
            spawnedDot.name = $"[{index.x},{index.y}]ball";
            spawnedDot.transform.SetParent(MainCanvas.transform);
            spawnedDot.transform.position = _spawnedObjects[_cellsPull[index]].transform.position;
            var dotImage = spawnedDot.GetComponent<Image>();
            dotImage.color = color;
            _spawnedObjects.Add(spawnedDot.GetInstanceID(), spawnedDot);
            _dotsPull.Add(index, spawnedDot.GetInstanceID());
        }

        private void UpdatePlayingField(Rect rect)
        {
            var cellSide = rect.width / 10.0f;
            _playingField.cellSize = new Vector2(cellSide, cellSide);
            Debug.Log($"New cell size = {cellSide}");
        }

        private void ChangeGameObject(int id)
        {
            _spawnedObjects[id].GetComponent<Image>().color = Color.red;
        }

        private void RemoveAllListeners()
        {
            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

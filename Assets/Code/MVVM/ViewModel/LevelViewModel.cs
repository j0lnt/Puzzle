using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class LevelViewModel : ILevelViewModel
    {
        #region Fields

        private static System.Random _random = new System.Random();

        #endregion


        #region Properties

        public ILevelModel LevelModel { get; }
        public IInputViewModel UIInput { get; }
        public IDotsViewModel DotsViewModel { get; private set; }
        public event Action<int> OnCellChange;
        public event Action<Rect> OnResolutionChanged;
        public event Action<Rect> UpdateView;
        public event Action<Vector2Int, GameObject, Color> OnBallSpawned;

        private Dictionary<Vector2Int, int> DotsMap { get; set; } 

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, IInputViewModel input, IDotsModel dotsModel)
        {
            LevelModel = model;
            LevelModel.AssignViewModel(this);
            //OnResolutionChanged.Invoke
            LevelModel.OnCellStateChanged += CellStateUpdated;

            UIInput = input;
            InitializeInput(UIInput);

            InstantiateView(this);

            DotsViewModel = new DotsViewModel(dotsModel, this);
            InitializeDots(DotsViewModel);
        }

        ~LevelViewModel()
        {
            UIInput.OnUITouchBegan -= UIInputHandler;
            UIInput.OnPlayingFieldTouchBegan -= PlayingFieldInputHandler;
            LevelModel.OnCellStateChanged -= CellStateUpdated;
            LevelModel.DisassignViewModel(this);
        }

        #endregion


        #region Methods

        public void InstantiateView(ILevelViewModel levelViewModel)
        {
            
            var go = GameObject.Instantiate(levelViewModel.LevelModel.LevelViewPrefab);
            var view = go.GetComponent<ILevelView>();
            AssignView(view);
            view.Initialize(levelViewModel, LevelModel.ViewProperties);
        }

        public void AssignView(ILevelView levelView)
        {
            levelView.SetUpResolution += ChangeResolution;
        }

        private void ChangeResolution(Rect resolution)
        {
            OnResolutionChanged.Invoke(resolution);
            UpdateView.Invoke(resolution);
        }

        public void DisassignView(ILevelView levelView)
        {
            levelView.SetUpResolution -= ChangeResolution;
        }

        public void InitializeInput(IInputViewModel input)
        {
            input.OnUITouchBegan += UIInputHandler;
            input.OnPlayingFieldTouchBegan += PlayingFieldInputHandler;
        }

        private void UIInputHandler(int gameObjectInstanceID)
        {
            Debug.Log($"Touch UI with ID:{gameObjectInstanceID}");
        }

        private void PlayingFieldInputHandler(int gameObjectInstanceID)
        {
            OnCellChange.Invoke(gameObjectInstanceID);
        }

        public void InitializeDots(IDotsViewModel dotsViewModel)
        {
            DotsViewModel = dotsViewModel;
            GenerateDotsMap(LevelModel.ViewProperties);

            for (int xPos = 0; xPos < LevelModel.ViewProperties.FieldProperties.FieldSize.x; xPos++)
            {
                for (int yPos = 0; yPos < LevelModel.ViewProperties.FieldProperties.FieldSize.y; yPos++)
                {
                    var currentPos = new Vector2Int(xPos, yPos);
                    if (DotsMap[currentPos] == 1)
                    {
                        OnBallSpawned.Invoke(currentPos, dotsViewModel.DotsModel.DotsProperties.DotPrefab, Color.red); // debug test
                        //var go = GameObject.Instantiate(dotsViewModel.DotsModel.DotsProperties.DotPrefab);
                        //go.name = $"[{xPos},{yPos}]ball";
                        //go.transform.position = 
                    }
                }
            }
        }

        private void GenerateDotsMap(ViewProperties viewProperties)
        {
            DotsMap = new Dictionary<Vector2Int, int>();
            var fieldCompletness = 0 / (viewProperties.FieldProperties.FieldMap.Count);
            for (int xPos = 0; xPos < viewProperties.FieldProperties.FieldSize.x; xPos++)
            {
                for (int yPos = 0; yPos < viewProperties.FieldProperties.FieldSize.y; yPos++)
                {
                    var currentPos = new Vector2Int(xPos, yPos);
                    if (viewProperties.FieldProperties.FieldMap[currentPos].Equals(CellState.Empty))
                    {
                        if (_random.Next()%2 > 0)
                        {
                            DotsMap[currentPos] = 1;
                            fieldCompletness += 1 / viewProperties.FieldProperties.FieldMap.Count;
                        }
                        else
                        {
                            DotsMap[currentPos] = 0;
                        }
                    }
                }
            }
        }

        private void CellStateUpdated(Vector2Int index, CellState state)
        {

        }

        public void RestartButtonHandle()
        {
            #if !UNITY_EDITOR && UNITY_ANDROID
            ShowAndroidToastMessage("Restart button not implemented");
            #endif
            throw new System.NotImplementedException("Restart button not implemented");
        }

        public void MenuButtonHandle()
        {
            #if !UNITY_EDITOR && UNITY_ANDROID
            ShowAndroidToastMessage("Menu button not implemented");
            #endif
            throw new System.NotImplementedException("Menu button not implemented");
        }

        #if !UNITY_EDITOR && UNITY_ANDROID
        private void ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
        #endif

        #endregion
    }
}

using System;
using UnityEngine;


namespace MVVM
{
    internal sealed class LevelViewModel : ILevelViewModel
    {
        #region Fields

        private System.Random _random = new System.Random();

        private RaycastHit _raycastHit;
        private Vector3 _rayCastPosition;

        #endregion


        #region Properties

        public ILevelModel LevelModel { get; }
        public IInputViewModel UIInput { get; }
        public IDotsViewModel DotsViewModel { get; private set; }
        public bool IsEmpty { get; }
        public event Action<int> OnCellChange;

        private int[] DotsMap { get; set; } 

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, IInputViewModel input, bool isEmpty = true)
        {
            _rayCastPosition = Vector3.forward;
            _rayCastPosition.z = Camera.main.farClipPlane;

            LevelModel = model;
            LevelModel.OnCellStateChanged += CellStateUpdated;

            IsEmpty = isEmpty;
            UIInput = input;
            InitializeInput(UIInput);

            var cellCount = LevelModel.FieldSize[0] * LevelModel.FieldSize[1];

            InstantiateView(this, cellCount, IsEmpty);
        }

        ~LevelViewModel()
        {
            UIInput.OnUITouchBegan -= UIInputHandler;
            UIInput.OnPlayingFieldTouchBegan -= PlayingFieldInputHandler;
            LevelModel.OnCellStateChanged -= CellStateUpdated;
        }

        #endregion


        #region Methods

        public void InstantiateView(ILevelViewModel levelViewModel, int cellCount, bool isEmpty)
        {            
            var go = GameObject.Instantiate(levelViewModel.LevelModel.LevelViewPrefab);
            var view = go.GetComponent<ILevelView>();
            view.Initialize(levelViewModel, cellCount, isEmpty);
            view.MainCanvas.enabled = true;
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
            GenerateDotsMap(DotsViewModel.DotsModel.DotsProperties.DefaultIntDotsCount); // вынести в отдельное свойство! LevelViewModel не доллжен знать о DotsModel, так же как и DotsViewModel
            for (int i = 0; i < DotsMap.Length; i++)
            {
                var go = GameObject.Instantiate(dotsViewModel.DotsModel.DotsProperties.DotPrefab);
                
            }
        }

        private void GenerateDotsMap(int dotsCount)
        {
            DotsMap = new int[LevelModel.FieldSize[0]]; // вынести FieldSize в отдельное свойство, для избежания NRE
            for (int i = 0; i < DotsMap.Length; i++)
            {
                if (dotsCount > 0)
                {
                    DotsMap[i] = _random.Next(0, 1);
                }
            }
        }

        private void CellStateUpdated(int index, CellState state)
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

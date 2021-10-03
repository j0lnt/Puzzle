using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class LevelViewModel : ILevelViewModel
    {
        #region Fields

        private System.Random _random = new System.Random();

        #endregion


        #region Properties

        public ILevelModel LevelModel { get; }
        public IInputProxy PlayerInput { get; }
        public List<ILevelView> Views { get; }
        public int[,] FieldMap { get; } 
        public bool IsEmpty { get; }

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, IInputProxy input, bool isEmpty = true)
        {
            Views = new List<ILevelView>();

            LevelModel = model;
            IsEmpty = isEmpty;
            PlayerInput = input;
            InitializeInput(PlayerInput);

            var cellCount = LevelModel.FieldSize[0] * LevelModel.FieldSize[1];

            InstantiateView(this, cellCount, IsEmpty);
        }

        ~LevelViewModel()
        {
            PlayerInput.TouchOnComplete -= TouchHandler;
        }

        #endregion


        #region Methods

        public void InstantiateView(ILevelViewModel levelViewModel, int cellCount, bool isEmpty)
        {
            
            var go = UnityEngine.GameObject.Instantiate(levelViewModel.LevelModel.LevelViewPrefab);
            var view = go.GetComponent<ILevelView>();
            view.Initialize(levelViewModel, cellCount, isEmpty);
            view.MainCanvas.enabled = true;

            Views.Add(view);
        }

        public void InitializeInput(IInputProxy input)
        {
            input.TouchOnComplete += TouchHandler;
        }

        public void RestartButtonHandle()
        {
            ShowAndroidToastMessage("Restart button not implemented");
            throw new System.NotImplementedException("Restart button not implemented");
        }

        public void MenuButtonHandle()
        {
            ShowAndroidToastMessage("Menu button not implemented");
            throw new System.NotImplementedException("Menu button not implemented");
        }

        private void TouchHandler(Touch[] touches)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                touchPosition.z = 0f;
                Debug.DrawRay(Camera.main.transform.position, touchPosition);
            }
        }

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

        #endregion
    }
}

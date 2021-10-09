using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
        public IInputProxy PlayerInput { get; }
        public List<ILevelView> Views { get; }
        public int[,] FieldMap { get; } 
        public bool IsEmpty { get; }

        #endregion


        #region ClassLifeCycles

        internal LevelViewModel(ILevelModel model, IInputProxy input, bool isEmpty = true)
        {
            _rayCastPosition = Vector3.forward;
            _rayCastPosition.z = Camera.main.farClipPlane;

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
            
            var go = GameObject.Instantiate(levelViewModel.LevelModel.LevelViewPrefab);
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

        private void TouchHandler(Touch[] touches)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                touchPosition.z = Camera.main.transform.position.z;
                Debug.DrawRay(touchPosition, _rayCastPosition);

                if (Views[0].HideCellByName(PlayerInput.StandaloneInputModule.GetHovered().name))
                {
                    Debug.Log($"{PlayerInput.StandaloneInputModule.GetHovered().name} hided");
                    #if !UNITY_EDITOR && UNITY_ANDROID
                    ShowAndroidToastMessage($"{PlayerInput.StandaloneInputModule.GetHovered().name} hided");
                    #endif
                }
                //Debug.Log($"{PlayerInput.StandaloneInputModule.GetHovered().name}");
                
            }
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

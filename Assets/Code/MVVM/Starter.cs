using System.Collections.Generic;
using UnityEngine;


namespace MVVM
{
    internal sealed class Starter : MonoBehaviour
    {
        #region Fields

        private ResourceLoader _resourceLoader;
        private ILevelModel _levelModel;
        private ILevelViewModel _levelViewModel;
        private List<IExecutable> _executables;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _executables = new List<IExecutable>();
            _resourceLoader = new ResourceLoader();

            _executables.Add(new InputViewModel(new AndroidInput()));

            _levelModel = new LevelModel(_resourceLoader.LoadPrefab("level_ui_template"));
            _levelViewModel = new LevelViewModel(_levelModel);

        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                transform.position = touchPosition;
            }
            //foreach (var executable in _executables)
            //{
            //    executable.Execute(Time.deltaTime);
            //}
        }

        #endregion


        #region Methods



        #endregion
    }
}

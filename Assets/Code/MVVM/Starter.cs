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

            var input = new InputViewModel(new AndroidInput());

            _executables.Add(input);

            _levelModel = new LevelModel(_resourceLoader.LoadPrefab("level_ui_template"));
            _levelViewModel = new LevelViewModel(_levelModel, input.Input, transform);

        }

        private void Update()
        {
            foreach (var executable in _executables)
            {
                executable.Execute(Time.deltaTime);
            }
        }

        #endregion


        #region Methods



        #endregion
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MVVM
{
    internal sealed class Starter : MonoBehaviour
    {
        #region Fields

        public CustomStandaloneInputModule CustomStandaloneInputModule;

        private ResourceLoader _resourceLoader;
        private InputViewModel _input;
        private ILevelModel _levelModel;
        private ILevelViewModel _levelViewModel;
        private List<IExecutable> _executables;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _executables = new List<IExecutable>();
            _resourceLoader = new ResourceLoader();

            _input = new InputViewModel(new AndroidInput(CustomStandaloneInputModule));

            _executables.Add(_input);

            _levelModel = new LevelModel(_resourceLoader.LoadPrefab("level_ui_template"));
            _levelViewModel = new LevelViewModel(_levelModel, _input, transform);

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
